using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class PowerupBase : MonoBehaviour
    {
        [SerializeField] float _dropSpeed = 0.5f;
        [SerializeField] private float lifetime = 5f;
        private Rigidbody2D _rigidbody;

        // Use this for initialization
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            if (_rigidbody == null)
            {
                Debug.LogError("No Rigidbody2D component was found from the GameObject");
            }
        }

        // Move powerup directly and destroy if not picked up in time.
        void FixedUpdate()
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 velocity = Vector2.down * _dropSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(currentPosition + velocity);

            lifetime -= Time.fixedDeltaTime;

            if (lifetime < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
