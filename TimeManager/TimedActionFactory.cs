using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IzzoLib;

/// <sumary>
/// This class is used to create a TimedAction. Calls are all static.
/// </sumary>
/// <seealso cref="TimedAction">
namespace IzzoLib
{
	public class TimedActionFactory {

		public static TimedAction CreateTimedAction(float duration, float time)
		{
			TimedAction new_action = new TimedAction(duration, time);
			TimeManager.RegisterTimedAction(new_action);
			return new_action;
		}

		public static TimedAction CreateTimedAction(TimedAction existing_timed_action)
		{
			TimedAction new_action = existing_timed_action;
			TimeManager.RegisterTimedAction(new_action);
			return new_action;
		}

	}

}
