using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IzzoLib;

namespace IzzoLib
{
	public interface IState {
		void Enter();
		IState Execute();
		void Exit();
		eStateType GetStateType();
	}

}
