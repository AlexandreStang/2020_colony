using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardMaker : MonoBehaviour
{

	public Text cardName;
	public Text cardPoints;
	public Image cardIllustration;
	public Image cardIcon;

	public card[] card;
	public card currentCard;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void makeCard(int cardID) {
		
		// Set text
		cardName.text = card[cardID].name;
		cardPoints.text = "" + card[cardID].points;
		cardPoints.color = card[cardID].type.color;

		// Set illustration & icon
		cardIllustration.GetComponent<Image>().sprite = card[cardID].illustration;
		cardIcon.GetComponent<Image>().sprite = card[cardID].type.icon;
		cardIcon.GetComponent<Image>().color = card[cardID].type.color;

		currentCard = card[cardID];

	}


}
