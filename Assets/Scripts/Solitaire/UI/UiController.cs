using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
	public class UiController : MonoBehaviour
	{
		[SerializeField]
		private SolitaireController _solitaireController;
		[SerializeField]
		private GameActivePage _gameActivePage;
		[SerializeField]
		private WinPage _winPage;
		[SerializeField]
		private LosePage _losePage;

		private void Awake()
		{
			_gameActivePage.gameObject.SetActive(true);
			_winPage.gameObject.SetActive(false);
			_losePage.gameObject.SetActive(false);
			
			_solitaireController.OnGameStateChanged += OnGameStateChanged;
		}

		private void OnGameStateChanged(GameState state)
		{
			_gameActivePage.gameObject.SetActive(false);
			if(state == GameState.Win)
			{
				_winPage.gameObject.SetActive(true);
			}
			else if(state == GameState.Lose)
			{
				_losePage.gameObject.SetActive(true);
			}
			else
			{
				throw new ArgumentException();
			}
		}
	}
}