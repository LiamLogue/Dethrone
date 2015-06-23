////////////////////////////////
/// File   : HealthPickup.cs
/// Author : Liam Logue
/// Desc   : Triggers when a player
///          walks over the health
///          potion.
////////////////////////////////
using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {	
	/// 
	/// Public Variables
	/// 
	public AudioClip PickupItemSound;
	
	/// 
	/// Private Variables
	/// 

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
	
	/// <summary>
	/// Raises the trigger enter2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerEnter2D(Collider2D collision) {
		//Player walked into it?
		if(collision.gameObject.tag == "Player") {
			//What type of potion			
			if(this.gameObject.name == "Health(Clone)") {
				//Add to potions
				collision.gameObject.GetComponent<Player>().HealthPotions++;
				
				//Play Sound
				AudioSource.PlayClipAtPoint(PickupItemSound, Camera.main.transform.position, 0.4f);
				
			}
			if(this.gameObject.name == "Shield(Clone)") {
				//Add to potions
				collision.gameObject.GetComponent<Player>().ShieldPotions++;
				
				//Play Sound
				AudioSource.PlayClipAtPoint(PickupItemSound, Camera.main.transform.position, 0.4f);
				
			}
			if(this.gameObject.name == "ReturnDamage(Clone)") {
				//Add to potions
				collision.gameObject.GetComponent<Player>().ReturnDamagePotions++;
				
				//Play Sound
				AudioSource.PlayClipAtPoint(PickupItemSound, Camera.main.transform.position, 0.4f);
				
			}
			if(this.gameObject.name == "DoubleDamage(Clone)") {
				//Add to potions
				collision.gameObject.GetComponent<Player>().DoubleDamagePotions++;
				
				
				//Play Sound
				AudioSource.PlayClipAtPoint(PickupItemSound, Camera.main.transform.position, 0.4f);
				
			}
			if(this.gameObject.name == "Ultimate(Clone)") {
				//Add to potions
				collision.gameObject.GetComponent<Player>().UltimatePotions++;
				
				//Play Sound
				AudioSource.PlayClipAtPoint(PickupItemSound, Camera.main.transform.position, 0.4f);
				
			}
			
			//Destroy object
			Destroy(this.gameObject);
				
		}
	}
}
