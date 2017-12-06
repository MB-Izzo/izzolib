using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IzzoLib;

/// <sumary>
/// This class updates all TimedAction. It also registers TimedAction to a List.
/// <seealso cref="TimedAction">
/// <seealso cref="TimedActionFactory">
/// </sumary>
namespace IzzoLib
{
	public static class TimeManager {

		public static List<TimedAction> timed_actions = new List<TimedAction>();

		public static TimedAction RegisterTimedAction(TimedAction timed_action)
		{
			timed_actions.Add(timed_action);
			return timed_action;
		}

		public static void Update()
		{
			foreach(TimedAction timed_action in timed_actions)
			{
				timed_action.Update(Time.deltaTime);
			}
		}

	}
}

