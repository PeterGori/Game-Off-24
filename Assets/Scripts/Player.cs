using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight;
	public float minJumpHeight;
	public float timeToJumpApex;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public float moveSpeed;
    public int maxHealth;
	public int currentHealth;

	public PlayerHealthBar healthBar;

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
		if (Input.GetKeyDown(KeyCode.E))
		{
			TakeDamage(20);
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			Heal(20);
		}

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) 
		{
			velocity.y = maxJumpVelocity;
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (velocity.y > minJumpVelocity)
			{
				velocity.y = minJumpVelocity;
			}
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
	
	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");
		}
		healthBar.SetHealth(currentHealth);
	}
	
	void Heal (int heal)
	{
		currentHealth += heal;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthBar.SetHealth(currentHealth);
	}
}
