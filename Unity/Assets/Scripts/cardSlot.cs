using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    
	// - - - - - - - - - - VARIABLES - - - - - - - - - -

	[Header("Game Manager")]
	public GameObject GameManager;

	[Header("Game Objects")]
	public GameObject slotHover;
	public GameObject cardParent;
	private GameObject currentCard;

	///
	private int pointsPrediction;

	[Header("States")]
	public bool hasCard;

	[Header("Raycasts")]
	public Transform[] castPoints;

	[Header("Colors")]
	public Color gain;
	public Color loss;
	public Color neutral;





	// - - - - - - - - - - AWAKE - - - - - - - - - -

	public void Awake() {
		hasCard = false;
	}




	// - - - - - - - - - - START - - - - - - - - - -

	public void Start() {


		GameManager = GameObject.FindGameObjectWithTag("Game Manager");

		// Hide the hover highlight
		slotHover.SetActive(false);
		

		// Reset the card variables
		
	}




		// - - - - - - - - - - SLOT IS BEING HOVERED - - - - - - - - - -

	public void OnPointerEnter(PointerEventData eventData) {
	//Debug.Log("PointerEnter");


		// If a card is being dragged at the moment...
		if(eventData.pointerDrag != null && !hasCard) {

		
			// If the card can be dragged around
			if(eventData.pointerDrag.GetComponent<cardManager>().canDrag) {

				// Highlight the slot
				slotHover.SetActive(true);

				// Add the points of the card to the prediction base
				pointsPrediction =  eventData.pointerDrag.GetComponent<cardManager>().card.points;

				DetectCard(0, eventData.pointerDrag);
				DetectCard(1, eventData.pointerDrag);
				DetectCard(2, eventData.pointerDrag);
				DetectCard(3, eventData.pointerDrag);
					
				GameManager.GetComponent<gameManager>().setPointsPrediction(pointsPrediction);

			}

		} 

	}




		// - - - - - - - - - - SLOT IS NOT BEING HOVERED ANYMORE - - - - - - - - - -

	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("PointerExit");


		// Cancel everything
		slotHover.SetActive(false);


		// Remove the points prediction
		GameManager.GetComponent<gameManager>().removePointsPrediction();
		pointsPrediction = 0;


		// Locate all the cards and remove the overlays
		GameObject[] cards ;
             cards = GameObject.FindGameObjectsWithTag("Card");
             foreach(GameObject card in cards) {
                 card.GetComponent<cardManager>().cardOverlay.SetActive(false);
             }

	}




	// - - - - - - - - - - CARD IS DROPPED INTO THE SLOT - - - - - - - - - -

  public void OnDrop(PointerEventData eventData) {
  	  //Debug.Log("OnDrop");

	  currentCard = eventData.pointerDrag;


	  if(currentCard.GetComponent<cardManager>().canDrag) {
		

		// Spawn a new card to replace the dropped card
	    currentCard.transform.parent.GetComponent<cardInventory>().SpawnCard();


		// Set the card as the slot's child
		currentCard.transform.SetParent(this.transform);


		// If the player holds a card and said card can be dragged, drop it
		if(currentCard != null && currentCard.GetComponent<cardManager>().canDrag) {


			//eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
			currentCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
		

			// Point Incrementation
			GameManager.GetComponent<gameManager>().currentPoints = GameManager.GetComponent<gameManager>().currentPoints + pointsPrediction; 

			GameManager.GetComponent<gameManager>().currentScore = GameManager.GetComponent<gameManager>().currentScore + pointsPrediction;

			GameManager.GetComponent<gameManager>().scoreText.text = "SCORE " + GameManager.GetComponent<gameManager>().currentScore;

			// Set the points at 0 if in the negative
			if (GameManager.GetComponent<gameManager>().currentPoints < 0) {
				GameManager.GetComponent<gameManager>().currentPoints = 0;
			}


			// Remove ability to drag card
			currentCard.GetComponent<CanvasGroup>().blocksRaycasts = true;
			currentCard.GetComponent<cardManager>().canDrag = false;
			currentCard.GetComponent<cardManager>().inSlot = true;
			hasCard = true;

		}

	}

	// Locate all slots and check if game over
		var allReady = true;
		GameObject[] slots ;
             slots = GameObject.FindGameObjectsWithTag("Slot");
             foreach(GameObject slot in slots) {
                 if(slot.GetComponent<cardSlot>().hasCard == false) {
					allReady = false;
				 }
             }

		if(allReady) {
			GameManager.GetComponent<gameManager>().finishGame();
		}


}




// - - - - - - - - - - DETECT NEIGHBORING CARDS - - - - - - - - - -

public void DetectCard(int i, GameObject currentCard) {
	

	// Add + points symbol on top of the selected card
	currentCard.GetComponent<cardManager>().cardOverlay.GetComponent<Image>().color = gain;
			currentCard.GetComponent<cardManager>().cardOverlay.SetActive(true);
			currentCard.GetComponent<cardManager>().futurePoints.text = "+" + currentCard.GetComponent<cardManager>().card.points;


	// Send the raycast
	RaycastHit2D hit = Physics2D.Raycast(castPoints[i].position, castPoints[i].position * 0, 1 << LayerMask.NameToLayer("UI"));


	// Check for colliding cards
	if(hit.collider != null) {


		// Check if the cards are in a slot
		if(hit.collider.GetComponent<cardManager>().inSlot) {

			
			// Default card overlay
			activateOverlay(hit.collider, neutral, "+", 0);


			// Check for gains
             foreach(type type in currentCard.GetComponent<cardManager>().card.type.gains) {


				// Check if the neighboring cards are a gain                 
				 if(type == hit.collider.GetComponent<cardManager>().card.type) {

					 activateOverlay(hit.collider, gain, "+", hit.collider.GetComponent<cardManager>().card.points);
					 pointsPrediction += hit.collider.GetComponent<cardManager>().card.points;

				 }
             }


			 // Check for losses
             foreach(type type in currentCard.GetComponent<cardManager>().card.type.losses) {


				// Check if the neighboring cards are a loss                
				 if(type == hit.collider.GetComponent<cardManager>().card.type) {

					  activateOverlay(hit.collider, loss, "-", hit.collider.GetComponent<cardManager>().card.points);
					  pointsPrediction -= hit.collider.GetComponent<cardManager>().card.points;

				 }

             }

		} 

	} 

}


public void activateOverlay(Collider2D card, Color color, string symbol, int points) {

	card.GetComponent<cardManager>().cardOverlay.GetComponent<Image>().color = color;
	card.GetComponent<cardManager>().cardOverlay.SetActive(true);
	card.GetComponent<cardManager>().futurePoints.text = symbol + points;

}

}
