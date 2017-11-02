using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShooter.States
{
	public class GameOverState : GameStateBase
	{
		public override string SceneName
		{
			get { return "GameOver"; }
		}

		public override GameStateType StateType
		{
			get { return GameStateType.GameOver; }
		}

		public GameOverState()
		{
			AddTargetState(GameStateType.MainMenu);
		}
	}
}
