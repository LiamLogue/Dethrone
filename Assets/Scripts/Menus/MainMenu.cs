////////////////////////////////
/// File   : MainMenu.cs
/// Author : Liam Logue
/// Desc   : Allows us to change 
///          between scenes at
///          the main menu.
////////////////////////////////
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	/// 
	/// Variables
	/// 
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		//Right scene?
		if(Application.loadedLevelName == "Score") {
			//Loop children
			foreach(Transform child in transform) {
				//Which one?
				if(child.gameObject.name == "HealthPotionsUsed"){
					child.gameObject.GetComponent<Text>().text = "HEALTH POTIONS USED : " + PlayerPrefs.GetInt("healthPotionsUsed");
				}
				if(child.gameObject.name == "ShieldPotionsUsed"){
					child.gameObject.GetComponent<Text>().text = "SHIELD POTIONS USED : " + PlayerPrefs.GetInt("shieldPotionsUsed");
				}
				if(child.gameObject.name == "DoubleDamagePotionsUsed"){
					child.gameObject.GetComponent<Text>().text = "DOUBLE DAMAGE POTIONS USED : " + PlayerPrefs.GetInt("doubleDamagePotionsUsed");
				}
				if(child.gameObject.name == "ReturnDamagePotionsUsed"){
					child.gameObject.GetComponent<Text>().text = "RETURN DAMAGE POTIONS USED : " + PlayerPrefs.GetInt("returnDamagePotionsUsed");
				}
				if(child.gameObject.name == "UltimatePotionsUsed"){
					child.gameObject.GetComponent<Text>().text = "ULTIMATE POTIONS USED : " + PlayerPrefs.GetInt("ultimatePotionsUsed");
				}
				if(child.gameObject.name == "TextTime") {
					child.gameObject.GetComponent<Text>().text = "TIME : " + PlayerPrefs.GetInt("time");
				}
				if(child.gameObject.name == "TextTotalExperience") {
					child.gameObject.GetComponent<Text>().text = "TOTAL EXPERIENCE : " + PlayerPrefs.GetInt("totalXP");
				}
				if(child.gameObject.name == "TextCharacterLevel") {
					child.gameObject.GetComponent<Text>().text = "CHARACTER LEVEL : " + PlayerPrefs.GetInt("characterLevel");
				}
				if(child.gameObject.name == "TextEnemiesKilled") {
					child.gameObject.GetComponent<Text>().text = "ENEMIES KILLED : " + PlayerPrefs.GetInt("enemiesKilled");
				}
			}
		}
	}
	
	/// <summary>
	/// New  game.
	/// </summary>
	public void NewGame() {
		//Load new game
		Application.LoadLevel("Main");
	}
	
	/// <summary>
	/// Shows the enemies info.
	/// </summary>
	public void ShowEnemiesInfo() {
		//Load enemy info screen
		Application.LoadLevel ("InfoEnemies");
	}
	
	/// <summary>
	/// Show info.
	/// </summary>
	public void ShowInfo() {
		//Load new game
		Application.LoadLevel("Info");
	}
	
	/// <summary>
	/// Shows the scores.
	/// </summary>
	public void ShowScores() {
		//Show score
		Application.LoadLevel("Score");
	}
	
	/// <summary>
	/// Shows the controls.
	/// </summary>
	public void ShowControls() {
		//Show control screen
		Application.LoadLevel("Controls");
	}
	
	/// <summary>
	/// Quits the game.
	/// </summary>
	public void QuitGame() {	
		//Quit
		Application.Quit();
	}
	
	/// <summary>
	/// Shows the title.
	/// </summary>
	public void ShowTitle() {
		//Show title
		Application.LoadLevel("Title");
	}
}
