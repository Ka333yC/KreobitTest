using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Clicker
{
	public class AnimatedTextPointsPool : MonoBehaviour
	{
		private readonly Queue<AnimatedTextPoints> _freeObjects = new Queue<AnimatedTextPoints>();
		
		[SerializeField]
		private AnimatedTextPoints _textPointPrefab;
		[SerializeField]
		private RectTransform _prefabParent;

		public AnimatedTextPoints Rent()
		{
			var result = _freeObjects.Any() ? _freeObjects.Dequeue() : Create();
			result.gameObject.SetActive(true);
			return result;
		}

		public void Return(AnimatedTextPoints textPoint)
		{
			textPoint.gameObject.SetActive(false);
			textPoint.RestoreDefaultValues();
			_freeObjects.Enqueue(textPoint);
		}

		private AnimatedTextPoints Create()
		{
			return Instantiate(_textPointPrefab, _prefabParent);
		}
	}
}