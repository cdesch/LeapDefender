using UnityEngine;
using System.Collections;
using System;
using Leap;

public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// Crosshairs
	/// </summary>
	public Transform Crosshair1;
	public GameObject Bullet;
	public float FireCooldown;
	public int Lives;
	public bool EnableLeapMotion;
	private float currentFireCooldown;
	private Controller controller;
	
	void Start ()
	{
		// Hide mouse :3
		UnityEngine.Screen.showCursor = false;
		
		currentFireCooldown = FireCooldown;
		
		if (EnableLeapMotion) {
			controller = new Controller ();
		}
	}
	
	void Update ()
	{
		currentFireCooldown -= Time.deltaTime;
		bool shoot = false;
		
		if (Crosshair1 != null) {
			
			Vector3 screenPointer = Vector3.zero;
			
			// Leap Motion
			if (EnableLeapMotion && controller.IsConnected) {
				
				Frame frame = controller.Frame();
				
				if(frame.Fingers.Count > 0)
				{
					Finger f = frame.Fingers[0];
					
					//Vector tipPositionInScreenCoordinate = f.TipPosition.ToScreenPoint(controller.CalibratedScreens.ClosestScreenHit(f));
					//Vector tipPositionInScreenCoordinate = f.TipPosition.ToScreenPoint();
					//Vector3 screenLocation = tipPositionInScreenCoordinate.ToVector3()
					
					var screen = controller.CalibratedScreens.ClosestScreenHit(f);
					Vector v = screen.Intersect(f, true);
					
					screenPointer = v.ToScreenPoint(screen).ToVector3 ();
					
					// Inverse Y axis
					screenPointer.y = screen.HeightPixels - screenPointer.y;
				}
			} 
			// Mouse
			else {
				screenPointer = Input.mousePosition;
			}
			
				RaycastHit hit;
				Ray ray = Camera.mainCamera.ScreenPointToRay (screenPointer);
			
				if (Physics.Raycast (ray, out hit) && hit.transform.name == "Ground") {
					shoot = true;
					Crosshair1.localPosition = new Vector3 (hit.point.x, hit.point.y, -2);
				}
			
			Debug.Log("Pointer location: "+screenPointer);
			
			if (currentFireCooldown <= 0 && shoot) {
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
					double rotation = Math.Atan (Crosshair1.localPosition.y / Crosshair1.localPosition.x);
					// To degrees
					float angle = (float)(rotation * (180 / Math.PI));
					
					bulletScript.transform.Rotate (0, 0, angle);
					
					// Ignore player collider
					Physics.IgnoreCollision (bullet.collider, collider);
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
