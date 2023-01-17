using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardInventory : MonoBehaviour
{

    // - - - - - - - - - - VARIABLES - - - - - - - - - -

	[Header("Game Manager")]
    public GameObject GameManager;

	[Header("Cards")]
	public GameObject card;
    private int randomCardID;

	[Header("Draw Shares (Out of 1)")]
	public float sFood;
	public float sHealth;
	public float sHousing;
	public float sPower;
	public float sIndustry;
	public float sResearch;
	public float sExploration;
	public float sDefense;
	public float sSpecial;

	[Header("Chances of getting previous tier cards (Out of 1)")]
	public float tier1InTier2;
	public float tier1InTier3;
	public float tier2InTier3;


    // - - - - - - - - - - START - - - - - - - - - -

    void Start()
    {
		SpawnCard();
    }




        // - - - - - - - - - - SPAWN A RANDOM CARD - - - - - - - - - -

    public void SpawnCard() {

       //randomCardID = Random.Range(0, 9);

		 // Get a card depending on the chance percentage
            float randValue = Random.value;

            if(randValue < sFood)
            {

			   verifyTier(0, 8, 16);

            } else if(randValue < (sFood + sHealth))
            {
               verifyTier(1, 9, 17);

            } else if(randValue < (sFood + sHealth + sHousing))
            {
                verifyTier(2, 10, 18);
            }
            else if (randValue < (sFood + sHealth + sHousing + sPower))
            {
                verifyTier(3, 11, 19);
            }
            else if (randValue < (sFood + sHealth + sHousing + sPower + sIndustry))
            {
                verifyTier(4, 12, 20);
            }
            else if (randValue < (sFood + sHealth + sHousing + sPower + sIndustry + sResearch))
            {
                verifyTier(5, 13, 21);
            }
            else if (randValue < (sFood + sHealth + sHousing + sPower + sIndustry + sResearch + sExploration))
            {
                verifyTier(6, 14, 22);
            }
            else if (randValue < (sFood + sHealth + sHousing + sPower + sIndustry + sResearch + sExploration + sDefense))
            {
                verifyTier(7, 15, 23);
            }
			else if (randValue < (sFood + sHealth + sHousing + sPower + sIndustry + sResearch + sExploration + sDefense + sSpecial)) 
			{
				//verifyTier(7, 15, 23);
			}


  

		// Generate the chosen card
		GameObject newCard = Instantiate(card) as GameObject;
		newCard.GetComponent<cardMaker>().makeCard(randomCardID);

		// Place the newly generated card
		newCard.transform.SetParent(this.transform);
		newCard.transform.localPosition = new Vector3(0,0,0);
		newCard.GetComponent<RectTransform>().localScale =  new Vector3(1f,1f,1f);

		
	}

	public void verifyTier(int cardTier1, int cardTier2, int cardTier3) {

		 if(cardTier1 == -1 || cardTier2 == -1 || cardTier3 == -1) {
		 	 SpawnCard();
		 } else if(GameManager.GetComponent<gameManager>().currentTier == 1) {

					randomCardID = cardTier1;

				} else if(GameManager.GetComponent<gameManager>().currentTier == 2) {

					float randValue = Random.value;

					if(randValue < tier1InTier2) {
						randomCardID = cardTier1;
					} else {
						randomCardID = cardTier2;
					}
					
				} else if(GameManager.GetComponent<gameManager>().currentTier == 3) {
					
					float randValue = Random.value;

					if(randValue < tier1InTier3) {
						randomCardID = cardTier1;
					} else if(randValue < (tier1InTier3 + tier2InTier3)) {
						randomCardID = cardTier2;
					} else {
						randomCardID = cardTier3;
					}
					
				}

	}



}
