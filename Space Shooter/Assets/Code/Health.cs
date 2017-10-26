using UnityEngine;

namespace SpaceShooter
{
	public class Health : MonoBehaviour, IHealth
	{
		[SerializeField] private int _initialHealth;
		[SerializeField] private int _minHealth;
		[SerializeField] private int _maxHealth;

		private int _currentHealth;
		private bool _isImmortal = false;

		public int CurrentHealth
		{
			get
			{
				return _currentHealth;
			}
			private set
			{
				_currentHealth = Mathf.Clamp(value, _minHealth, _maxHealth);
			}
		}

		public bool IsDead
		{
			get { return CurrentHealth == _minHealth; }
		}

		protected void Awake()
		{
			CurrentHealth = _initialHealth;
		}

		public void DecreaseHealth(int amount)
		{			
			if (!_isImmortal)
			{
				// CurrentHealth = CurrentHealth - amount;
				CurrentHealth -= amount;
			}
		}

		public void IncreaseHealth(int amount)
		{
			CurrentHealth += amount;
		}

		public void SetImmortal(bool isImmortal)
		{
			_isImmortal = isImmortal;
		}
	}
}
