using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// Crosshairs
	/// </summary>
	public Transform Crosshair1;
	public GameObject Bullet;
	public float FireCooldown;
	public int Lives;
	private float currentFireCooldown;
	
	void Start ()
	{
		// Hide mouse :3
		Screen.showCursor = false;
		
		currentFireCooldown = FireCooldown;
	}
	
	void Update ()
	{
		currentFireCooldown -= Time.deltaTime;
		
		if (Crosshair1 != null) {
			
			RaycastHit hit;
			Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit) && hit.transform.name == "Ground") {
				Crosshair1.localPosition = new Vector3 (hit.point.x, hit.point.y, -2);
			}
			
			if (currentFireCooldown <= 0) {
				currentFireCooldown = FireCooldown;	
				
				// Instanciate a bullet
				if (Bullet != null) {
					
					GameObject bullet = (GameObject)GameObject.Instantiate (Bullet);
					
					BulletScript bulletScript = (BulletScript)bullet.GetComponent ("BulletScript");
					
					// Compute direction
					Vector3 direction = (Crosshair1.localPosition - this.transform.localPosition);
					direction.z = 0;
					direction.Normalize ();
					bulletScript.Direction = new Vector3 (direction.x, direction.y, direction.z);
					
					// Rotate to this direction
					double rotation = Math.Atan( Crosshair1.localPosition.y / Crosshair1.localPosition.x );
					// To degrees
					float angle = (float)(rotation *(180/Math.PI));
					
					bulletScript.transform.Rotate(0,0,angle);
					
					// Ignore player collider
					Physics.IgnoreCollision(bullet.collider, collider);
				}
			}
			
		}
	}
	
	void OnCollisionEnter (Collision collision)
	{
		
		if (collision.gameObject.tag == "Enemy") {
			// An enemy has reached the player!
			// Lose a life
			Lives--;
			
			// enemy is destroyed
			Destroy (collision.gameObject);
			
			Debug.Log ("Player lives remaining " + Lives);
		}
	}

}
