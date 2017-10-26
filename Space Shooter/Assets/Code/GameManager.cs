using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpaceShooter
{
	public class GameManager : MonoBehaviour
	{
		#region Statics
		private static GameManager _instance;

		public static GameManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Instantiate(Resources.Load<GameManager>("GameManager"));
				}
				return _instance;
			}
		}
		#endregion Statics

		[SerializeField]
		private int _startingLives;

		private int _currentLives;

		public int CurrentScore { get; private set; }

		public int CurrentLives
		{
			get { return _currentLives; }
			set
			{
				_currentLives = value;
				if(_currentLives <= 0)
				{
					_currentLives = 0;
				}
				if(LevelContoller.Current != null)
				{
					LevelContoller.Current.LivesLost();
				}
			}
		}

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
			else if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Init();
		}

		private void Init()
		{
			DontDestroyOnLoad(gameObject);
			Reset();
		}

		public void Reset()
		{
			_currentLives = _startingLives;
			CurrentScore = 0;
		}
	}
}
