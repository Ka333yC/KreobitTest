using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
	public class Card : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _forwardFace;
		[SerializeField]
		private SpriteRenderer _backFace;
		[SerializeField]
		private float _animationDuration = 0.5f;
		
		private CardState _state = CardState.Close;
		private int _order;

		public CardSuitValue SuitValue { get; private set; }
		public CardState State => _state;
		public int Order
		{
			get => _order;
			set
			{
				_order = value;
				SetZPositionFitOrder(_order);
			}
		}

		private void Awake()
		{
			transform.rotation = Quaternion.Euler(GetRotationByFace(_state));
		}

		public void Initialize(CardSuitValue suitValue, Sprite forwardFace, Sprite backFace)
		{
			SuitValue = suitValue;
			_forwardFace.sprite = forwardFace;
			_backFace.sprite = backFace;
		}

		public UniTask Flip()
		{
			var newState = _state switch
			{
				CardState.Open => CardState.Close,
				CardState.Close => CardState.Open,
				_ => throw new ArgumentException()
			};

			_state = newState;
			return transform.DORotate(GetRotationByFace(newState), _animationDuration).ToUniTask();
		}
		
		public async UniTask MoveToPosition(Vector3 cardEndPosition)
		{
			SetZPositionFitOrder(100);
			cardEndPosition.z = transform.position.z;
			await transform.DOMove(cardEndPosition, _animationDuration).ToUniTask();
			SetZPositionFitOrder(_order);
		}

		private void SetZPositionFitOrder(int order)
		{
			// Необходимо не допустить графических багов, поэтому, устанавливаем очередь отрисовки через z координату
			var newPosition = transform.position;
			newPosition.z = -order * SolitaireConstantData.GapBetweenCards;
			transform.position = newPosition;
		}

		private Vector3 GetRotationByFace(CardState facePosition)
		{
			return facePosition switch
			{
				CardState.Open => new Vector3(0, 0, 0),
				CardState.Close => new Vector3(0, 180, 0),
				_ => throw new ArgumentException()
			};
		}
	}
}