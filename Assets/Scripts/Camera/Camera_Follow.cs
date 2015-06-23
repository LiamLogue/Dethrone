////////////////////////////////
/// File   : Camera_Follow.cs
/// Author : Liam Logue
/// Desc   : Creates a smooth 
///          following camera
////////////////////////////////
using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public Transform Target;
	public float Speed = 0.1f;
	
	/// 
	/// Private Variables
	///
	private Camera myCam;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Initialize mycam
		myCam  = GetComponent<Camera>();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Calculate orthographic size of camera
		myCam.orthographicSize = (Screen.height / 100f) / 3.5f;
		
		//Have we got a target?
		if(Target) {
			//Smoothly move camera to target using lerp
			transform.position = Vector3.Lerp(transform.position, Target.position, Speed) + new Vector3(0, 0, -10);
		}
	}
}
