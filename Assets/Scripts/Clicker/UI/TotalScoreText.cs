using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Clicker
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TotalScoreText : MonoBehaviour
	{
		private TextMeshProUGUI _text;

		private void Awake()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		public void SetScore(int score)
		{
			_text.text = $"Total score: {score.ToString()}";
		}
	}
}