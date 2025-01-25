using System;
using System.Buffers;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(Camera))]
	public class SolitaireEventsInvoker : MonoBehaviour
	{
		private readonly RaycastHit[] _raycastHits = new RaycastHit[128];
		private Camera _camera;
		
		private void Awake()
		{
			_camera = GetComponent<Camera>();
		}

		private void Update()
		{
			if(Input.GetMouseButtonUp(0))
			{
				InvokeEvents(Input.mousePosition);
			}
		}

		private void InvokeEvents(Vector3 pointerPosition)
		{
			var ray = _camera.ScreenPointToRay(pointerPosition);
			var hitsCount = Physics.RaycastNonAlloc(ray, _raycastHits);
			for(int i = 0; i < hitsCount; i++)
			{
				var hittedTransform = _raycastHits[i].transform;
				var cardContainer = hittedTransform.GetComponent<CardContainer>();
				if(cardContainer != null)
				{
					cardContainer.Click();
					continue;
				}
				
				var cardDeck = hittedTransform.GetComponent<CardDeck>();
				if(cardDeck != null)
				{
					cardDeck.Click();
				}
			}
		}
	}
}