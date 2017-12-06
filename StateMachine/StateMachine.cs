using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IzzoLib;

/// <summary> 
/// Create a StateMachine object wherever you want. Use OverrideCurrentState to change state.
/// Create your states by implementing IState interface.
/// Adapt the public enum to your needs.
/// <seealso cref="IState">
/// </summary>
namespace IzzoLib
{
	public class StateMachine {

		private IState _current_state;

		public void Update()
		{
			if (_current_state != null)
			{
				IState new_state = _current_state.Execute ();
				if (new_state != null)
				{
					OverrideCurrentState (new_state);	
				}
					
			}
		}

		public void OverrideCurrentState(IState new_state)
		{
			if (_current_state != null)
			{
				_current_state.Exit ();
			}

			_current_state = new_state;

			if (_current_state != null)
			{
				_current_state.Enter ();
			}
		}

		public eStateType GetCurrentStateType()
		{
			return _current_state == null ? eStateType.none : _current_state.GetStateType ();
		}
	}

	public enum eStateType
	{
		none = 0,
		moving,
		using_element,
		idle
	}
}
