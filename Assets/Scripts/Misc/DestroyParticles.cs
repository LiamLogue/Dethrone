////////////////////////////////
/// File   : DestroyParticles.cs
/// Author : Liam Logue
/// Desc   : Kills off a particle
////         after X seconds
////////////////////////////////
using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {
	/// 
	/// Private Variables
	///
	private ParticleSystem attachedParticleSystem;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Get the particle system
		attachedParticleSystem = GetComponent<ParticleSystem>();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Are we playing?
		if(attachedParticleSystem.isPlaying) {
			//All good
			return;
		}
		
		//Done playing?
		Destroy(this.gameObject);
	}
}
