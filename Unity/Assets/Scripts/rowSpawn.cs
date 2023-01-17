using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class rowSpawn : MonoBehaviour
	
{

	public GameObject GameManager;

	[Header("Row")]
	public GameObject Row;
	

    // Start is called before the first frame update
    void Start()
    {
		GameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

	public void spawnRow(int currentSol) {

		// Generate the row
		GameObject newRow = Instantiate(Row) as GameObject;

			// Deal with the position and the scaling
			newRow.transform.SetParent(this.transform);
			newRow.transform.localPosition = new Vector3(0,0,0);

			newRow.GetComponent<RectTransform>().localScale =  new Vector3(1f,1f,1f);

			newRow.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
			newRow.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
			newRow.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
			newRow.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);

			newRow.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
			newRow.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);

		// Confirm position at the Row Spawner
		newRow.GetComponent<row>().currentPosition = newRow.GetComponent<row>().rowPosition[0];

		// Update the current sol
		newRow.GetComponent<row>().currentSol = currentSol + 2;

	}



}
