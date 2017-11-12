using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class HealthPowerUp : PowerupBase
    {
        [SerializeField] int _healAmount = 20;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                damageReceiver.TakeHealth(_healAmount);
            }

            Destroy(gameObject);
        }
    }
}
