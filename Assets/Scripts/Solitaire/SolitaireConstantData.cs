using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
	public static class SolitaireConstantData
	{
		public const int FirstRowSize = 3;
		public const int SecondRowSize = 6;
		public const int ThirdRowSize = 9;
		public const int FourthRowSize = 10;

		public const float GapBetweenCards = 0.01f;
		
		public static readonly IReadOnlyList<CardSuit> CardSuits = (CardSuit[])Enum.GetValues(typeof(CardSuit));
		public static readonly IReadOnlyList<CardValue> CardValues = (CardValue[])Enum.GetValues(typeof(CardValue));
		public static readonly IReadOnlyList<CardSuitValue> CardSuitValues = GetAllSuitValues();
		
		public static readonly IReadOnlyDictionary<CardValue, CardValue> NextCardValues = GetNextCardValues();
		public static readonly IReadOnlyDictionary<CardValue, CardValue> PreviousCardValues = GetPreviousCardValues();

		private static CardSuitValue[] GetAllSuitValues()
		{
			var cardSuitValues = new CardSuitValue[52];
			for(int i = 0; i < CardSuits.Count; i++)
			{
				for(int j = 0; j < CardValues.Count; j++)
				{
					cardSuitValues[i * CardValues.Count + j] = (CardSuitValue)((int)CardSuits[i] * (int)CardValues[j]);
				}
			}
			
			return cardSuitValues;
		}

		private static Dictionary<CardValue, CardValue> GetNextCardValues()
		{
			var dictionary = new Dictionary<CardValue, CardValue>();
			var cardValues = (CardValue[])Enum.GetValues(typeof(CardValue));
			dictionary.Add(CardValue.King, CardValue.Ace);
			for(int i = 0; i < cardValues.Length - 1; i++)
			{
				dictionary.Add(cardValues[i], cardValues[i + 1]);
			}
			
			return dictionary;
		}

		private static Dictionary<CardValue, CardValue> GetPreviousCardValues()
		{
			var dictionary = new Dictionary<CardValue, CardValue>();
			var cardValues = (CardValue[])Enum.GetValues(typeof(CardValue));
			dictionary.Add(CardValue.Ace, CardValue.King);
			for(int i = 0; i < cardValues.Length - 1; i++)
			{
				dictionary.Add(cardValues[i + 1], cardValues[i]);
			}
			
			return dictionary;
		}
	}
}