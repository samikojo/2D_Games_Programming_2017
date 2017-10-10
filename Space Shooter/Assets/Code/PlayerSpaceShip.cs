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

		// The initial amount of lives that can be set in Unity editor and read by spawner. Ugly.
		[SerializeField]
		private int _lives = 3;

		// Time the ship is immortal after respawn.
		[SerializeField]
		private float _timeImmortal = 3f;

		// Blink period.
		[SerializeField]
		private float _blinkPeriod = .2f;

		public int Lives {
			get {
				return _lives;
			}
			set {
				_lives = value;
			}
		}

		// Holder for the player spawner and its getter and setter. Used to trigger re-spawn.
		private PlayerSpawn _spawner;

		public PlayerSpawn Spawner {
			private get {
				return _spawner;
			}
			set {
				_spawner = value;
			}
		}

		public override Type UnitType {
			get { return Type.Player; }
		}

		private Vector3 GetInputVector ()
		{
			float horizontalInput = Input.GetAxis (HorizontalAxis);
			float verticalInput = Input.GetAxis (VerticalAxis);

			return new Vector3 (horizontalInput, verticalInput);
		}

		protected override void Update ()
		{
			base.Update ();

			if (Input.GetButton (FireButtonName)) {
				Shoot ();
			}
		}

		protected override void Move ()
		{
			Vector3 inputVector = GetInputVector ();
			Vector2 movementVector = inputVector * Speed;
			transform.Translate (movementVector * Time.deltaTime);
		}

		protected override void Die ()
		{
			// Destroys the gameobject. Otherwise Unity will see two player ships.
			base.Die ();

			// Respawn if lives left.
			if (_lives > 0) {
				
				// Spawn new ship.
				PlayerSpaceShip newShip = Spawner.Spawn ();

				// The new ship has one life less;
				newShip.Lives = Lives - 1;
				Debug.Log ("Respawn with " + (Lives - 1).ToString () + " lives left.");

				// Make new ship immortal!
				newShip.MakeImmortal ();
		

			} else {

				Debug.Log ("Player died");
			
			}

		
		}

		protected void MakeImmortal ()
		{

			// Start the timed immortality.
			StartCoroutine (Immortality ());

		}

		private IEnumerator Immortality ()
		{

			// Disable collisions. Should use a more elegant solution, but I couldn't find an actual reason to do it yet.
			gameObject.GetComponent<Collider2D> ().enabled = false;

			Debug.Log ("Become Immortal.");

			// Make the player blink;
			StartCoroutine (Blink ());

			// Wait the set time.
			yield return new WaitForSeconds (_timeImmortal);

			Debug.Log ("Become mortal.");

			// Enable collisions.
			gameObject.GetComponent<Collider2D> ().enabled = true;
		}

		private IEnumerator Blink ()
		{
			int periods = (int)(_timeImmortal / _blinkPeriod);
			SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();

			while (periods > 0) {

				yield return new WaitForSeconds (_blinkPeriod/2f);

				renderer.enabled = false;

				yield return new WaitForSeconds (_blinkPeriod/2f);

				renderer.enabled = true;

				periods--; // Remember to count down. I did not. 

			}
		}
	}
}

