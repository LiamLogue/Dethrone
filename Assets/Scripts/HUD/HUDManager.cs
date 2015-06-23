////////////////////////////////
/// File   : HUDManager.cs
/// Author : Liam Logue
/// Desc   : Passes updates  
///          about GameObjects 
///          to the HUD. 
////////////////////////////////
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public GameObject PlayerObject;
	public GameObject ShieldOverlay;
	public GameObject ReturnDamageOverlay;
	public GameObject AchievementOverlay;
	
	/// 
	/// Variables
	/// 
	GameObject statsChild;
	GameObject abilitiesChild;
	GameObject inventoryChild;
	GameObject playerHealthText;
	GameObject achievementObject;
	
	float gameTime;
	float achievementTimer = 6f;
	
	string achievementTitle;
	string achievementText;
	
	bool hasKillAchievement = false;
	bool hasPotionAchievement = false;
	bool hasShieldAchievement = false;
	bool achievementShowing = false;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Loop children to find "Stats" GameObject
		foreach (Transform child in transform)
		{
			//Right GameObject?
			if(child.gameObject.name == "Stats") {
				//Store it for later
				statsChild = child.gameObject;
				
			}
			if(child.gameObject.name == "Ability Bar") {
				//Store it for later
				abilitiesChild = child.gameObject;
				
			}
			if(child.gameObject.name == "Inventory (Potions)") {
				//Store it for later
				inventoryChild = child.gameObject;
				
			}
			if(child.gameObject.name == "AchievementBox") {
				//Store it for later
				achievementObject = child.gameObject;
				
			}
		}
		
		//Get GameObjects
		playerHealthText = GameObject.FindGameObjectWithTag("PlayerHealthText");
		
}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Update Game time
		gameTime += Time.deltaTime;
		
		//Loop children to find "Stats" GameObject
		foreach (Transform child in statsChild.transform)
		{
			//Check each child and update their text
			if(child.gameObject.name == "EnemiesKilled") {
				child.gameObject.GetComponent<Text>().text = "ENEMIES KILLED : " + PlayerObject.GetComponent<Player>().EnemiesKilled.ToString();				
			}
			if(child.gameObject.name == "TotalExperience") {
				child.gameObject.GetComponent<Text>().text = "TOTAL EXP : " + PlayerObject.GetComponent<Player>().ExperienceTotal.ToString() + " / " + PlayerObject.GetComponent<Player>().ExperiencePerLevel.ToString();				
			}
			if(child.gameObject.name == "Level") {
				child.gameObject.GetComponent<Text>().text = "CHARACTER LEVEL : " + PlayerObject.GetComponent<Player>().CharacterLevel.ToString();				
			}
			if(child.gameObject.name == "Time") {
				child.gameObject.GetComponent<Text>().text = "TIME : " + Mathf.RoundToInt(gameTime).ToString() + " secs";			
			}
			if(child.gameObject.name == "DamageModifier") {
				child.gameObject.GetComponent<Text>().text = "DAMAGE MOD : " + PlayerObject.GetComponent<Player>().DamageModifier.ToString() + "X";
			}
		}
		
		//Loop children to find "inventory" GameObject
		foreach (Transform child in inventoryChild.transform)
		{
			//Check each child and update their text
			if(child.gameObject.name == "HealthPotionsText") {
				child.gameObject.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().HealthPotions.ToString() + " LEFT";				
			}
			if(child.gameObject.name == "ShieldPotionsText") {
				child.gameObject.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().ShieldPotions.ToString() + " LEFT";				
			}
			if(child.gameObject.name == "DoubleDamagePotionsText") {
				child.gameObject.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().DoubleDamagePotions.ToString() + " LEFT";				
			}
			if(child.gameObject.name == "ReturnDamagePotionsText") {
				child.gameObject.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().ReturnDamagePotions.ToString() + " LEFT";				
			}
			if(child.gameObject.name == "UltimatePotionText") {
				child.gameObject.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().UltimatePotions.ToString() + " LEFT";				
			}
		}
		
		//Got achievement?
		if(!hasKillAchievement) {
			if(PlayerObject.GetComponent<Player>().EnemiesKilled == 10) {
				//Update title / text
				achievementTitle = "Killing Spree".ToUpper();
				achievementText = "Kill 5 enemies +50XP".ToUpper();
				
				//Update showing
				achievementShowing = true;
				
				//Give XP
				PlayerObject.GetComponent<Player>().GainExp(50);
				
				//Set bool
				hasKillAchievement = true;
			}
		}
		if(!hasPotionAchievement) {
			if(PlayerObject.GetComponent<Player>().healthPotionsUsed == 5) {
				//Update title / text
				achievementTitle = "The Healer".ToUpper();
				achievementText = "Use 5 health potions +50XP".ToUpper();
				
				//Update showing
				achievementShowing = true;
				
				//Give XP
				PlayerObject.GetComponent<Player>().GainExp(50);
				
				//Set bool
				hasPotionAchievement = true;
			}
		}
		if(!hasShieldAchievement) {
			if(PlayerObject.GetComponent<Player>().shieldPotionsUsed == 5) {
				//Update title / text
				achievementTitle = "Unbreakable Shield".ToUpper();
				achievementText = "Use 5 shield potions +50XP".ToUpper();
				
				//Update showing
				achievementShowing = true;
				
				//Give XP
				PlayerObject.GetComponent<Player>().GainExp(50);
				
				//Set bool
				hasShieldAchievement = true;
			}
		}		
		
		//Achievement showing?
		if(achievementShowing) {			
			//Loop children to find "achievement" GameObject
			foreach (Transform child in achievementObject.transform)
			{
				//Check each child and update their text
				if(child.gameObject.name == "Title") {
					child.gameObject.GetComponent<Text>().text = achievementTitle;				
				}
				if(child.gameObject.name == "Text") {
					child.gameObject.GetComponent<Text>().text = achievementText;				
				}
				
				AchievementOverlay.transform.gameObject.SetActive(true); 
			}
			
			//Timer
			achievementTimer -= Time.deltaTime;
			
			// < 0?
			if(achievementTimer  <= 0) {
				AchievementOverlay.transform.gameObject.SetActive(false); 
				achievementShowing = false;
				achievementTimer = 3f;                    
			}
		}
		
		//Update player health
		if(PlayerObject.GetComponent<Player>().Health >= 75) {
			playerHealthText.GetComponent<Text>().color = new Color(0f, 255f, 0f);
		}
		if(PlayerObject.GetComponent<Player>().Health < 75) {
			playerHealthText.GetComponent<Text>().color = new Color(255f, 255f, 0f);
		}
		if(PlayerObject.GetComponent<Player>().Health < 50) {
			playerHealthText.GetComponent<Text>().color = new Color(255f, 181f, 0f);
		}
		if(PlayerObject.GetComponent<Player>().Health < 25) {
			playerHealthText.GetComponent<Text>().color = new Color(255f, 0f, 0f);
		}
		
		//Show shield?
		if(PlayerObject.GetComponent<Player>().HasShield == true) {
			ShieldOverlay.transform.gameObject.SetActive(true);
		}
		else {
			ShieldOverlay.transform.gameObject.SetActive(false);
		}
		//Show Return Damage
		if(PlayerObject.GetComponent<Player>().HasReturnDamage == true) {
			ReturnDamageOverlay.transform.gameObject.SetActive(true);
		}
		else {
			ReturnDamageOverlay.transform.gameObject.SetActive(false);
		}
			
		//Update health
		playerHealthText.GetComponent<Text>().text = PlayerObject.GetComponent<Player>().Health.ToString() + " / 100";
	}
}
