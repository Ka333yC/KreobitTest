using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
	public class CardDeck : MonoBehaviour, IEnumerable<Card>
	{
		private readonly LinkedList<Card> _cardInDeck = new LinkedList<Card>();

		[SerializeField]
		private Vector2 _cardsShift = new Vector2(0.01f, 0.01f);

		public event Action OnClick;
		
		public int CardsCount => _cardInDeck.Count;
		public Card TopCard => _cardInDeck.Last.Value;

		public bool Any() => CardsCount > 0;

		public void Click()
		{
			OnClick?.Invoke();
		}

		public void PushCard(Card card)
		{
			card.Order = _cardInDeck.Count + 1;
			_cardInDeck.AddLast(card);
		}

		public Card PopCard()
		{
			var card = _cardInDeck.Last.Value;
			_cardInDeck.RemoveLast();
			return card;
		}

		public Vector3 GetNextCardPositionInDeck()
		{
			Vector3 cardPositionInDeck =  transform.position + (Vector3)(_cardInDeck.Count * _cardsShift);
			return cardPositionInDeck;
		}

		public LinkedList<Card>.Enumerator GetEnumerator()
		{
			return _cardInDeck.GetEnumerator();
		}

		IEnumerator<Card> IEnumerable<Card>.GetEnumerator()
		{
			return _cardInDeck.GetEnumerator();
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}