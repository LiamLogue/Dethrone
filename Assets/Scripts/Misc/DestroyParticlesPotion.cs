////////////////////////////////
/// File   : DestroyParticles.cs
/// Author : Liam Logue
/// Desc   : Kills off a particle
////         after X seconds
////////////////////////////////
using UnityEngine;
using System.Collections;

public class DestroyParticlesPotion : MonoBehaviour {	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Are we playing?
		if(this.gameObject.particleSystem.isPlaying) {
			//All good
			return;
		}
		
		//Done playing?
		Destroy(this.gameObject);
	}
}
