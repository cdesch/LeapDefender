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
				Crosshair1.localPosition = new Vector3 (hit.point.x, 2, hit.point.z);
			}
			
			if (currentFireCooldown <= 0) {
				currentFireCooldown = FireCooldown;	
				
				// Instanciate a bullet
				if (Bullet != null) {
					
					GameObject b = (GameObject)GameObject.Instantiate (Bullet);
					
					BulletScript bulletScript = (BulletScript)b.GetComponent ("BulletScript");
					
					bulletScript.Direction = (Crosshair1.localPosition - this.transform.localPosition);
					bulletScript.Direction.Normalize ();
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
