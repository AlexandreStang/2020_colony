using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class card : ScriptableObject
{

	public new string name;
	public string description;
	public Sprite illustration;
	public type type;
	public int points;

	 [Range(1,3)]
	public int tier;

	[Header("Special Card")]
	public bool special;
	public int specialId;
	public string specialEffectDescription;

}
