using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.States;
using TMPro;

namespace SpaceShooter
{
	public class LevelController : MonoBehaviour
	{
		public static LevelController Current
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

		[SerializeField]
		private Spawner _playerSpawner;

		[SerializeField]
		private int _targetEnemiesKilled = 5;

		[SerializeField]
		private GameStateType _nextState;

		[SerializeField]
		private bool _isLastLevel = false;

        // Reference to the text that shows player health.
        [SerializeField]
        private TextMeshProUGUI _heathText;

        // Reference to text that shows weapon powerup information
        [SerializeField]
        private TextMeshProUGUI _powerupText;

        [SerializeField]
        private float _powerupSpawnProbability = 0.3f;

        [SerializeField] GameObject[] _powerups;

        private bool _playerImmortal = false;

        // Amount of enemies spawned so far.
        private int _enemyCount;

		private int _killedEnemies = 0;


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

            // Sanity checks about interface texts:
            if (_heathText == null || _powerupText == null)
            {
                GameObject temp = GameObject.Find("HealthText");

                if (temp != null)
                    _heathText = temp.GetComponent<TextMeshProUGUI>();
                else
                    Debug.Log("PowerupText not found!");

                temp = GameObject.Find("PowerupText");

                if (temp != null)
                    _powerupText = temp.GetComponent<TextMeshProUGUI>();
                else
                    Debug.Log("PowerupText not found!");
            }
		}

		protected void Start()
		{
			// Starts a new coroutine.
			StartCoroutine(SpawnEnemyRoutine());
			SpawnPlayer();
		}

		private IEnumerator SpawnEnemyRoutine()
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

		public void EnemyDestroyed()
		{
			_killedEnemies++;
			if(_killedEnemies >= _targetEnemiesKilled)
			{
				if(_isLastLevel)
				{
					GameManager.Instance.PlayerWins = true;
				}

				if( GameStateController.PerformTransition(_nextState) == false)
				{
					Debug.LogError("Could not change state to " + _nextState);
				}
			}
		}

		public PlayerSpaceShip SpawnPlayer()
		{
			PlayerSpaceShip player = null;
			GameObject playerObject = _playerSpawner.Spawn();
			if (playerObject != null)
			{
				player = playerObject.GetComponent<PlayerSpaceShip>();
			}

			player.BecomeImmortal();

			return player;
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
				Projectile projectile = result.GetComponent<Projectile>();
				if(projectile == null)
				{
					Debug.LogError("Projectile component could not be found " +
						"from the object fetched from the pool.");
				}
				return projectile;
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

		public void LivesLost()
		{
			if(GameManager.Instance.CurrentLives <= 0)
			{
				GameStateController.PerformTransition(GameStateType.GameOver);
			}
			else
			{
				SpawnPlayer();
			}
		}

        // Updates the amount of health the player has on the screen.
        public void UpdateHealth(int health)
        {
            if (!_playerImmortal)
            {
                _heathText.text = "Health: " + health;
            }
        }

        // Used to show player information about powerup duration.
        public void UpdatePowerup(string text)
        {
            _powerupText.text = text;
        }

        // Updates the healthText when player becomes immortal or becomes mortal
        public void ToggleImmortal(bool immortal, int health)
        {
            _playerImmortal = immortal;

            if (immortal)
            {
                _heathText.text = "Invulnerable";
            }
            else
            {
                UpdateHealth(health);
            }
        }

        // Might spawn a random powerup if random number meets probability requirement.
        public void SpawnPowerup(Vector3 position)
        {
            if (Random.value < _powerupSpawnProbability && _powerups.Length > 0)
            {
                /* Pick a random powerup to spawn.
                 * Used whole float numbers to avoid fractions rounding up/down.
                 */
                int options = _powerups.Length;
                float random = Random.value * (options * 1.0f);
                float range = 1f;
                float lowerLimit = 0f;
                float upperLimit = 0f + range;

                for (int i = 0; i < options; i++)
                {
                    if (lowerLimit <= random &&
                        upperLimit >= random)
                    {
                        GameObject go = Instantiate<GameObject>(_powerups[i], position, Quaternion.identity, transform);
                        Debug.Log("Spawned " + go);
                        break;
                    }
                    else
                    {
                        lowerLimit += range;
                        upperLimit += range;
                    }
                }
            }
            else
            {
                Debug.Log("Bad luck!");
                return;
            }
        }
	}
}
