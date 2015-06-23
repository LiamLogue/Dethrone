////////////////////////////////
/// File   : MainMenu.cs
/// Author : Liam Logue
/// Desc   : Allows us to pause and 
///          quit the game.
////////////////////////////////
using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public bool isPaused;
	public GameObject pauseMenuCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Paused?
		if(isPaused) {
			//yep
			pauseMenuCanvas.SetActive(true);
			
			//Set game time
			Time.timeScale = 0.0f;
		}
		else {
			//nope
			pauseMenuCanvas.SetActive(false);
			
			//Set game time
			Time.timeScale = 1.0f;
		}
		
		//ESC to pause
		if(Input.GetKeyDown(KeyCode.Escape)) {
			//Pause / unpause
			isPaused = !isPaused;
		}
	}
	
	/// <summary>
	/// Resume game.
	/// </summary>
	public void Resume() {
		//Change isPaused
		isPaused = false;
	}
	
	/// <summary>
	/// Quits the game.
	/// </summary>
	public void QuitGame() {
		//Quit
		Application.Quit();
	}
}
