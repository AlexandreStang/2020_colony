using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class row : MonoBehaviour
{


	[Header("Positions")]
	public GameObject[] rowPosition;
	public GameObject currentPosition;

	[Header("Current Sol")]
	public int currentSol;
	public Text currentSolText;



	void Awake() {
		
             rowPosition[0] = GameObject.FindGameObjectWithTag("Row 01");
			 rowPosition[1] = GameObject.FindGameObjectWithTag("Row 02");
			 rowPosition[2] = GameObject.FindGameObjectWithTag("Row 03");
			 rowPosition[3] = GameObject.FindGameObjectWithTag("Row 04");
			 rowPosition[4] = GameObject.FindGameObjectWithTag("Row 05");

	}


    // Start is called before the first frame update
    void Start()
    {

		// Determine the Sol number
		if(currentSol < 10) {
			currentSolText.text = "00" + currentSol;
		} else if (currentSol < 100) {
			currentSolText.text = "0" + currentSol;
		} else {
			currentSolText.text = "" + currentSol;
		}
   
    }

    // Update is called once per frame
    void Update()
    {
           
    }


	public void moveForward() {

		// Move each row one step forward
		if(currentPosition == rowPosition[0]) {
			
			transform.position = rowPosition[1].transform.position;
			currentPosition = rowPosition[1];

		} else if(currentPosition == rowPosition[1]) {

			transform.position = rowPosition[2].transform.position;
			currentPosition = rowPosition[2];

		} else if(currentPosition == rowPosition[2]) {

			transform.position = rowPosition[3].transform.position;
			currentPosition = rowPosition[3];

		} else if(currentPosition == rowPosition[3]) {

			transform.position = rowPosition[4].transform.position;
			currentPosition = rowPosition[4];

			// Destroy the row once it reaches the last position
			Destroy(gameObject);

		} 

	}

}
