////////////////////////////////
/// File   : Player.cs
/// Author : Liam Logue
/// Desc   : Core feature of 
///          player movement, 
///          interactions 
///          and combat.
////////////////////////////////
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public GameObject CurrentTarget;
	public int Health;
	public int EnemiesKilled;
	public int ExperienceTotal;
	public int ExperiencePerLevel = 100;
	public int CharacterLevel;
	public float Speed;
	public Transform FirePosition;
	public GameObject AbilityUse;
	public GameObject NullSphere;
	public GameObject ScatterTheWeak;
	public GameObject GravityFieldImplosion;
	public GameObject HitParticles;
	public GameObject DeathParticles;
	public Texture HealthForeground;
	public Texture HealthBackground;
	public Texture HealthFrame;
	public GameObject ShieldOverlay;
	public GameObject ReturnDamageOverlay;
	
	[HideInInspector]
	public bool HasShield = false;
	[HideInInspector]
	public bool HasDoubleDamage = false;
	[HideInInspector]
	public bool HasReturnDamage = false;
	[HideInInspector]
	public float shieldTimer = 10.0f;
	[HideInInspector]
	public float doubleDamageTimer = 8.0f;
	[HideInInspector]
	public GameObject ClickedPoint;
	[HideInInspector]
	public float DamageModifier = 0.0f;
	[HideInInspector]
	public AbilityCooldownController AbilityQ;
	[HideInInspector]
	public AbilityCooldownController AbilityW;
	[HideInInspector]
	public AbilityCooldownController AbilityE;
	[HideInInspector]
	public AbilityCooldownController AbilityR;
	[HideInInspector]
	public int qDamage;
	[HideInInspector]
	public int wDamage;
	[HideInInspector]
	public int eDamage;
	[HideInInspector]
	public int rDamage;
	[HideInInspector]
	public int HealthPotions = 0;
	[HideInInspector]
	public int ShieldPotions = 0;
	[HideInInspector]
	public int DoubleDamagePotions = 0;
	[HideInInspector]
	public int ReturnDamagePotions = 0;
	[HideInInspector]
	public int UltimatePotions = 0;
	[HideInInspector]
	public float DivineBlessingTimer = 5.0f;
	[HideInInspector]
	public bool isAlive = true;
	
	/// 
	/// Private Variables
	/// 
	private Animator anim;
	private Rigidbody2D rBody;
	private Vector2 movementVector;
	private Vector3 mousePos;
	private GameObject[] clickedPoints;
	private GameObject abilityImage;
	private float previousDamageModifier;
	private bool HasDivineBlessing = false;
	private float lastSpeed;
	public int healthPotionsUsed = 0;
	public int shieldPotionsUsed = 0;
	private int doubleDamagePotionsUsed = 0;
	private int returnDamagePotionsUsed = 0;
	private int ultimatePotionsUsed = 0;
	private float gameTime;
	private float PrefsExperienceTotal;
	
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Disable overlays
		ShieldOverlay.transform.gameObject.SetActive(false);
		ReturnDamageOverlay.transform.gameObject.SetActive(false);
		
		//Set Character level
		CharacterLevel = 0;
		
		//Grab components from the player
		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody2D>();		
		
		//Add ability components
		AbilityQ = gameObject.AddComponent<AbilityCooldownController>();
		AbilityW = gameObject.AddComponent<AbilityCooldownController>();
		AbilityE = gameObject.AddComponent<AbilityCooldownController>();
		AbilityR = gameObject.AddComponent<AbilityCooldownController>();
		
		//Setup abilities
		AbilityQ.cooldown = 0.5f;
		AbilityQ.key = "Q";
		
		AbilityW.cooldown = 20f;
		AbilityW.key = "W";
		
		AbilityE.cooldown = 10f;
		AbilityE.key = "E";
		
		AbilityR.cooldown = 45f;
		AbilityR.key = "R";
		
		//Reset scores	
		PlayerPrefs.SetInt("healthPotionsUsed", 0);
		PlayerPrefs.SetInt("shieldPotionsUsed", 0);
		PlayerPrefs.SetInt("doubleDamagePotionsUsed", 0);
		PlayerPrefs.SetInt("returnDamagePotionsUsed", 0);
		PlayerPrefs.SetInt("ultimatePotionsUsed", 0);
		PlayerPrefs.SetInt("time", 0);
		PlayerPrefs.SetInt("totalXP", 0);
		PlayerPrefs.SetInt("characterLevel", 0);
		PlayerPrefs.SetInt("enemiesKilled", 0);
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Update Game time
		gameTime += Time.deltaTime;
		
		//Level Up?
		if(ExperienceTotal >= ExperiencePerLevel) {
			//Upgrade base damage
			DamageModifier += 1.2f;
			
			//Reset experience and grant level
			ExperienceTotal = 0;
			CharacterLevel++;
		}
				
		//Do we have a mouse click?
		if(Input.GetMouseButtonDown(1)) {
			//Grab the inital mouse position s
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			mousePos.z = 0.0f;
			
			//Create movement vector and normalize the result
			movementVector = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
			movementVector.Normalize();
			
			//Check for other clicked point objects
			clickedPoints = GameObject.FindGameObjectsWithTag("ClickedPoint");	
			
			//Room for one?
			if(clickedPoints.Length <= 0) {
				//Show the clicked point to the player
				Instantiate(ClickedPoint, new Vector3(mousePos.x, mousePos.y, 0.0f), Quaternion.identity);
			}
			else {
				//Destroy the imposters D:
				foreach(GameObject point in clickedPoints) {
					Destroy(point);
				}
				
				//Show the clicked point to the player
				Instantiate(ClickedPoint, new Vector3(mousePos.x, mousePos.y, 0.0f), Quaternion.identity);
			}		
		}
		else {
			//Cast ray and set current target for targetd spells / enemy click
		}
				
		//Keypresses for abilities
		if(Input.GetKeyDown(KeyCode.Q)) {
			//Set cooldown
			if(!AbilityQ.isOnCooldown) {
				AbilityQ.isOnCooldown = true;
				
				//Show the particles and ability coming from infront of the player
				Instantiate(AbilityUse, new Vector3(FirePosition.position.x, FirePosition.position.y - 0.01f, FirePosition.position.z), FirePosition.rotation);
				Instantiate(NullSphere, FirePosition.position, FirePosition.rotation);	
			}
		}
		if(Input.GetKeyDown(KeyCode.W)) {
			//Set cooldown
			if(!AbilityW.isOnCooldown) {
				//Visual feedback for having buff	
				this.gameObject.GetComponent<SpriteRenderer>().renderer.material.SetColor("_Color", Color.green);
				
				//Play sound
				if(this.gameObject.GetComponent<AudioSource>().isPlaying == false) {
					this.gameObject.GetComponent<AudioSource>().Play();
				}
				
				//Set cooldown
				AbilityW.isOnCooldown = true;
				
				//Speed up the character and heal them
				lastSpeed = Speed;
				Speed += Speed;
				
				//Calculate health to add
				Health += (Mathf.RoundToInt((100 - Health) / 2));
				
				//Have it
				HasDivineBlessing = true;
					
			}
		}	
		if(Input.GetKeyDown(KeyCode.E)) {
			//Set cooldown
			if(!AbilityE.isOnCooldown) {
				AbilityE.isOnCooldown = true;
				
				//Show the particles and ability coming from infront of the player
				Instantiate(AbilityUse, new Vector3(FirePosition.position.x, FirePosition.position.y - 0.01f, FirePosition.position.z), FirePosition.rotation);
				Instantiate(ScatterTheWeak, FirePosition.position, FirePosition.rotation);
			}
		}	
		if(Input.GetKeyDown(KeyCode.R)) {
			//Set cooldown
			if(!AbilityR.isOnCooldown) {
				AbilityR.isOnCooldown = true;
				
				//Store mouse position
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				//Close enough?
				if(Vector3.Distance(mousePos, this.gameObject.transform.position) <= 100.0300f) {
					//Instantiate
					Instantiate(AbilityUse, new Vector3(FirePosition.position.x, FirePosition.position.y - 0.01f, FirePosition.position.z), FirePosition.rotation);
					Instantiate(GravityFieldImplosion, new Vector3(mousePos.x, mousePos.y, 0f), FirePosition.rotation);
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			//Use potion?
			if((HealthPotions != 0) && (Health != 100)) {
				//Give player health
				if(Health + 25 >= 100) {
					//Round to 100
					Health = 100;
				}
				else {
					//Have 25
					Health += 25;
				}
				
				//Take one away
				HealthPotions--;
				
				//Add one to total used
				healthPotionsUsed++;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			//Use potion?
			if((ShieldPotions != 0) && (!HasShield)) {
				//Give player shield
				HasShield = true;
				
				//Take one away
				ShieldPotions--;
				
				//Add one to total used
				shieldPotionsUsed++;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			//Use potion?
			if((ReturnDamagePotions != 0) && (!HasReturnDamage)) {				
				//Give player DD
				HasReturnDamage = true;
				
				//Take one away
				ReturnDamagePotions--;
				
				//Add one to total used
				returnDamagePotionsUsed++;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)) {
			//Use potion?
			if((DoubleDamagePotions != 0) && (!HasDoubleDamage)) {
				//Store previous damage modifier
				previousDamageModifier = DamageModifier;
				
				//Modify base damage
				if(DamageModifier == 0f) {
					DamageModifier = 2.0f;
				}
				else {
					DamageModifier = DamageModifier * 2;
				}
				
				//Give player DD
				HasDoubleDamage = true;
				
				//Take one away
				DoubleDamagePotions--;
				
				//Add one to total used
				doubleDamagePotionsUsed++;
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)) {
			//Use potion?
			if((UltimatePotions != 0)) {				
				//Take one away
				UltimatePotions--;
				
				//Add one to total used
				ultimatePotionsUsed++;
				
				//Remove cooldowns
				AbilityQ.isOnCooldown = false;
				AbilityW.isOnCooldown = false;
				AbilityE.isOnCooldown = false;
				AbilityR.isOnCooldown = false;
				
				//Give potions
				HealthPotions++;
				DoubleDamagePotions++;
				ReturnDamagePotions++;
				ShieldPotions++;
				
				//Full health
				Health = 100;
			}
		}
		
		//Shield
		if(HasShield) {
			shieldTimer -= Time.deltaTime;
				
			//Cooldown done?
			if(shieldTimer <= 0f) {
				//Vulnerable... again
				HasShield = false;
				
				shieldTimer = 10.0f;
			}
		}
		//DoubleDamage
		if(HasDoubleDamage) {
			doubleDamageTimer -= Time.deltaTime;
			
			//Cooldown done?
			if(doubleDamageTimer <= 0f) {
				//Weak... again
				HasDoubleDamage = false;
				DamageModifier = previousDamageModifier;
				
				doubleDamageTimer = 8.0f;
			}
		}
		//Divine Blessing
		if(HasDivineBlessing) {
			//Timer			
			DivineBlessingTimer -= Time.deltaTime;
			
			//Done?
			if(DivineBlessingTimer <= 0f) {
				//Lost the buff
				HasDivineBlessing = false;
				DivineBlessingTimer = 5.0f;
				Speed = lastSpeed;
				
				// Remove visual feedback
				this.gameObject.GetComponent<SpriteRenderer>().renderer.material.SetColor("_Color", Color.white);
			}
		}
		
		
		//Are we moving?
		if(movementVector != Vector2.zero) {
			//We sure are
			anim.SetBool("Is_Walking", true);
			anim.SetFloat("Input_X", movementVector.x);
			anim.SetFloat("Input_Y", movementVector.y);
			
		}
		else {
			//Nope
			anim.SetBool("Is_Walking", false);
			
		}
		
		//Move the RididBody2D to our newly calculated position
		rBody.MovePosition(rigidbody2D.position + (movementVector * Speed) * Time.deltaTime);
		
	}
	
	/// <summary>
	/// Raises the trigger enter 2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerExit2D(Collider2D collision) {		
		//Change the state of the player based off different collisions
		if(collision.gameObject.name == "Clicked Point(Clone)") {
			//Kill the movement vector and set the anim state
			movementVector = Vector2.zero;
			anim.SetBool("Is_Walking", false);
			
			//Destroy the collided point
			Destroy(collision.gameObject);
		}
	}
	
	/// <summary>
	/// Raises the collision enter 2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D collision) {
		//Change the state of the player based off different collisions
		if(collision.gameObject.tag == "LevelBounds") {
			//Player hit the bounds
			movementVector = Vector2.zero;
			anim.SetBool("Is_Walking", false);
			
			//Destroy the clicked point
			Destroy(GameObject.FindGameObjectWithTag("ClickedPoint"));
		}
		
		//Zombie bite?	
		if(collision.gameObject.name == "ZombieGeneral") {
			//Playing?
			if(!(collision.gameObject.GetComponent<AudioSource>().isPlaying)) {
				//Play it, white boy
				collision.gameObject.GetComponent<AudioSource>().Play();	
			}
		} 

	}
	
	/// <summary>
	/// Takes the amount.
	/// </summary>
	/// <param name="amount">Amount of damage to take.</param>
	public void TakeDamage(int pAmount) {
		//Show splatter
		Instantiate(HitParticles, this.transform.position, Quaternion.identity);
		
		//Can we take it (Or do we have a shield / return damage)?
		if((Health >= 0) && (!HasShield)) {
			//Yeah		
			Health -= pAmount;		
		}
		
		//Are we dead?
		if (Health <= 0) {
			//We are dead :(
			//Show splatter
			Instantiate(DeathParticles, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			isAlive = false;
			
			//Store scores	
			PlayerPrefs.SetInt("healthPotionsUsed", healthPotionsUsed);
			PlayerPrefs.SetInt("shieldPotionsUsed", shieldPotionsUsed);
			PlayerPrefs.SetInt("doubleDamagePotionsUsed", doubleDamagePotionsUsed);
			PlayerPrefs.SetInt("returnDamagePotionsUsed", returnDamagePotionsUsed);
			PlayerPrefs.SetInt("ultimatePotionsUsed", ultimatePotionsUsed);
			PlayerPrefs.SetInt("time", Mathf.RoundToInt(gameTime));
			PlayerPrefs.SetInt("totalXP", Mathf.RoundToInt(PrefsExperienceTotal));
			PlayerPrefs.SetInt("characterLevel", CharacterLevel);
			PlayerPrefs.SetInt("enemiesKilled", EnemiesKilled);
			
				
			//Load scene
			Application.LoadLevel("Score");
		}
	}
	
	/// <summary>
	/// Gain experience to level up.
	/// </summary>
	/// <param name="pAmount">Amount to gain.</param>
	public void GainExp(int pAmount) {
		//Add to total experience
		ExperienceTotal += pAmount;
		PrefsExperienceTotal += pAmount;
		

	}
	
	/// <summary>
	/// Raises the GUI event.
	/// </summary>
	void OnGUI() {	
		//Draw the health bar
		GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 60, 200, 10), HealthBackground, ScaleMode.StretchToFill, true, 1.0f);
		GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 60, (Health * 2), 10), HealthForeground, ScaleMode.StretchToFill, true, 1.0f);
		GUI.DrawTexture(new Rect((Screen.width / 2) - 100, Screen.height - 60, 200, 10), HealthFrame, ScaleMode.StretchToFill, true, 1.0f);
		
	}
}