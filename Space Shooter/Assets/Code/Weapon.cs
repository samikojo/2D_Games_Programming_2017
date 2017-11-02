using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField]
		private float _cooldownTime = 0.5f;
		[SerializeField]
		private Projectile _projectilePrefab;

		private float _timeSinceShot = 0;
		private bool _isInCooldown = false;
		private SpaceShipBase _owner;

		public void Init( SpaceShipBase owner )
		{
			_owner = owner;
		}
		
		public bool Shoot()
		{
			if(_isInCooldown)
			{
				return false;
			}
			
			// Get the projectile from the pool and set its position and rotation.
			Projectile projectile = LevelController.Current.GetProjectile(_owner.UnitType);
			if(projectile != null)
			{
				projectile.transform.position = transform.position;
				projectile.transform.rotation = transform.rotation;
				projectile.Launch(this, transform.up);

				// Go to the cooldown phase.
				_isInCooldown = true;
				// We just shot the projectile so time since shot is 0.
				_timeSinceShot = 0;

				return true;
			}

			return false;
		}

		public bool DisposeProjectile( Projectile projectile )
		{
			return LevelController.Current.ReturnProjectile(_owner.UnitType, projectile);
		}
		
		void Update()
		{
			if(_isInCooldown)
			{
				_timeSinceShot += Time.deltaTime;
				if(_timeSinceShot >= _cooldownTime)
				{
					_isInCooldown = false;
				}
			}
		}
	}
}
