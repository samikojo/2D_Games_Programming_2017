using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceShooter.States
{
	public class MenuState : GameStateBase
	{
		public override string SceneName
		{
			get { return "MainMenu"; }
		}

		public override GameStateType StateType
		{
			get { return GameStateType.MainMenu; }
		}

		// Constructor. This is called right after the object is instantiated.
		public MenuState()
		{
			AddTargetState(GameStateType.Level1);
		}

		public override void Activate()
		{
			base.Activate();

			GameManager.Instance.Reset();
		}
	}
}
