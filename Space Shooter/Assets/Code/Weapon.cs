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
		
		public bool Shoot()
		{
			if(_isInCooldown)
			{
				return false;
			}

			// Instantiate Projectile object and launch it.
			Projectile projectile = 
				Instantiate(_projectilePrefab, transform.position, transform.rotation);
			projectile.Launch(transform.up);

			// Go to the cooldown phase.
			_isInCooldown = true;
			// We just shot the projectile so time since shot is 0.
			_timeSinceShot = 0;

			return true;
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
