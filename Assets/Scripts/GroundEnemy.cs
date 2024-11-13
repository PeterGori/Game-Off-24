using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent (typeof (Controller2D))]
public class GroundEnemy : MonoBehaviour {

	public float maxJumpHeight;
	public float minJumpHeight;
	public float timeToJumpApex;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public float moveSpeed;
    public int maxHealth;
	public float currentHealth;

	public EnemyHealthBar healthBar;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	void Start() 
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
	}

	void Update() 
	{
		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
	
	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");
			Die();
		}
		healthBar.SetHealth(currentHealth);
	}
	
	public void Heal (int heal)
	{
		currentHealth += heal;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthBar.SetHealth(currentHealth);
	}
	
	void Die()
	{
		Destroy(gameObject);
	}
}
