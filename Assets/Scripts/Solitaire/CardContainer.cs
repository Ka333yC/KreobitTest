using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class CardContainer : MonoBehaviour
	{
		private readonly List<CardContainer> _collidingCardContainers = new List<CardContainer>();

		private bool _isInitialized;
		private Card _card;
		private int _order;

		public event Action<CardContainer> OnClick;
		public event Action<CardContainer> OnCardChanged;

		public int OverlappingCardContainersCount { get; private set; }

		public Card Card
		{
			get => _card;
			set
			{
				_card = value;
				if(_card != null)
				{
					_card.Order = _order;
				}
				
				OnCardChanged?.Invoke(this);
			}
		}

		// TODO: использовать рейкаст вместо OnTrigger ?
		private void OnTriggerEnter(Collider other)
		{
			var cardContainer = other.GetComponent<CardContainer>();
			if(cardContainer == null)
			{
				return;
			}
			
			_collidingCardContainers.Add(cardContainer);
		}

		public void Initialize(int order)
		{
			_order = order;
			_isInitialized = true;
			foreach(var collidingCardContainer in _collidingCardContainers)
			{
				CheckForOverlappingCardContainers(collidingCardContainer);
				collidingCardContainer.CheckForOverlappingCardContainers(this);
			}
		}

		public void Click()
		{
			OnClick?.Invoke(this);
		}

		private void CheckForOverlappingCardContainers(CardContainer cardContainer)
		{
			if(!_isInitialized || !cardContainer._isInitialized || _order > cardContainer._order)
			{
				return;
			}

			OverlappingCardContainersCount++;
			cardContainer.OnCardChanged += OverlappingCardContainerChanged;
		}

		private void OverlappingCardContainerChanged(CardContainer cardContainer)
		{
			if(cardContainer.Card != null)
			{
				return;
			}
			
			OverlappingCardContainersCount--;
			if(OverlappingCardContainersCount == 0)
			{
				Card.Flip();
			}
		}
	}
}