////////////////////////////////
/// File   : Ability.cs
/// Author : Liam Logue
/// Desc   : A base for each 
///		     ability that the
///          player can use
////////////////////////////////
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityCooldownController : MonoBehaviour {
	/// 
	/// Public Variables
	///
	public float cooldown;
	public bool isOnCooldown;
	public string key;

	/// 
	/// Variables
	///	
	GameObject abilityImage;
	GameObject abilityText;
	float cooldownTimer;
	float cooldownRemaining;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		//Populate GameObject
		abilityImage = GameObject.FindGameObjectWithTag("Ability" + key);
		abilityText = GameObject.FindGameObjectWithTag("CooldownText" + key);
		
		//Cooldownremaining
		cooldownRemaining = cooldown;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {		
		//Are we on cooldown?
		if(isOnCooldown) {			
			//Add to the timer
			cooldownTimer += Time.deltaTime;
			
			//Record cooldown remaining
			cooldownRemaining -= Time.deltaTime;
			
			//Check timer
			if(cooldownTimer >= cooldown) {
				//Reset timer and remove cooldown
				cooldownTimer = 0f;
				cooldownRemaining = cooldown;
				isOnCooldown = false;
									
			}
		}
		
		//Change HUD
		if(isOnCooldown) {
			//Update ability image
			abilityImage.transform.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
			
			//Show cooldown text
			abilityText.GetComponent<Text>().text = Mathf.RoundToInt(cooldownRemaining).ToString();
			
		}
		else {
			//Update ability image
			abilityImage.transform.gameObject.GetComponent<CanvasRenderer>().SetAlpha(255);
			
			//Hide cooldown text
			abilityText.GetComponent<Text>().text = "";	
			
		}
	}
}
