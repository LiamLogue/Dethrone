////////////////////////////////
/// File   : BringParticlesForward.cs
/// Author : Liam Logue
/// Desc   : Sets the layer name
///          and order for Particle
///          effects.
////////////////////////////////
using UnityEngine;
using System.Collections;

public class BringForwardParticles : MonoBehaviour {
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		// Set the sorting layer of the particle system.
		this.gameObject.particleSystem.renderer.sortingLayerName = "foreground";
		this.gameObject.particleSystem.renderer.sortingOrder = 102;
	}
}