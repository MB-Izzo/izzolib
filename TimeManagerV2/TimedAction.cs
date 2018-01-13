using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace IzzoLib
{
	
	public enum eActionStatus
	{
		running,
		paused,
		interrupted,
		finished
	}

	// Define action state to be done in a certain time.
	// Needs a start time and a duration = update this object (when running).
	// Can call action when it's finished. 
	public class TimedAction 
	{
		/// <summary>>
		/// Action to be called when TimedAction is running.
		/// Float: ratio value.
		/// </summary>
		private Action<TimedAction, float> _update_cb;
		/// <summary>>
		/// Action called on finished TimedAction.
		/// Parameter: ended TimedAction.
		/// </summary>
		private Action<TimedAction> _end_cb;
		private float _time_elapsed;

		// Public getters.
		public float start_time { get; private set; }
		public float duration { get; private set; }
		public float ratio { get { return _time_elapsed / duration; } }
		public eActionStatus current_status { get; private set; }

		// Helpers.
		private bool _action_finished { get { return ratio == 1.0f; } }

		///<summary>
		/// Class constructor.
		/// </summary>
		public TimedAction(float duration, Action<TimedAction, float> update_cb = null, Action<TimedAction> end_cb = null)
		{
			this.duration = duration;
			this.start_time = TimeManager.instance.game_time;
			this.current_status = eActionStatus.running;
			this._time_elapsed = 0.0f;
			this._update_cb = update_cb;
			this._end_cb = end_cb;
		}

		///<summary>
		/// Called by TimeManager to update when running.
		/// Returns the current status of the TimedAction object.
		/// </summary>
		public eActionStatus Update(float delta)
		{
			if (current_status == eActionStatus.running)
			{
				_time_elapsed += delta;
				if (_time_elapsed > duration)
					_time_elapsed = duration;

				if (_action_finished == false)
				{
					if (_update_cb != null)
						_update_cb (this, ratio);
				}
				else
				{
					Stop();
				}
			}

			return current_status;
		}

		public void Reset()
		{
			_time_elapsed = 0.0f;
			current_status = eActionStatus.running;
		}

		public void Pause()
		{
			if (current_status != eActionStatus.finished)
				current_status = eActionStatus.paused;
		}

		public void Resume()
		{
			if (current_status != eActionStatus.finished)
				current_status = eActionStatus.running;
		}

		public void Stop()
		{
			current_status = _action_finished ? eActionStatus.finished : eActionStatus.interrupted;
			if (_end_cb != null)
				_end_cb (this);
		}
	}
}

