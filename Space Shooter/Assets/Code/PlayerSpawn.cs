using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class PlayerSpawn : MonoBehaviour
	{

		[SerializeField]
		private GameObject _prefabToSpawn;

		private PlayerSpaceShip _spawnedPlayer;

		// Use this for initialization
		void Start ()
		{

			// Spawn the original player ship.
			Spawn ();
					
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		public PlayerSpaceShip Spawn ()
		{
			
				GameObject spawnedObject = Instantiate (_prefabToSpawn, transform.position, transform.rotation);
				PlayerSpaceShip _spawnedPlayer = spawnedObject.GetComponent<PlayerSpaceShip> ();
				_spawnedPlayer.Spawner = this;
				
			return _spawnedPlayer;
		}
	}
}

