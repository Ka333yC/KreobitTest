using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace.Clicker
{
	public class ClickerController : MonoBehaviour
	{
		[SerializeField]
		private ClickableObject _clickableObject;
		[SerializeField]
		private AnimatedTextPointsPool _textPointsPool;
		[SerializeField]
		private TotalScoreText _totalScoreText;

		private ClickerSaveDataFileLoader _saveDataFileLoader;
		private int _totalScore;

		private void Awake()
		{
			_saveDataFileLoader = new ClickerSaveDataFileLoader();
		}

		private async void Start()
		{
			await LoadSaveData();
			_clickableObject.OnClick += OnClick;
		}

		private void OnDestroy()
		{
			_saveDataFileLoader.Write(new ClickerSaveData() { Score = _totalScore });
		}

		private void OnClick()
		{
			_clickableObject.Shake();
			CreatePointsText(1, Input.mousePosition).Forget();
			_totalScore++;
			_totalScoreText.SetScore(_totalScore);
		}

		private async UniTask LoadSaveData()
		{
			var clickerSaveData = await _saveDataFileLoader.Read();
			if(clickerSaveData == null)
			{
				_totalScore = 0;
			}
			else
			{
				_totalScore = clickerSaveData.Score;
			}
			
			_totalScoreText.SetScore(_totalScore);
		}

		private async UniTaskVoid CreatePointsText(int points, Vector2 screenPointer)
		{
			var textPoints = _textPointsPool.Rent();
			textPoints.SetPoints(points);
			await textPoints.MoveWithFade(screenPointer);
			_textPointsPool.Return(textPoints);
		} 
	}
}