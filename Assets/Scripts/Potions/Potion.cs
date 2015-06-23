////////////////////////////////
/// File   : Potions.cs
/// Author : Liam Logue
/// Desc   : A base for each 
///		     potion avaiable to
///          the player
////////////////////////////////
using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public enum Type{Health, DoubleDamage, ReturnDamage, Shield, Ultimate};
	public Type PotionType;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
	
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
	
	}
}
