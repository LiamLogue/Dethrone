////////////////////////////////
/// File   : Enemy.cs
/// Author : Liam Logue
/// Desc   : Core feature of 
///          enemy movement, 
///          interactions 
///          and combat.
////////////////////////////////
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public int Health;
	public float Speed;
	public int Damage;
	public float DamageModifier = 0.0f;
	public float AttackCooldown;
	public float ChaseRadius;
	public float WanderRange = 0.5f;
	public int ExperienceForKill;
	public bool HasBuff = false;
	public GameObject ClickedPoint;
	public GameObject PlayerObject;
	public GameObject DeathParticles;
	public GameObject HitParticles;
	public GameObject AcidSpit;
	public GameObject PlasmaLaserStars;
	public GameObject BoneToss;	
	public GameObject HealthPotion;
	public GameObject DoubleDamagePotion;
	public GameObject ReturnDamagePotion;
	public GameObject ShieldPotion;
	public GameObject UltimatePotion;	
	public AudioClip DeathSound;
	public AudioClip SkellikingRebuild;
	public float Droprate;
	public float DroprateHealth;
	public float DroprateDoubleDamage;
	public float DroprateReturnDamage;
	public float DroprateShield;
	public float DroprateUltimate;
	public Texture HealthForeground;
	public Texture HealthBackground;
	public Texture HealthFrame;
	
	[HideInInspector]
	public GameObject abilityGameObject;
	[HideInInspector]
	public GameObject zombieGeneralGivingBuff;
	
	/// 
	/// Private Variables
	/// 
	private Animator anim;
	private Rigidbody2D rBody;
	private Vector2 destinationVector;
	private Vector3 startingPos;
	private Vector3 newPOI;
	
	/// 
	/// Variables
	/// 
	float timer = 0f;
	float subDroprate;
	float cooldownTimer;
	float oldSpeed;
	float oldRange;
	int oldDamage;
	bool inGravityField = false;
	float skellikingPassiveCooldown = 45f;
	bool skellikingPassiveAvailable = true;
	
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Grab animator from zombie
		anim = GetComponent<Animator>();
		rBody = GetComponent<Rigidbody2D>();
		
		//Store last speed
		oldSpeed = Speed;
		oldRange = WanderRange;
		oldDamage = Damage;
		
		//Get the radius of the indicator and use it for detection
		foreach (Transform child in transform)
		{
			//Radius indicator
			if(child.gameObject.name == "Radius Indicator") {
				//Scale the ChaseRadius based off the size of the Radius Indicator
				ChaseRadius = child.gameObject.renderer.bounds.extents.x;
				
			}
			//Radius indicator
			if(child.gameObject.name == "Radius Indicator Basic Zombie") {
				//Scale the ChaseRadius based off the size of the Radius Indicator
				ChaseRadius = child.gameObject.renderer.bounds.extents.x;
				
			}
			//Radius indicator
			if(child.gameObject.name == "Radius Indicator Zombie General") {
				//Scale the ChaseRadius based off the size of the Radius Indicator
				ChaseRadius = child.gameObject.renderer.bounds.extents.x;
				
			}
		}	
		
		//Set starting position
		startingPos = this.transform.position;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//In gravity field?
		if(inGravityField) {
			//Check if it's still alive (unity bug)
			if(GameObject.FindGameObjectWithTag("GravityFieldImplosion(Clone)") == null) {
				//Rest stats
				Speed = oldSpeed;
				Damage = oldDamage;
				WanderRange = oldRange;
				
				inGravityField = false;
			}
		}
		
		//Check if it's still alive (unity bug)		
		if(zombieGeneralGivingBuff == null) {
			//Right enemy?
			if(this.gameObject.name == "BasicZombie") {
				//Rest stats
				Damage = oldDamage;
				DamageModifier = 0.0f;
				HasBuff = false;
				this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0 ,0 ,0);
			}
		}
		
		//Skelliking got passive?
		if(skellikingPassiveAvailable == false) {
			//Cooldown left?
			skellikingPassiveCooldown -= Time.deltaTime;
			
			//Reset?
			if(skellikingPassiveCooldown <= 0f) {
				//Give it back!
				skellikingPassiveAvailable = true;
				skellikingPassiveCooldown = 45;
			}
		}
		
		//Check the players position
		if(PlayerObject.GetComponent<Player>().isAlive == true) {
			if(Vector2.Distance(PlayerObject.transform.position, this.transform.position) <= ChaseRadius) {			
				//Add to the timer
				timer += Time.deltaTime;
				cooldownTimer += Time.deltaTime;
				
				//Can I attack
				if(cooldownTimer >= AttackCooldown) {
					//Which enemy am I?
					if(this.gameObject.name == "BasicZombie") {
						//Attack him!
						abilityGameObject = Instantiate(AcidSpit, this.transform.position, Quaternion.identity) as GameObject;
						abilityGameObject.transform.parent = this.transform;
					}
					if(this.gameObject.name == "ZombieGeneral") {
						//Go towards the player and attack
						destinationVector = new Vector2(PlayerObject.transform.position.x - this.transform.position.x, PlayerObject.transform.position.y - this.transform.position.y);
						destinationVector.Normalize();
					}
					if(this.gameObject.name == "Destructobot" ) {
						//Go towards the player and attack
						destinationVector = new Vector2(PlayerObject.transform.position.x - this.transform.position.x, PlayerObject.transform.position.y - this.transform.position.y);
						destinationVector.Normalize();
						
						//Stop moving and target the player				
						abilityGameObject = Instantiate(PlasmaLaserStars, this.transform.position, Quaternion.identity) as GameObject;
						abilityGameObject.transform.parent = this.transform;
				
					}
					if(this.gameObject.name == "Skelliking") {
						//Bone toss
						abilityGameObject = Instantiate(BoneToss, this.transform.position, Quaternion.identity) as GameObject;
						abilityGameObject.transform.parent = this.transform;
						
					}
					
					//Reset timer	
					cooldownTimer = 0f;
				}
				
			}
			else {
				//Are we at home?
				if(V3Equal(this.transform.position, startingPos)) {	
					//Store a new POI			
					newPOI = GeneratePOI();
					
				}
				
				//Have we reached our new POI?
				if(V3Equal(this.transform.position, newPOI)) {
					//Return home
					newPOI = startingPos;
					
				}
				
				//Create and normalize a destination vector
				destinationVector = new Vector2(newPOI.x - this.transform.position.x, newPOI.y - this.transform.position.y);
				destinationVector.Normalize();
				
			}
		}
		
		//Are we moving?
		if(destinationVector != Vector2.zero) {
			//We sure are
			anim.SetBool("Is_Walking", true);
			anim.SetFloat("Direction_X", destinationVector.x);
			anim.SetFloat("Direction_Y", destinationVector.y);			
			
			//Move the RididBody2D to our newly calculated position
			rBody.MovePosition(rBody.position + (destinationVector * Speed) * Time.deltaTime);
		}
		else {
			//Nope
			anim.SetBool("Is_Walking", false);
			
		}
	}
	
	/// <summary>
	/// Raises the trigger enter 2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Player") {
			//Only for zombie general (he's melee)
			if(this.gameObject.name == "ZombieGeneral") {
				//Does the player have return damage? D:
				if(collision.gameObject.GetComponent<Player>().HasReturnDamage) {
					//Stop hitting yourself..
					Health -= Damage;
					collision.gameObject.GetComponent<Player>().HasReturnDamage = false;
				}
				else {
					//Oh hurt em
					collision.gameObject.GetComponent<Player>().TakeDamage(Damage);
				}
			}
		}
	}
	
	/// <summary>
	/// Raises the trigger stay2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerEnter2D(Collider2D collision) {
		//Gravity Field?
		if(collision.gameObject.name == "GravityFieldImplosion(Clone)") {		
			//In gravity field
			inGravityField = true;
			
			Speed = 0.0f;
			WanderRange = 0.0f;
			Damage = 0;
		}
	}
	
	/// <summary>
	/// Raises the collision stay 2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionStay2D(Collision2D collision)  {
		//Set the new POI 
		newPOI = GeneratePOI();
		
	}	

		/// <summary>
	/// Take damage.
	/// </summary>
	/// <param name="pDamage">Damage to take.</param>
	public void TakeDamage(int pDamage) {
		//Show splatter
		Instantiate(HitParticles, this.transform.position, Quaternion.identity);
						
		//Take damage
		Health -= pDamage;

		//Are we dead?
		if(Health <= 0) {
			//Skelliking passive?
			if(this.gameObject.name == "Skelliking") {
				if(skellikingPassiveAvailable) {
					//We are ded :( (OR ARE WE?!?! :D)
					Instantiate(DeathParticles, this.transform.position, Quaternion.identity);
					
					//WE LIED!!
					AudioSource.PlayClipAtPoint(SkellikingRebuild, this.transform.position, 2.0f);
					
					//Rebuild me!
					Health = 100;
					skellikingPassiveAvailable = false;
					return;
				}
			}
			
			//Audio modifier. Loud for big bad boss.
			if(!(this.gameObject.name == "Skelliking")) {
				AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, 0.4f);
			}
			else {
				AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, 2.0f);
		
			}
			
			//We are ded :(
			Instantiate(DeathParticles, this.transform.position, Quaternion.identity);

			//Drop anything?
			if (Random.Range(0f, 1f) <= Droprate) {
				//Drop depending on enemy type
				if(this.gameObject.name == "BasicZombie") {
					//Drop a health potion
					Instantiate(HealthPotion, this.transform.position, Quaternion.identity);
				}
				if(this.gameObject.name == "ZombieGeneral") {
					//Decide which to drop
					float toDrop = Random.Range(0f, 1f);
					
					if(toDrop < DroprateDoubleDamage) {
						//Drop a DD potion
						Instantiate(DoubleDamagePotion, this.transform.position, Quaternion.identity);
					}
					else if(toDrop > DroprateReturnDamage) {
						//Drop a RD potion
						Instantiate(ReturnDamagePotion, this.transform.position, Quaternion.identity);
					}
				}
				if(this.gameObject.name == "Destructobot") {
					//Decide which to drop
					float toDrop = Random.Range(0f, 1f);
					
					if(toDrop < DroprateShield) {
						//Drop a shield potion
						Instantiate(ShieldPotion, this.transform.position, Quaternion.identity);
					}
					else if(toDrop > DroprateUltimate) {
						//Drop an ulti potion
						Instantiate(UltimatePotion, this.transform.position, Quaternion.identity);
					}
				}
				if(this.gameObject.name == "Skelliking") {
					//Decide which to drop
					float toDrop = Random.Range(0f, 1f);
					
					if(toDrop < DroprateShield) {
						//Drop a shield potion
						Instantiate(ShieldPotion, this.transform.position, Quaternion.identity);
					}
					else if(toDrop > DroprateUltimate) {
						//Drop an ulti potion
						Instantiate(UltimatePotion, this.transform.position, Quaternion.identity);
					}
				}
			
			}
			
			//Destroy
			Destroy(this.gameObject);
			
			//Aware the killer
			PlayerObject.GetComponent<Player>().GainExp(ExperienceForKill);
			PlayerObject.GetComponent<Player>().EnemiesKilled++;
		}	
	}
	
	/// <summary>
	/// Checks if two Vector3 positions are equal.
	/// </summary>
	/// <param name="a">The first Vector3.</param>
	/// <param name="b">The second Vector3.</param>
	public bool V3Equal(Vector3 a, Vector3 b){
		//Check if the vectors are "close enough" and return
		return Vector3.SqrMagnitude(a - b) < 0.0001;
	}
	
	/// <summary>
	/// Generates and returns a new POI.
	/// </summary>
	/// <returns>The POI.</returns>
	public Vector2 GeneratePOI() {
		//Return a new position
		return new Vector2(Random.Range(startingPos.x - WanderRange, startingPos.x + WanderRange), Random.Range (startingPos.y - WanderRange, startingPos.y + WanderRange));
		
	}
	
	
	/// <summary>
	/// Raises the GUI event.
	/// </summary>
	void OnGUI() {
		//Target to draw our GUI
		Vector2 targetPos = Camera.main.WorldToScreenPoint(this.transform.position);
		targetPos.y = Screen.height - (targetPos.y + 40);
		targetPos.x = targetPos.x - 50;
	
		//Draw the health bar
		GUI.DrawTexture(new Rect(targetPos.x, targetPos.y, 100, 10), HealthBackground, ScaleMode.StretchToFill, true, 1.0f);
		GUI.DrawTexture(new Rect(targetPos.x, targetPos.y, Health, 10), HealthForeground, ScaleMode.StretchToFill, true, 1.0f);
		GUI.DrawTexture(new Rect(targetPos.x, targetPos.y , 100, 10), HealthFrame, ScaleMode.StretchToFill, true, 1.0f);
		
	}
}
