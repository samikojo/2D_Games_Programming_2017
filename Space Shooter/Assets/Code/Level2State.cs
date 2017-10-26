using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShooter.States
{
	public class Level2State : GameStateBase
	{
		public override string SceneName
		{
			get { return "Level2"; }
		}

		public override GameStateType StateType
		{
			get { return GameStateType.Level2; }
		}

		public Level2State()
		{
			AddTargetState(GameStateType.GameOver);
		}
	}
}
