using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Clicker
{
	public class ClickableObject : MonoBehaviour
	{
		[SerializeField]
		private float _animationDuration = 0.1f;
		[SerializeField]
		private Vector3 _shakeStrength = new Vector3(0.01f, 0.01f);
		
		private Tween _shakeAnimation;
		
		public event Action OnClick;

		private void OnMouseUp()
		{
			OnClick?.Invoke();
		}

		public void Shake()
		{
			_shakeAnimation?.Kill();
			var startPosition = transform.position;
			_shakeAnimation = transform
				.DOShakePosition(_animationDuration, _shakeStrength)
				.OnKill(() =>
				{
					transform.position = startPosition;
					_shakeAnimation = null;
				});
		}
	}
}