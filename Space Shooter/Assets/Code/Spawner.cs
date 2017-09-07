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
			GameObject spawnedObject = Instantiate(_prefabToSpawn,
				transform.position, transform.rotation);
			return spawnedObject;
		}
	}
}
