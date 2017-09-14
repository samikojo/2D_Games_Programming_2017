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

		private Weapon[] _weapons;

		public float Speed
		{
			get { return _speed; }
			protected set { _speed = value; }
		}

		public Weapon[] Weapons
		{
			get { return _weapons; }
		}

		protected virtual void Awake()
		{
			_weapons = GetComponentsInChildren<Weapon>(includeInactive:true);
		}

		protected void Shoot()
		{
			foreach( Weapon weapon in Weapons )
			{
				weapon.Shoot();
			}
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
