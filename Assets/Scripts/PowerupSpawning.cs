using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawning : MonoBehaviour
{
	public GameObject strengthUp;
	public GameObject speedUp;
	public GameObject healthUp;
	enum Powerups
	{
		Strength,
		Speed,
		Health,
	}

	private List<Powerups> powerups = new List<Powerups>()
	{
		Powerups.Strength,
		Powerups.Speed,
		Powerups.Health,
	};
	
	public static void SpawnPowerup(int powerupType, Transform enemyPos, string enemyName)
	{
		var result = powerupType switch
		{
			1 => Instantiate(GameObject.Find("Player").GetComponent<PowerupSpawning>().strengthUp, enemyPos.position, Quaternion.identity),
			2 => Instantiate(GameObject.Find("Player").GetComponent<PowerupSpawning>().speedUp, enemyPos.position, Quaternion.identity),
			3 => Instantiate(GameObject.Find("Player").GetComponent<PowerupSpawning>().healthUp, enemyPos.position, Quaternion.identity),
		};
		Debug.Log("step2");
	}
}
