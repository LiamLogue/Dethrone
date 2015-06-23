////////////////////////////////
/// File   : AbilityController.cs
/// Author : Liam Logue
/// Desc   : Controls the players abilities
////////////////////////////////
using UnityEngine;
using System.Collections;

public class AbilityController : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public float Speed;
	public int Damage;
	public float TimeToLive;
	public float RotationSpeed;
	public float DamageModifier = 0.0f;
	
	/// 
	/// Variables
	/// 
	Vector3 mousePos;
	Vector3 movementVector;
	float lastSpeed;
	GameObject playerObject;
	float timer;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Get damage modifier
		playerObject = GameObject.FindGameObjectWithTag("Player");
		DamageModifier = playerObject.gameObject.GetComponent<Player>().DamageModifier;
		Damage += Mathf.RoundToInt(Damage * DamageModifier);
		
		//Grab the inital mouse position 
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		mousePos.z = 0.0f;
		
		//Create movement vector and normalize the result
		movementVector = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
		movementVector.Normalize();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		//Add to the timer
		timer += Time.deltaTime;
		
		//Lived long enough?
		if(timer >= TimeToLive) {
			//Destroy
			Destroy(this.gameObject);
		}
		
		//Add the velocity to the rigidbody
		this.rigidbody2D.velocity = new Vector2(movementVector.x, movementVector.y)  * Speed;
		
		//Should we spin left or right?
		if(movementVector.x < 0) {
			//Spinning left
			rigidbody2D.angularVelocity = -RotationSpeed;
			
		}
		else {
			//Spinning right
			rigidbody2D.angularVelocity = RotationSpeed;
			
		}
	}
	
	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerEnter2D(Collider2D collision) {
		//What did we collide with?
		if(collision.tag == "Enemy") {			
			//Round and take the hit
			collision.GetComponent<Enemy>().TakeDamage(Damage);
			
			//Scatter the weak / Ulti?
			if(this.gameObject.name != "ScatterTheWeak(Clone)" && this.gameObject.name != "GravityFieldImplosion(Clone)") {
				//Destroy GameObject
				Destroy(this.gameObject);
			}				
		}
		
		//What did we collide with?
		if(collision.tag == "LevelBounds") {			
			//Scatter the weak / Ulti?
			if(this.gameObject.name != "ScatterTheWeak(Clone)" && this.gameObject.name != "GravityFieldImplosion(Clone)") {
				//Destroy GameObject
				Destroy(this.gameObject);
			}				
		}
	}
	
	/// <summary>
	/// Raises the collision enter2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter2D(Collision2D collision) {
		//What did we collide with?

	}
}
