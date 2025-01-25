using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class MainMenuPage : MonoBehaviour
	{
		[SerializeField]
		private Button _toClicker;
		[SerializeField]
		private Button _toSolitaire;
		
		private void Awake()
		{
			_toClicker.onClick.AddListener(OnToClickerButton);
			_toSolitaire.onClick.AddListener(OnToSolitaireButton);
		}

		private void OnToClickerButton()
		{
			SceneManager.LoadScene(SceneIndexes.ClickerSceneIndex);
		}

		private void OnToSolitaireButton()
		{
			SceneManager.LoadScene(SceneIndexes.SolitaireSceneIndex);
		}
	}
}