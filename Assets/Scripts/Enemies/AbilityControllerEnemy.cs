////////////////////////////////
/// File   : AbilityControllerEnemy.cs
/// Author : Liam Logue
/// Desc   : Controls the NullPasses updates  
///          sphere GameObject
////////////////////////////////
using UnityEngine;
using System.Collections;

public class AbilityControllerEnemy : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public float Speed;
	public float TimeToLive;
	public float RotationSpeed;
	public float DamageModifier = 0.0f;
	
	/// 
	/// Variables
	/// 
	Vector3 movementVector;
	Vector3 playerPosition;
	GameObject playerObject;
	float timer;
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		//Grab the player GameObject
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerPosition = playerObject.gameObject.transform.position;
		
		//Create movement vector and normalize the result
		movementVector = new Vector2(playerPosition.x - this.transform.position.x, playerPosition.y - this.transform.position.y);
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
		if(collision.tag == "Player") {
			//Does the player have return damage? D:
			if(collision.GetComponent<Player>().HasReturnDamage) {
				//Stop hitting yourself..
				this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().Health -= this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().Damage;
				collision.GetComponent<Player>().HasReturnDamage = false;
			}
			else {
				
				//Oh hurt em
				collision.GetComponent<Player>().TakeDamage(this.gameObject.transform.parent.gameObject.GetComponent<Enemy>().Damage);
			}
			
			//Destroy the game object
			Destroy(this.gameObject);
		}
		
		if(collision.tag == "LevelBounds") {
			//Just destroy it
			Destroy (this.gameObject);
		}
	}
}
