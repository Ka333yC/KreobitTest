using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.Clicker.UI
{
	public class GamePage : MonoBehaviour
	{
		[SerializeField]
		private Button _toMainMenuButton;
		[SerializeField]
		private Button _toSolitaireButton;

		private void Awake()
		{
			_toMainMenuButton.onClick.AddListener(OnToMainMenuButton);
			_toSolitaireButton.onClick.AddListener(OnToSolitaireButton);
		}

		private void OnToMainMenuButton()
		{
			SceneManager.LoadScene(SceneIndexes.MainMenuSceneIndex);
		}

		private void OnToSolitaireButton()
		{
			SceneManager.LoadScene(SceneIndexes.SolitaireSceneIndex);
		}
	}
}