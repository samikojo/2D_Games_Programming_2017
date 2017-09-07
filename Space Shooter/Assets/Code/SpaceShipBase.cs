using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public abstract class SpaceShipBase : MonoBehaviour
	{
		// Backing field for the property Speed.
		// SerializeField attribute forces Unity to serialize this variable
		// in order to make it editable inside the editor.
		[SerializeField]
		private float _speed = 1.5f;

		public float Speed
		{
			get { return _speed; }
			protected set { _speed = value; }
		}

		protected abstract void Move();

		protected virtual void Update()
		{
			try
			{
				Move();
			}
			catch(System.NotImplementedException exception)
			{
				Debug.Log(exception.Message);
			}
			catch(System.Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
