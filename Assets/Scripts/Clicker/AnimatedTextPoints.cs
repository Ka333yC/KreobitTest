using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Clicker
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class AnimatedTextPoints : MonoBehaviour
	{
		[SerializeField]
		private float _animationDuration = 0.2f;
		[SerializeField]
		private Vector2 _motionDelta = new Vector2(0, 1);

		private RectTransform _rectTransform;
		private TextMeshProUGUI _text;
		private RectTransform _parentTransform;
		private Camera _mainCamera;
		
		private void Awake()
		{
			_rectTransform = transform as RectTransform;
			_text = GetComponent<TextMeshProUGUI>();
			_parentTransform = transform.parent as RectTransform;
			_mainCamera = Camera.main;
		}

		public void RestoreDefaultValues()
		{
			var color = _text.color;
			color.a = 1;
			_text.color = color;
			_rectTransform.localPosition = Vector3.zero;
		}

		public void SetPoints(int pointsCount)
		{
			_text.text = $"+{pointsCount.ToString()}";
		}

		public async UniTask MoveWithFade(Vector2 screenPoint)
		{
			var fadeAnimation = _text.DOFade(0, _animationDuration);
			var motionAnimation = AnimateMotion(screenPoint);
			await UniTask.WhenAll(fadeAnimation.ToUniTask(), motionAnimation.ToUniTask());
		}

		private Tween AnimateMotion(Vector2 screenPoint)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentTransform, screenPoint, Camera.main, 
				out var rectTransformPosition);
			_rectTransform.localPosition = rectTransformPosition;
			var positionAnimation =
				transform.DOLocalMove(rectTransformPosition + _motionDelta, _animationDuration);
			return positionAnimation;
		}
	}
}