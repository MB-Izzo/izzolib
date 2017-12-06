using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IzzoLib;

/// <sumary>
/// A TimedAction has a duration, can be played, paused, and reversed. 
/// It is updated by TimeManager. 
/// Can be created by a TimedActionFactory.
/// <seealso cref="TimeManager">
/// <seealso cref="TimedActionFactory">
/// </sumary>
namespace IzzoLib
{
	public class TimedAction {

		private float _time_started;
		public float time_started { get { return _time_started; } }

		private float _action_ratio;
		public float ratio { get { return _action_ratio; } }

		private float _time_progress;
		public float time_progress { get { return _time_progress; } }

		private float _time_remaining;
		public float time_remaining { get { return _time_remaining; } }

		private float _duration;
		public float duration { get { return _duration; } }

		private eStateTime _current_state;

		public TimedAction(float duration, float time_started)
		{
			this._time_started = time_started;
			this._duration = duration;
			this._action_ratio = 0.0f;
			this._time_remaining = duration;
			this._current_state = eStateTime.paused;
		}

		public virtual void Update(float dt)
		{
			switch(_current_state)
			{
				case eStateTime.playing:
					_time_progress += dt;
					_time_remaining -= dt;
					break;
				case eStateTime.paused:
					// do not update.
					break;
				case eStateTime.reversed:
					_time_progress -= dt;
					_time_remaining +=dt;
					break;
				default:
					break;
			}

			_action_ratio = Mathf.Min((float)_time_progress / _duration, 1f);
		}

		#region TimedAction API

		public bool IsFinished()
		{
			return _action_ratio == 1;
		}

		public void Play()
		{
			if (_current_state == eStateTime.playing)
			{
				Debug.Log("Action already playing.");
			}
			else
			{
				_current_state = eStateTime.playing;
			}
		}

		public void Stop()
		{
			if (_current_state == eStateTime.paused)
			{
				Debug.Log("Action already paused.");

			}
			else
			{
				_current_state = eStateTime.paused;
			}
		}

		public void Reverse()
		{
			if (_current_state == eStateTime.reversed)
			{
				Debug.Log("Reverse already playing.");
			}
			else
			{
				_current_state = eStateTime.reversed;
			}
		}

		public eStateTime GetStateType()
		{
			return _current_state;
		}

		#endregion
	}

	public enum eStateTime {
		playing,
		paused,
		reversed
	}

}

