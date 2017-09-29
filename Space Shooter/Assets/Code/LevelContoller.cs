using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
	public class LevelContoller : MonoBehaviour
	{
		public static LevelContoller Current
		{
			get; private set;
		}

		[SerializeField]
		private Spawner _enemySpawner;

		[SerializeField]
		private GameObject[] _enemyMovementTargets;

		// How often we should spawn a new enemy.
		[SerializeField]
		private float _spawnInterval = 1;

		[SerializeField, Tooltip("The time before the first spawn.")]
		private float _waitToSpawn;

		// Maximum amount of enemies to spawn.
		[SerializeField]
		private int _maxEnemyUnitsToSpawn;

		[SerializeField]
		private GameObjectPool _playerProjectilePool;

		[SerializeField]
		private GameObjectPool _enemyProjectilePool;

		// Amount of enemies spawned so far.
		private int _enemyCount;

		protected void Awake()
		{
			if(Current == null)
			{
				Current = this;
			}
			else
			{
				Debug.LogError("There are multiple LevelControllers in the scene!");
			}

			if(_enemySpawner == null)
			{
				Debug.Log("No reference to an enemy spawner.");
				//_enemySpawner = GameObject.FindObjectOfType<Spawner>();
				_enemySpawner = GetComponentInChildren<Spawner>();
			}
		}

		protected void Start()
		{
			// Starts a new coroutine.
			StartCoroutine(SpawnRoutine());
		}

		private IEnumerator SpawnRoutine()
		{
			// Wait for a while before spawning the first enemy.
			yield return new WaitForSeconds(_waitToSpawn);

			while(_enemyCount < _maxEnemyUnitsToSpawn)
			{
				EnemySpaceShip enemy = SpawnEnemyUnit();
				if(enemy != null)
				{
					// Same as _enemyCount = _enemyCount + 1;
					_enemyCount++;
				}
				else
				{
					Debug.LogError("Could not spawn an enemy!");
					yield break; // Stops the execution of this coroutine.
				}
				yield return new WaitForSeconds(_spawnInterval);
			}
		}

		private EnemySpaceShip SpawnEnemyUnit()
		{
			GameObject spawnedEnemyObject = _enemySpawner.Spawn();
			EnemySpaceShip enemyShip = spawnedEnemyObject.GetComponent<EnemySpaceShip>();
			if(enemyShip != null)
			{
				enemyShip.SetMovementTargets(_enemyMovementTargets);
			}
			return enemyShip;
		}

		public Projectile GetProjectile(SpaceShipBase.Type type)
		{
			GameObject result = null;

			// Try to get pooled object from the correct pool based on the type
			// of the spaceship.
			if(type == SpaceShipBase.Type.Player)
			{
				result = _playerProjectilePool.GetPooledObject();
			}
			else
			{
				result = _enemyProjectilePool.GetPooledObject();
			}

			// If the pooled object was found, get the Projectile component
			// from it and return that. Otherwise just return null.
			if(result != null)
			{
				return result.GetComponent<Projectile>();
			}
			return null;
		}

		public bool ReturnProjectile(SpaceShipBase.Type type, Projectile projectile)
		{
			if(type == SpaceShipBase.Type.Player)
			{
				return _playerProjectilePool.ReturnObject(projectile.gameObject);
			}
			else
			{
				return _enemyProjectilePool.ReturnObject(projectile.gameObject);
			}
		}
	}
}
