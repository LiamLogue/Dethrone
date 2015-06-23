////////////////////////////////
/// File   : ZombieGeneralBuff.cs
/// Author : Liam Logue
/// Desc   : KBuffs Basic Zombies
////         inside the radius
////////////////////////////////
using UnityEngine;
using System.Collections;

public class ZombieGeneralBuff : MonoBehaviour {
	/// 
	/// Public Variables
	/// 
	public float BuffAmount;

	/// 
	/// Variables
	/// 
	int lastDamage;
	
	/// <summary>
	/// Raises the trigger stay2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerStay2D(Collider2D collision) {
		//Right one?
		if(collision.gameObject.name == "Radius Indicator Basic Zombie") {
			//Buffs!		
			if(!collision.transform.parent.gameObject.GetComponent<Enemy>().HasBuff) {
				//Set GO
				collision.transform.parent.gameObject.GetComponent<Enemy>().zombieGeneralGivingBuff = this.transform.parent.gameObject;
				
				//Modify damage
				lastDamage = collision.transform.parent.gameObject.GetComponent<Enemy>().Damage;
				collision.transform.parent.gameObject.GetComponent<Enemy>().DamageModifier = BuffAmount;
				collision.transform.parent.gameObject.GetComponent<Enemy>().HasBuff = true;
				collision.transform.parent.gameObject.GetComponent<Enemy>().Damage = Mathf.RoundToInt(lastDamage * BuffAmount);
				
				//Change color to show buff
				collision.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);

			}
		}
	}
	
	
	/// <summary>
	/// Raises the trigger stay2D event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnTriggerExit2D(Collider2D collision) {
		//Right one?
		if(collision.gameObject.name == "Radius Indicator Basic Zombie") {
			//Buffs!		
			if(collision.transform.parent.gameObject.GetComponent<Enemy>().HasBuff) {
				//Reset
				collision.transform.parent.gameObject.GetComponent<Enemy>().Damage = lastDamage;
				collision.transform.parent.gameObject.GetComponent<Enemy>().DamageModifier = 0.0f;
				collision.transform.parent.gameObject.GetComponent<Enemy>().HasBuff = false;
				
				//Change color to show buff
				collision.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
				
			}
		}
	}
}
