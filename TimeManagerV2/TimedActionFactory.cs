using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace IzzoLib
{
	public static class TimedActionFactory {

		/// <summary>
		/// Creates a timed action and registers it to TimeManager.
		/// </summary>
		/// <returns>The timed action.</returns>
		/// <param name="duration">Duration of the action</param>
		/// <param name="group">Group of the action</param>
		/// <param name="update_callback">Update callback.</param>
		/// <param name="end_callback">End callback.</param>
		public static TimedAction CreateTimedAction(float duration, eTimeGroup group, Action<TimedAction, float> update_callback = null, Action<TimedAction> end_callback = null)
		{
			TimedAction new_action = new TimedAction (duration, update_callback, end_callback);
			TimeManager.instance.RegisterTimedAction (new_action, group);
			return new_action;
		}
	}
}
