using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,  IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

	// - - - - - - - - - - VARIABLES - - - - - - - - - -

	[Header("Card Scriptable Object")]
	public card card;

	[Header("Sidebar Information")]
	public Sprite emptyIcon;
	private Text nameText;
	private Text descriptionText;

	private Text typeText;
	private Image typeIcon;
	private Text pointsText;

	private GameObject[] gains;
	private GameObject[] losses;



	[Header("Canvas")]
	[SerializeField] private Canvas canvas;

	///
	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;

	[Header("Card States")]
	public bool canDrag; 
	public bool inSlot;
	public bool cardStart;
	public static GameObject cardBeingDragged;

	///
	Vector3 startPosition;

	[Header("Card Parts")]
	public GameObject cardOverlay;
	public Text futurePoints;




	// - - - - - - - - - - AWAKE - - - - - - - - - -

	private void Awake() {

		// Set the sidebar informations
		nameText = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().nameText.GetComponent<Text>();
		descriptionText = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().descriptionText.GetComponent<Text>();

		typeText = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().typeText.GetComponent<Text>();
		typeIcon = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().typeIcon.GetComponent<Image>();
		pointsText = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().pointsText.GetComponent<Text>();
		
		gains = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().gains;
		losses = GameObject.FindGameObjectWithTag("Sidebar").GetComponent<sidebar>().losses;
			
		
		
		// Set the Canvas
		canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

		// Reset hovering effects
		stopHovering();
		
		//
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();

		// Reset card states
		canDrag = true;
		inSlot = false;
		cardStart = true;
	}




	// - - - - - - - - - - START - - - - - - - - - -

    void Start()
    {
		card = GetComponent<cardMaker>().currentCard;
    }




	// - - - - - - - - - - UPDATE - - - - - - - - - -

    void Update()
    {

		// Reset the card once it's back at its starting position
		if(transform.position == startPosition) {
			cardStart = true;
			canDrag = true;
		}
		
		// If the card is not neither at its starting position or in a slot, bring it back
		if(!cardStart && !inSlot) {
			 transform.position = Vector3.Lerp(transform.position, startPosition, .3f);
		}
       
    }


		

	// - - - - - - - - - - CARD IS BEING HOVERED - - - - - - - - - -

	public void OnPointerEnter(PointerEventData eventData) {
		startHovering();
	}




	// - - - - - - - - - - CARD IS NOT BEING HOVERED ANYMORE - - - - - - - - - -

	public void OnPointerExit(PointerEventData eventData) {
		stopHovering();
	}




	// - - - - - - - - - - CARD STARTS GETTING DRAGGED - - - - - - - - - -

	public void OnBeginDrag(PointerEventData eventData) {
		//Debug.Log("OnBeginDrag");

		if(canDrag == true) {
			//canvasGroup.alpha = .6f;
			canvasGroup.blocksRaycasts = false;

			cardBeingDragged = gameObject;
			startPosition = transform.position;


		}

	}




	// - - - - - - - - - - CARD IS BEING DRAGGED - - - - - - - - - -

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log("OnDrag");

		if(canDrag == true) {
			rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
			rectTransform.parent.SetAsLastSibling();

			startHovering();
		}

	}




	// - - - - - - - - - - CARD IS NOT BEING DRAGGED ANYMORE - - - - - - - - - -

	public void OnEndDrag(PointerEventData eventData) {
		//Debug.Log("OnEndDrag");

		cardStart = false;

		if(canDrag == true) {
			//transform.position = startPosition;
			canvasGroup.blocksRaycasts = true;

			cardBeingDragged = null;	
			canDrag = false;
		}

		canvasGroup.alpha = 1f;
		
	}


	// When the mouse button is released
	public void OnPointerDown(PointerEventData eventData) {
		//Debug.Log("OnPointerDown");
	}


	public void OnDrop(PointerEventData eventData) {
		throw new System.NotImplementedException();
	}




	// - - - - - - - - - - SIDEBAR TEXT APPEARS - - - - - - - - - -

	public void startHovering() {
		
		// Name & Description & Points
		nameText.text = card.name;
		
		pointsText.text = card.points + " XP";
		//pointsText.color = card.type.color;

		// If Special Card...
		if(card.special) {
			descriptionText.text = card.description + "\n \n" + card.specialEffectDescription;
		} else {
			descriptionText.text = card.description + "\n \n No special ability.";
		}



		// Type Icon
		typeIcon.sprite = card.type.icon;
		typeIcon.color = card.type.color;

		// Adjust the type name (Remove additional text)
		typeText.text = card.type.name + " Card";
		typeText.color = card.type.color;
		

		// Gain & Losses

		 
			for (int i = 0; i < card.type.gains.Length; i++) {
          
				if(card.type.gains[i]) {
				
					// Adjust the name
					//string typeAbb = card.type.gains[i].name + "";
					//string typeAbbClean = typeAbb.Substring(0, 3);
					//[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().text = typeAbbClean + ".";
					//gains[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().color = card.type.gains[i].color;
				
					// Adjust the icon
					gains[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(true);
					gains[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().sprite = card.type.gains[i].icon;
					gains[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().color = card.type.gains[i].color;

				} else  {
					gains[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(false);
				}
			}

			
			for (int i = 0; i < card.type.losses.Length; i++) {
          
				if(card.type.losses[i]) {
				
					// Adjust the name
					//string typeAbb = card.type.losses[i].name + "";
					//string typeAbbClean = typeAbb.Substring(0, 3);
					//losses[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().text = typeAbbClean + ".";
					//losses[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().color = card.type.losses[i].color;
					
					// Adjust the icon
					losses[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(true);
					losses[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().sprite = card.type.losses[i].icon;
					losses[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().color = card.type.losses[i].color;

				} else  {
					losses[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(false);
				}
		   }

		   
		
		

	}


	// - - - - - - - - - - SIDEBAR TEXT DISAPPEARS - - - - - - - - - -

	public void stopHovering() {
		
		// Name & Description
		nameText.text = "";
		descriptionText.text = "";

		// Card Icon + Type + Points
		typeText.text = "";
		typeIcon.color = new Color(0F, 0F, 0F, 0F);
		pointsText.text = "";

		// Gains & Losses
		 for (int i = 0; i < card.type.gains.Length; i++) {
          
			gains[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().text = "";
			gains[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0F);
			gains[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(false);

			losses[i].GetComponent<gainLoss>().nameText.GetComponent<Text>().text = "";
			losses[i].GetComponent<gainLoss>().typeIcon.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0F);
			losses[i].GetComponent<gainLoss>().typeIcon.transform.parent.gameObject.SetActive(false);
			
		   }
	}


}
