using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

	/// <summary>
	/// Moving speed
	/// </summary>
	public Vector3 Speed;
	/// <summary>
	/// Remaining lives.
	/// </summary>
	public int Lives;
	
	void Start ()
	{

	}

	void Update ()
	{
		// Move towards the player
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		if (player != null) {
			// Find the direction
			Vector3 direction = (player.transform.localPosition - this.transform.localPosition);
			direction.Normalize ();

			// Add a speed vector
			float xForce = direction.x * (Speed.x * Time.deltaTime);
			float yForce = direction.y * (Speed.y * Time.deltaTime);
			float zForce = direction.z * (Speed.z * Time.deltaTime);
			
			// Move by applying force
			this.rigidbody.AddForce(xForce, yForce, zForce);
		}
	}
	
	void OnCollisionEnter (Collision collision)
	{
		// Bullet hit the enemy?
		if (collision.gameObject.tag == "Bullet") {
			// Get damage count
			BulletScript b = (BulletScript)collision.gameObject.GetComponent ("BulletScript");
			
			Lives -= b.Damage;
			
			// The bullet is destroyed
			Destroy (collision.gameObject);
			
			// Destroy enemy if no more lives
			if (Lives <= 0) {
				Destroy (gameObject);
			}
		}
	}

}
