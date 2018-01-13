using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace IzzoLib
{
	public enum eTimeGroup
	{
		common,
		menu
	}

	/// <summary>
	/// Allows to use several in game timers.
	/// Allows to pause/resume/stop a group of TimedActions.
	/// </summary>
	public class TimeManager : MonoBehaviour 
	{
		// TimedActions grouped by TimeGroup type.
		private Dictionary<eTimeGroup, List<TimedAction>> _time_actions;
		private Dictionary<eTimeGroup, List<TimedAction>> _next_actions;

		public float game_time { get; private set; }
		public static TimeManager instance { get; private set; }

		private void Awake()
		{
			instance = this;
			_time_actions = new Dictionary<eTimeGroup, List<TimedAction>> ();
			_next_actions = new Dictionary<eTimeGroup, List<TimedAction>> ();
		}

		/// <summary>
		/// Add an action to a group.
		/// </summary>
		/// <param name="new_action">New action to add.</param>
		/// <param name="group">Group of the TimedAction that is being added.</param>
		public void RegisterTimedAction (TimedAction new_action, eTimeGroup group = eTimeGroup.common)
		{
			// if group does not exit, create it.
			if (_next_actions.ContainsKey (group) == false)
				_next_actions.Add (group, new List<TimedAction> ());

			// Error check.
			if (new_action != null)
			{
				// Add the action to the list of the right group.
				_next_actions [group].Add (new_action);
			}
			else
			{
				Debug.LogError ("Can't add null TimedAction to TimeManager.");
			}
		}

		/// <summary>
		/// Pauses a group.
		/// </summary>
		/// <param name="group">Group to pause.</param>
		public void PauseGroup(eTimeGroup group)
		{
			if (_time_actions.ContainsKey (group) == true)
			{
				List<TimedAction> actions = _time_actions [group];
				foreach (TimedAction ta in actions)
				{
					ta.Pause ();
				}
			}
		}

		/// <summary>
		/// Resumes a group.
		/// </summary>
		/// <param name="group">Group to resume.</param>
		public void ResumeGroup(eTimeGroup group)
		{
			if (_time_actions.ContainsKey (group) == true)
			{
				List<TimedAction> actions = _time_actions [group];
				foreach (TimedAction ta in actions)
				{
					ta.Resume ();
				}
			}
		}

		/// <summary>
		/// Stops a group.
		/// </summary>
		/// <param name="group">Group to stop</param>
		public void StopGroup(eTimeGroup group)
		{
			if (_time_actions.ContainsKey (group) == true)
			{
				List<TimedAction> actions = _time_actions [group];
				foreach (TimedAction ta in actions)
				{
					ta.Stop ();
				}
			}
		}

		private void AddNextActions()
		{
			if (_next_actions.Count != 0)
			{
				// for each existing group.
				foreach (KeyValuePair<eTimeGroup, List<TimedAction>> pair in _next_actions)
				{
					List<TimedAction> actions = pair.Value;
					foreach (TimedAction action in actions)
					{
						// If time_actions list does not already contains the next.
						if (_time_actions.ContainsKey (pair.Key) == false)
						{
							_time_actions.Add (pair.Key, actions);
						}
						else
						{
							_time_actions [pair.Key].AddRange (actions);
						}
					}
				}
			}
			_next_actions.Clear ();
		}
			
		private void Update () {
			game_time += Time.deltaTime;
			AddNextActions (); 

			List<TimedAction> finished_actions;
			TimedAction current_timed_action;

			// Get TimedAction list for every groups.
			foreach (KeyValuePair<eTimeGroup, List<TimedAction>> pair in _time_actions)
			{
				finished_actions = new List<TimedAction> ();
				IEnumerator<TimedAction> enumerator = pair.Value.GetEnumerator ();
				while (enumerator.MoveNext () == true)
				{
					current_timed_action = enumerator.Current;
					if (current_timed_action.Update (Time.deltaTime) == eActionStatus.finished)
					{
						finished_actions.Add (current_timed_action);
					}
				}

				// Remove finished actions
				foreach (TimedAction ta in finished_actions)
				{
					pair.Value.Remove (ta);
				}
			}
		}
	}
}
