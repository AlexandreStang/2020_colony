using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Type", menuName = "Type")]
public class type : ScriptableObject
{

	public new string name;
	public Sprite icon;
	public Color color;

	public type[] gains;
	public type[] losses;

}
