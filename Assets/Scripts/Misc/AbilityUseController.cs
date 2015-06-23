////////////////////////////////
/// File   : AbilityUseController.cs
/// Author : Liam Logue
/// Desc   : Kills off a particle
////         after X seconds
////////////////////////////////
using UnityEngine;
using System.Collections;

public class AbilityUseController : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public float TimeToLive;
	
	/// 
	/// Variables
	/// 
	float timer;
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Add to timer
		timer += Time.deltaTime;
		
		//Are we over the particle time to live?
		if(timer >= TimeToLive) {
			//Destroy
			Destroy(this.gameObject);
		}
	}
}
