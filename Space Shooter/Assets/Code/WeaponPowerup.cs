using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class WeaponPowerup : PowerupBase
    {
        [SerializeField] float duration = 5f;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null)
            {
                player.GivePowerup(gameObject, duration);
            }

            Destroy(gameObject);
        }
    }
}
