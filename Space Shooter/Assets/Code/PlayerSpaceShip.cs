using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpaceShip : SpaceShipBase
	{
		public const string HorizontalAxis = "Horizontal";
		public const string VerticalAxis = "Vertical";
		public const string FireButtonName = "Fire1";

		private Vector3 GetInputVector()
		{
			float horizontalInput = Input.GetAxis(HorizontalAxis);
			float verticalInput = Input.GetAxis(VerticalAxis);

			return new Vector3(horizontalInput, verticalInput);
		}

		protected override void Update()
		{
			base.Update();

			if(Input.GetButton(FireButtonName))
			{
				Shoot();
			}
		}

		protected override void Move()
		{
			Vector3 inputVector = GetInputVector();
			Vector2 movementVector = inputVector * Speed;
			transform.Translate(movementVector * Time.deltaTime);
		}
	}
}
