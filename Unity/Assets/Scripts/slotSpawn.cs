using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class slotSpawn : MonoBehaviour
{

	///
	public GameObject GameManager;

	[Header("Slot")]
	public GameObject Slot;

	[Header("Card")]
	//public GameObject[] cardArray;
	public GameObject card;

	[Header("Draw Shares (Out of 1)")]
	public float sTerrain;
	public float sHazard;


    // Start is called before the first frame update
    void Start()
    {
		GameManager = GameObject.FindGameObjectWithTag("Game Manager");

        spawnSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	void spawnSlot() {

		// Generate a slot
		GameObject newSlot = Instantiate(Slot) as GameObject;

			newSlot.transform.SetParent(this.transform);
			newSlot.transform.localPosition = new Vector3(0,0,0);
			newSlot.GetComponent<RectTransform>().localScale =  new Vector3(1f,1f,1f);

		
		 float randValue = Random.value;

            if(randValue < 0.05)
            {	

               spawnCard(24);
			   newSlot.GetComponent<cardSlot>().hasCard = true;
			 

            } else if(randValue < 0.07) {

				if(GameManager.GetComponent<gameManager>().currentTier == 1) {
			 
				} else {
					spawnCard(27);
					newSlot.GetComponent<cardSlot>().hasCard = true;
			 }

		}

	}


	void spawnCard(int cardID) {

		// Generate random card
		//GameObject newCard = Instantiate(cardArray[cardID]) as GameObject;

		GameObject newCard = Instantiate(card) as GameObject;
		newCard.GetComponent<cardMaker>().makeCard(cardID);

			newCard.transform.SetParent(this.transform);
			newCard.transform.localPosition = new Vector3(0,0,0);
			newCard.GetComponent<RectTransform>().localScale =  new Vector3(1f,1f,1f);

			newCard.GetComponent<cardManager>().canDrag = false;
			newCard.GetComponent<cardManager>().inSlot = true;
			newCard.GetComponent<cardManager>().cardStart = false;

	}

}
