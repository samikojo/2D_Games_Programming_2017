using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpaceShooter.States
{
	public static class GameStateController
	{
		private static List<GameStateBase> _gameStates =
			new List<GameStateBase>();

		public static GameStateBase CurrentState { get; private set; }

		static GameStateController()
		{
			if(!AddStartingState(new MenuState()))
			{
				Debug.LogError("Couldn't add starting state");
				return;
			}

			_gameStates.Add(new Level1State());
			_gameStates.Add(new Level2State());
		}

		public static bool PerformTransition(GameStateType targetStateType)
		{
			if (CurrentState.IsValidTargetState(targetStateType) == false)
			{
				return false;
			}

			GameStateBase state = GetStateByType(targetStateType);

			if(state == null)
			{
				return false;
			}

			CurrentState.Deactivate();
			CurrentState = state;
			CurrentState.Activate();

			return true;
		}

		private static bool AddStartingState(GameStateBase startingState)
		{
			foreach ( GameStateBase state in _gameStates )
			{
				if(state.StateType == startingState.StateType)
				{
					return false;
				}
			}

			_gameStates.Add(startingState);
			CurrentState = startingState;
			CurrentState.Activate();
			return true;
		}

		public static GameStateBase GetStateByType(GameStateType targetStateType)
		{
			GameStateBase result = null;
			foreach (GameStateBase gameState in _gameStates)
			{
				if (gameState.StateType == targetStateType)
				{
					result = gameState;
					break;
				}
			}
			return result;
		}
	}
}
