using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
	// Все значения карт представлены простыми числами для того, чтобы была возможность выполнять операцию % над ними
	// и определять масть и старшинство карты
	[Serializable]
	public enum CardSuit
	{
		Diamonds = 2, // Бубна
		Hearts = 3, // Черви
		Clubs = 5, // Крести
		Spades = 7, // Пики
	}

	[Serializable]
	public enum CardValue
	{
		Ace = 11,
		Two = 13,
		Three = 17,
		Four = 19,
		Five = 23,
		Six = 29,
		Seven = 31,
		Eight = 37,
		Nine = 41,
		Ten = 43,
		Jack = 47,
		Queen = 53,
		King = 59,
	}

	[Serializable]
	public enum CardSuitValue
	{
		// Diamonds
		DiamondsAce = CardValue.Ace * CardSuit.Diamonds,
		DiamondsTwo = CardValue.Two * CardSuit.Diamonds,
		DiamondsThree = CardValue.Three * CardSuit.Diamonds,
		DiamondsFour = CardValue.Four * CardSuit.Diamonds,
		DiamondsFive = CardValue.Five * CardSuit.Diamonds,
		DiamondsSix = CardValue.Six * CardSuit.Diamonds,
		DiamondsSeven = CardValue.Seven * CardSuit.Diamonds,
		DiamondsEight = CardValue.Eight * CardSuit.Diamonds,
		DiamondsNine = CardValue.Nine * CardSuit.Diamonds,
		DiamondsTen = CardValue.Ten * CardSuit.Diamonds,
		DiamondsJack = CardValue.Jack * CardSuit.Diamonds,
		DiamondsQueen = CardValue.Queen * CardSuit.Diamonds,
		DiamondsKing = CardValue.King * CardSuit.Diamonds,

		// Hearts
		HeartsAce = CardValue.Ace * CardSuit.Hearts,
		HeartsTwo = CardValue.Two * CardSuit.Hearts,
		HeartsThree = CardValue.Three * CardSuit.Hearts,
		HeartsFour = CardValue.Four * CardSuit.Hearts,
		HeartsFive = CardValue.Five * CardSuit.Hearts,
		HeartsSix = CardValue.Six * CardSuit.Hearts,
		HeartsSeven = CardValue.Seven * CardSuit.Hearts,
		HeartsEight = CardValue.Eight * CardSuit.Hearts,
		HeartsNine = CardValue.Nine * CardSuit.Hearts,
		HeartsTen = CardValue.Ten * CardSuit.Hearts,
		HeartsJack = CardValue.Jack * CardSuit.Hearts,
		HeartsQueen = CardValue.Queen * CardSuit.Hearts,
		HeartsKing = CardValue.King * CardSuit.Hearts,

		// Clubs
		ClubsAce = CardValue.Ace * CardSuit.Clubs,
		ClubsTwo = CardValue.Two * CardSuit.Clubs,
		ClubsThree = CardValue.Three * CardSuit.Clubs,
		ClubsFour = CardValue.Four * CardSuit.Clubs,
		ClubsFive = CardValue.Five * CardSuit.Clubs,
		ClubsSix = CardValue.Six * CardSuit.Clubs,
		ClubsSeven = CardValue.Seven * CardSuit.Clubs,
		ClubsEight = CardValue.Eight * CardSuit.Clubs,
		ClubsNine = CardValue.Nine * CardSuit.Clubs,
		ClubsTen = CardValue.Ten * CardSuit.Clubs,
		ClubsJack = CardValue.Jack * CardSuit.Clubs,
		ClubsQueen = CardValue.Queen * CardSuit.Clubs,
		ClubsKing = CardValue.King * CardSuit.Clubs,

		// Spades
		SpadesAce = CardValue.Ace * CardSuit.Spades,
		SpadesTwo = CardValue.Two * CardSuit.Spades,
		SpadesThree = CardValue.Three * CardSuit.Spades,
		SpadesFour = CardValue.Four * CardSuit.Spades,
		SpadesFive = CardValue.Five * CardSuit.Spades,
		SpadesSix = CardValue.Six * CardSuit.Spades,
		SpadesSeven = CardValue.Seven * CardSuit.Spades,
		SpadesEight = CardValue.Eight * CardSuit.Spades,
		SpadesNine = CardValue.Nine * CardSuit.Spades,
		SpadesTen = CardValue.Ten * CardSuit.Spades,
		SpadesJack = CardValue.Jack * CardSuit.Spades,
		SpadesQueen = CardValue.Queen * CardSuit.Spades,
		SpadesKing = CardValue.King * CardSuit.Spades,
	}
	
	public static class CardSuitValueExtensions
	{
		public static CardSuit GetSuit(this CardSuitValue suitValue)
		{
			foreach(var cardSuit in SolitaireConstantData.CardSuits)
			{
				if((int)suitValue % (int)cardSuit == 0)
				{
					return cardSuit;
				}
			}
			
			throw new ArgumentException("Invalid CardSuitValue");
		}
		
		public static CardValue GetValue(this CardSuitValue suitValue)
		{
			foreach(var cardValue in SolitaireConstantData.CardValues)
			{
				if((int)suitValue % (int)cardValue == 0)
				{
					return cardValue;
				}
			}
			
			throw new ArgumentException("Invalid CardSuitValue");
		}
	}
}