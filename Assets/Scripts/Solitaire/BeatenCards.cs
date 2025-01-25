using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class BeatenCards : MonoBehaviour
	{
		private readonly LinkedList<Card> _beatenCards = new LinkedList<Card>();

		[SerializeField]
		private Vector2 _cardsShift = new Vector2(0.01f, 0.01f);
		
		public int CardsCount => _beatenCards.Count;
		public Card TopCard => _beatenCards.Last.Value;

		public bool Any() => CardsCount > 0;

		public Vector3 GetNextCardPositionInBeaten()
		{
			var cardPositionInBeaten = transform.position + (Vector3)(_beatenCards.Count * _cardsShift);
			return cardPositionInBeaten;
		}
		
		public void PushCard(Card card)
		{
			card.Order = _beatenCards.Count + 1;
			_beatenCards.AddLast(card);
		}

		public Card PopCard()
		{
			var card = _beatenCards.Last.Value;
			_beatenCards.RemoveLast();
			return card;
		}
	}
}