using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShooter.States
{
	public class Level1State : GameStateBase
	{
		public override string SceneName
		{
			get { return "Level1"; }
		}

		public override GameStateType StateType
		{
			get { return GameStateType.Level1; }
		}

		public Level1State()
		{
			AddTargetState(GameStateType.Level2);
			AddTargetState(GameStateType.GameOver);
		}
	}
}
