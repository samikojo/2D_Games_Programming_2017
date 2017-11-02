using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SpaceShooter.States;

namespace SpaceShooter.UI
{
	public class GameOver : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _scoreText;

		[SerializeField]
		private TextMeshProUGUI _winText;

		// Called every time GameObject is activated. If the GameObject is active when
		// it is instantiated, OnEnable will be called right after Awake is called.
		private void OnEnable()
		{
			SetScore(GameManager.Instance.CurrentScore);
		}

		public void ToMainMenu()
		{
			GameStateController.PerformTransition(GameStateType.MainMenu);
		}

		private void SetScore(int score)
		{
			if(_scoreText != null)
			{
				_scoreText.text = "Score: " + score;
			}

			if(_winText != null)
			{
				string text;
				if(GameManager.Instance.PlayerWins)
				{
					text = "Player Wins!";
				}
				else
				{
					text = "Player Loses!";
				}

				_winText.text = text;
			}
		}
	}
}
