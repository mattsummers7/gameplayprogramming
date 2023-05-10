using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	[SerializeField]
	GameObject projectile;

	[SerializeField]
	Transform Point;

	[SerializeField]
	float turningSpeed = 6;
	
	Transform target;

	float fireRate = 2.5f;

	private void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		fireRate -= Time.deltaTime;

		Vector3 direction = transform.position - target.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turningSpeed * Time.deltaTime);
		
		if(fireRate <= 0 )
		{
			fireRate = 2.5f;
			Shoot();
		}

	}

	void Shoot()
	{
		Instantiate(projectile, Point.position, Point.rotation);
	}
	
    
    
  
}
