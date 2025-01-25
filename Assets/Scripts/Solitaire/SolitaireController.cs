using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace DefaultNamespace
{
	public class SolitaireController : MonoBehaviour
	{
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();
		
		[SerializeField]
		private CardContainer[] _cardContainers = Array.Empty<CardContainer>();
		[SerializeField]
		private CardFactory _cardFactory;
		[SerializeField]
		private CardDeck _cardDeck;
		[SerializeField]
		private BeatenCards _beatenCards;
		
		private GameState _state = GameState.Playing;
		private CancellationToken _cancellationToken;
		
		public event Action<GameState> OnGameStateChanged;

		public GameState State
		{
			get => _state;
			set
			{
				_state = value;
				OnGameStateChanged?.Invoke(_state);
			}
		}

		private void Awake()
		{
			_cancellationToken = _cts.Token;
		}

		private void Start()
		{
			StartGame();
		}

		private void OnDestroy()
		{
			_cts.Cancel();
			_cts.Dispose();
		}

		public async UniTaskVoid StartGame()
		{
			await FillDeckWithRandomCards();
			InitializeCardContainers();
			await FillCardContainersWithCards();
			await FlipCardsInUncoveredContainers();

			_cardDeck.OnClick += OnCardDeckClick;
		}

		private async UniTask FillDeckWithRandomCards()
		{
			var cardSuitValues = new List<CardSuitValue>(SolitaireConstantData.CardSuitValues);
			cardSuitValues.Shuffle();
			foreach(var cardSuitValue in cardSuitValues)
			{
				var createdCard = _cardFactory.CreateCard(cardSuitValue);
				createdCard.transform.position = _cardDeck.GetNextCardPositionInDeck();
				_cardDeck.PushCard(createdCard);
				await UniTask.Yield();
				_cancellationToken.ThrowIfCancellationRequested();
			}
		}

		private void InitializeCardContainers()
		{
			for(int i = 0; i < _cardContainers.Length; i++)
			{
				var cardContainer = _cardContainers[i];
				cardContainer.Initialize(i + 1);
				cardContainer.OnClick += CardContainerClick;
			}
		}

		private async UniTask FillCardContainersWithCards()
		{
			foreach(var cardContainer in _cardContainers)
			{
				var card = _cardDeck.PopCard();
				cardContainer.Card = card;
				card.MoveToPosition(cardContainer.transform.position).Forget();
				await UniTask.Yield();
				_cancellationToken.ThrowIfCancellationRequested();
			}
		}

		private async UniTask FlipCardsInUncoveredContainers()
		{
			foreach(var cardContainer in _cardContainers)
			{
				if(cardContainer.OverlappingCardContainersCount == 0)
				{
					cardContainer.Card.Flip();
					await UniTask.Yield();
					_cancellationToken.ThrowIfCancellationRequested();
				}
			}
		}

		private void OnCardDeckClick()
		{
			if(_state != GameState.Playing || !_cardDeck.Any())
			{
				return;
			}

			MoveTopCardFromDeckToBeaten();
		}

		private void MoveTopCardFromDeckToBeaten()
		{
			var card = _cardDeck.PopCard();
			var cardPositionInBeaten = _beatenCards.GetNextCardPositionInBeaten();
			_beatenCards.PushCard(card);
			card.Flip();
			card.MoveToPosition(cardPositionInBeaten);
			CheckForEndGame();
		}

		private void CardContainerClick(CardContainer cardContainer)
		{
			if(_state != GameState.Playing)
			{
				return;
			}
			
			var card = cardContainer.Card;
			if(card == null || 
			   card.State == CardState.Close || 
			   (_beatenCards.Any() && !IsCardsMatch(card, _beatenCards.TopCard)))
			{
				return;
			}

			cardContainer.Card = null;
			var cardPositionInBeaten = _beatenCards.GetNextCardPositionInBeaten();
			_beatenCards.PushCard(card);
			card.MoveToPosition(cardPositionInBeaten);
			CheckForEndGame();
		}

		private bool IsCardsMatch(Card card1, Card card2)
		{
			var card1Value = card1.SuitValue.GetValue();
			var card2Value = card2.SuitValue.GetValue();
			return SolitaireConstantData.NextCardValues[card1Value] == card2Value ||
				SolitaireConstantData.PreviousCardValues[card1Value] == card2Value;
		}

		private void CheckForEndGame()
		{
			if(State != GameState.Playing)
			{
				return;
			}
			
			if(IsPlayerWin())
			{
				State = GameState.Win;
			}
			else if(IsPlayerLose())
			{
				State = GameState.Lose;
			}
		}

		private bool IsPlayerWin()
		{
			foreach(var cardContainer in _cardContainers)
			{
				if(cardContainer.Card != null)
				{
					return false;
				}
			}

			return true;
		}

		private bool IsPlayerLose()
		{
			if(_cardDeck.Any())
			{
				return false;
			}
			
			foreach(var cardContainer in _cardContainers)
			{
				var card = cardContainer.Card;
				if(card != null &&
				   card.State == CardState.Open &&
				   IsCardsMatch(_beatenCards.TopCard, card))
				{
					return false;
				}
			}

			return true;
		}
	}
}