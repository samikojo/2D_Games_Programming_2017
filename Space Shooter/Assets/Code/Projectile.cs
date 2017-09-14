using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class Projectile : MonoBehaviour, IDamageProvider
	{
		[SerializeField]
		private int _damage;
		[SerializeField]
		private float _speed;

		private Rigidbody2D _rigidbody;
		private Vector2 _direction;
		private bool _isLaunched = false;

		protected virtual void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();

			if (_rigidbody == null)
			{
				Debug.LogError("No Rigidbody2D component was found from the GameObject");
			}
		}

		protected void FixedUpdate()
		{
			if (!_isLaunched)
			{
				return;
			}

			Vector2 velocity = _direction * _speed;
			// Current position can be read from transform
			Vector2 currentPosition =
				new Vector2(transform.position.x, transform.position.y);
			Vector2 newPosition = currentPosition + velocity * Time.fixedDeltaTime;
			_rigidbody.MovePosition(newPosition);
		}

		public void Launch(Vector2 direction)
		{
			_direction = direction;
			_isLaunched = true;
		}

		public int GetDamage()
		{
			return _damage;
		}
	}
}
