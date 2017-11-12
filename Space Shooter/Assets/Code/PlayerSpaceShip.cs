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

		[SerializeField]
		private float _immortalTime = 1;

		private float _blinkInterval = 0.1f;

        private float weaponPowerupTimer = 0f;
        private bool powerupActive = false;

		public override Type UnitType
		{
			get { return Type.Player; }
		}

        public void Start()
        {
            // Update the interface text.
            LevelController.Current.UpdateHealth(Health.CurrentHealth);
        }

		private Vector3 GetInputVector()
		{
			float horizontalInput = Input.GetAxis(HorizontalAxis);
			float verticalInput = Input.GetAxis(VerticalAxis);

			return new Vector3(horizontalInput, verticalInput);
		}

		protected override void Update()
		{
			base.Update();

            if (powerupActive)
            {
                LevelController.Current.UpdatePowerup("Shotgun " + System.Math.Round(weaponPowerupTimer, 1) + "s");
                weaponPowerupTimer -= Time.deltaTime;
                
                if (weaponPowerupTimer < 0f)
                {
                    GameObject temp;
                    _weapons = null;
                    Transform[] powerupWeapons = transform.GetComponentsInChildren<Transform>();

                    for (int i = 0; i < powerupWeapons.Length; i++)
                    {
                        temp = powerupWeapons[i].gameObject;

                        if (temp.layer == 13)
                        {
                            Destroy(temp);
                        }
                    }

                    UpdateWeapons();
                    powerupActive = false;
                    LevelController.Current.UpdatePowerup("");
                }
            }

            if (Input.GetButton(FireButtonName))
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

		protected override void Die()
		{
			base.Die();
			GameManager.Instance.CurrentLives--;
		}

		public void BecomeImmortal()
        {
            // Update the interface.
            LevelController.Current.ToggleImmortal(true, Health.CurrentHealth);

            StartCoroutine(ImmortalRoutine());
		}

		private IEnumerator ImmortalRoutine()
		{
			Health.SetImmortal(true);
			SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
			if(spriteRenderer == null)
			{
				throw new System.Exception("No renderer found from Player Spaceship object!");
			}

			float timer = 0f;
			Color color = spriteRenderer.color;

			while (timer < _immortalTime)
			{
				timer += _blinkInterval;
				// A short way to write if-else.
				color.a = color.a == 1 ? 0 : 1;
				spriteRenderer.color = color;
				yield return new WaitForSeconds(_blinkInterval);
			}

			color.a = 1;
			spriteRenderer.color = color;

			Health.SetImmortal(false);

            // Update the interface text.
            LevelController.Current.ToggleImmortal(false, Health.CurrentHealth);
		}

        public override void TakeDamage(int amount)
        {
            Health.DecreaseHealth(amount);

            // Update the interface text.
            LevelController.Current.UpdateHealth(Health.CurrentHealth);

            if (Health.IsDead)
            {
                Die();
            }
        }

        public override void TakeHealth(int amount)
        {
            Health.IncreaseHealth(amount);

            // Update the interface text.
            LevelController.Current.UpdateHealth(Health.CurrentHealth);
        }

        public void GivePowerup(GameObject powerup, float time)
        {
            weaponPowerupTimer += time;

            if (powerupActive)
            {
                return;
            }
            else
            {
                powerupActive = true;

                int newWeapons = powerup.transform.childCount;

                Transform temp = null;
                Transform[] powerupWeapons = powerup.transform.GetComponentsInChildren<Transform>();

                // Transfer powerup weapon parentage to PlayerSpaceShip:
                for (int i = 0; i < powerupWeapons.Length; i++)
                {
                    temp = powerupWeapons[i];
                    temp.SetParent(transform, false);
                }

                // Add powerup weapons to weapons array:
                UpdateWeapons();

                // Update user interface:
                LevelController.Current.UpdatePowerup("Shotgun " + System.Math.Round(weaponPowerupTimer, 1) + "s");
            }
        }
    }
}
