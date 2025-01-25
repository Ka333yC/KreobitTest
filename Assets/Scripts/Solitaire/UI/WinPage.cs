using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
	public class WinPage : MonoBehaviour
	{
		[SerializeField]
		private Button _restartButton;
		[SerializeField]
		private Button _toMainMenuButton;
		
		private void Awake()
		{
			_restartButton.onClick.AddListener(OnRestartButton);
			_toMainMenuButton.onClick.AddListener(OnToMainMenuButton);
		}

		private void OnRestartButton()
		{
			SceneManager.LoadScene(SceneIndexes.SolitaireSceneIndex);
		}

		private void OnToMainMenuButton()
		{
			SceneManager.LoadScene(SceneIndexes.MainMenuSceneIndex);
		}
	}
}