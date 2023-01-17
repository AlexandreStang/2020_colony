using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
	// - - - - - - - - - - VARIABLES - - - - - - - - - -

	[Header("Game Information")]
	public int currentSol;
    public int currentTier;
	public int currentScore;

	[Header("Point Bar")]
	public int currentPoints;
	public int[] pointsNeeded;
	private int pointsPrediction;

	public Image barPoints;

	[Header("Sidebar Information")]
	public Text currentPointsText;
	public Text pointsNeededText;
	public Text currentSolText;
	public Text pointsPredictionText;
	public Text scoreText;

	[Header("Row Spawner")]
	public GameObject rowSpawner;

	[Header("Colors")]
	public Color gain;
	public Color loss;
	public Color neutral;

	[Header("Tier Years")]
	public int tier2;
	public int tier3;

	[Header("Game Over Screen")]
	public GameObject gameOverScreen;
	public Text gameOverText;
	public int finalScore;



    // - - - - - - - - - - START - - - - - - - - - -

    void Awake()
    {	
		pointsPredictionText.text = "";
        currentPoints = 0;

		currentSol = 1;
        currentTier = 1;

		barPoints = barPoints.GetComponent<Image>();

		gameOverScreen.SetActive(false);

    }




    // - - - - - - - - - - UPDATE - - - - - - - - - -

    void Update()
    {

		// Adjust the bar with the current points
		 barPoints.fillAmount = currentPoints/(float)pointsNeeded[currentSol];

		// Change text depending on the current year (Adds 1 year to adjust the time)
			
				currentSolText.text = "YEAR " + currentSol.ToString();
			
		

		// Change text of the Siderbar Counter
        currentPointsText.text = currentPoints.ToString();
		pointsNeededText.text = "/ " + pointsNeeded[currentSol].ToString() + " XP";


		// If the points needed are acquired...
		if(currentPoints >= pointsNeeded[currentSol]) {
			forwardTime();
		}

		// Reset the level < - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - TO REMOVE AT THE END OF THE PROJECT
		if (Input.GetKeyDown("r")) { //If you press R
			 SceneManager.LoadScene("level"); //Load scene called Game
		}


		// Check the tier and change the tier if needed
		if(currentSol == tier2) {
			currentTier = 2;
		} 
		
		//else if (currentSol == tier3) {
		//	currentTier = 3;
		//}

		







	}



	// - - - - - - - - - - MOVE FORWARD IN TIME - - - - - - - - - -

	void forwardTime() {
		
		// Update the year and the points
		currentPoints = currentPoints - pointsNeeded[currentSol];
		currentSol++;

		// Generate a new row
		rowSpawner.GetComponent<rowSpawn>().spawnRow(currentSol);

		// Locate all rows and move them forward
		GameObject[] rows ;
             rows = GameObject.FindGameObjectsWithTag("Row");
             foreach(GameObject row in rows) {
                 row.GetComponent<row>().moveForward();
             }
			
	}




	// - - - - - - - - - - PRESENT THE POTENTIAL POINTS TO BE GAINED/LOST - - - - - - - - - -

	public void setPointsPrediction(int pointsPrediction) {
		
		if (pointsPrediction > 0) {

				pointsPredictionText.text = "+ " + pointsPrediction;
				pointsPredictionText.color = gain;

			} else if (pointsPrediction < 0) {

				pointsPredictionText.text = "- " + (-pointsPrediction);
				pointsPredictionText.color = loss;

			} else {

				pointsPredictionText.text = "+ 0";
				pointsPredictionText.color = neutral;;

			}
		}




	// - - - - - - - - - - CLEAN THE PREDICTION - - - - - - - - - -

	public void removePointsPrediction() {
		pointsPredictionText.text = "";
	}

	// - - - - - - - - - - GAME OVER - - - - - - - - - -

	public void finishGame() {
		Debug.Log("Game Over!");

		gameOverScreen.SetActive(true);

		finalScore = currentSol;

		if(finalScore == 1) {
			gameOverText.text = "It's only been a year and you've already screwed up this up. How?";
		} else {
			gameOverText.text = "Congratulations! Your colony lasted " + finalScore + " years! \n \n Score: " + currentScore;
		}
		

	}

}
