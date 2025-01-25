using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "CardFactory", menuName = "Solitaire/Card factory")]
	public class CardFactory : ScriptableObject
	{
		[SerializeField]
		private Card _cardPrefab;
		[SerializeField]
		private Sprite _cardBackFaceSprite;
		[SerializeField]
		private CardDataSprite[] _cardForwardFaceSprites;

		public Card CreateCard(CardSuitValue suitValue)
		{
			var card = Instantiate(_cardPrefab);
			var cardForwardFaceSprite = GetCardForwardFaceSprite(suitValue);
			card.Initialize(suitValue, cardForwardFaceSprite, _cardBackFaceSprite);
			return card;
		}
		
#if UNITY_EDITOR
		public void SetCardForwardFaceSprites(CardDataSprite[] sprites)
		{
			// for(int i = 0; i < sprites.Length; i++)
			// {
			// 	_cardForwardFaceSprites[i].SuitValue = sprites[i].SuitValue;
			// }
			
			_cardForwardFaceSprites = sprites;
		}
#endif
		
		private Sprite GetCardForwardFaceSprite(CardSuitValue cardData)
		{
			var result = _cardForwardFaceSprites
				.First(cardSprite => cardSprite.SuitValue == cardData);
			return result.Sprite;
		}
	}
	
#if UNITY_EDITOR
	[CustomEditor(typeof(CardFactory))]
	public class CardFactoryButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			var cardFactory = (CardFactory)target;
			if(!GUILayout.Button("Fill sprites with card suits and values"))
			{
				return;
			}

			Undo.RecordObject(cardFactory, "Sprites with card suits and values filled");
			cardFactory.SetCardForwardFaceSprites(CreateArray());
			EditorUtility.SetDirty(cardFactory);
		}

		private CardDataSprite[] CreateArray()
		{
			var allSuitValues = SolitaireConstantData.CardSuitValues;
			var cardForwardFaceSprites = new CardDataSprite[allSuitValues.Count];
			for(int i = 0; i < allSuitValues.Count; i++)
			{
				var cardDataSprite = new CardDataSprite();
				cardDataSprite.SuitValue = allSuitValues[i];
				cardForwardFaceSprites[i] = cardDataSprite;
			}
			
			return cardForwardFaceSprites;
		}
	}
#endif
}