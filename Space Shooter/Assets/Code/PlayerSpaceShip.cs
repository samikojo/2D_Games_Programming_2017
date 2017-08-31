using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpaceShip : MonoBehaviour
	{
		public float Speed = 1.5f;

		// Update is called once per frame
		void Update()
		{
			Vector3 movementVector = GetMovementVector();
			transform.Translate(movementVector * Speed * Time.deltaTime);
		}

		private Vector3 GetMovementVector()
		{
			// This variable will have the movement vector after we have read
			// user's input.
			Vector3 movementVector = Vector3.zero;

			if( Input.GetKey(KeyCode.LeftArrow) )
			{
				// Same as movementVector = movementVector + Vector3.left;
				movementVector += Vector3.left;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				movementVector += Vector3.right;
			}
			if(Input.GetKey(KeyCode.UpArrow))
			{
				movementVector += Vector3.up;
			}
			if(Input.GetKey(KeyCode.DownArrow))
			{
				movementVector += Vector3.down;
				//movementVector += new Vector3(0, -1, 0);
			}
			//if(Input.GetAxis("Horizontal"))

			return movementVector;
		}
	}
}
