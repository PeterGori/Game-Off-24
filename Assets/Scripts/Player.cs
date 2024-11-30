using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
	public static float maxJumpHeight = 4.2f;
	public float minJumpHeight;
	public static float timeToJumpApex = 0.4f;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public static float moveSpeed = 7.5f;
    public int maxHealth;
	public int currentHealth;
	public static int damage = 20;
	public static float damageModifier = 1;
	private readonly float coyoteTime = 0.35f;
	private float coyoteTimeCounter = 0;
	private readonly float JumpBufferTime = 0.2f;
	private float JumpBufferCounter = 0;
	public float worldSwitch_Y_Value;
	public int goodWorld;  // 1 = goodworld
	public bool PlayerSwitch_active = false;

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
		if (Input.GetKeyDown(KeyCode.LeftAlt) && PlayerSwitch_active == false)
		{
			StartCoroutine(WorldSwitchCoroutine());
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			TakeDamage(20);
		}
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			Heal(20);
		}
		
		if (controller.collisions.below)
		{
			coyoteTimeCounter = coyoteTime;
		}
		else
		{
			coyoteTimeCounter -= Time.deltaTime;
		}

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown(KeyCode.Space))
		{
			JumpBufferCounter = JumpBufferTime;
		}
		else
		{
			JumpBufferCounter -= Time.deltaTime;
		}
		
		if (JumpBufferCounter > 0 && coyoteTimeCounter > 0f) 
		{
			velocity.y = maxJumpVelocity;
			JumpBufferCounter = 0;
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (velocity.y > minJumpVelocity)
			{
				velocity.y = minJumpVelocity;
			}
			coyoteTimeCounter = 0;
		}
		
		// Flip sprite based on direction of travel
		if (velocity.x > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (velocity.x < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
		
	}

	public IEnumerator WorldSwitchCoroutine()
	{
		PlayerSwitch_active = true;
		yield return new WaitForSeconds(0.4f);
		
		if (goodWorld == 1)
		{
			Vector3 newPosition = transform.position;
			newPosition.y += worldSwitch_Y_Value;
			transform.position = newPosition;
			goodWorld = 0;
		}
		else if (goodWorld == 0)
		{
			Vector3 newPosition = transform.position;
			newPosition.y -= worldSwitch_Y_Value;
			transform.position = newPosition;
			goodWorld = 1;
		}
		yield return new WaitForSeconds(1.5f);
		PlayerSwitch_active = false;
	}
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Death();
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
	
	public void Death()
	{
		SceneManager.LoadScene("Main Level");
	}
}
