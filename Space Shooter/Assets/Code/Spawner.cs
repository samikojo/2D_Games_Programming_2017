using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField]
		private GameObject _prefabToSpawn;

		public GameObject Spawn()
		{
            // Spawn with spawner as the parent.
			GameObject spawnedObject = Instantiate(_prefabToSpawn,
				transform.position, transform.rotation, transform);

			return spawnedObject;
		}
	}
}
