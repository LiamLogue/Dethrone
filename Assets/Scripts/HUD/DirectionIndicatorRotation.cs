////////////////////////////////
/// File   : DirectionIndicatorRotation.cs
/// Author : Liam Logue
/// Desc   : Allows the direction 
///          indicator to rotate 
///          towards the mouse
///			 direction. 
////////////////////////////////
using UnityEngine;
using System.Collections;

public class DirectionIndicatorRotation : MonoBehaviour {
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Start () {
		//Caclulate the dirrence between the transform position and the mouse position 
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		
		//Normalize the vector
		difference.Normalize();
		
		//Calculate the rotation and apply it to the transform
		float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.Euler(0f, 0f, rotation - 90);	}
}
