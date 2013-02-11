using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
	
	public Vector3 Direction;
	public Vector3 Speed;
	public int Damage;
	private float timeToLive;
	
	// Use this for initialization
	void Start ()
	{
		timeToLive = 5.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToLive -= Time.deltaTime;
		
		if (timeToLive <= 0) {
			Destroy (gameObject);	
		} else {
			
			Vector3 force = new Vector3 (
				100 * Direction.x * Speed.x * Time.deltaTime,
				100 * Direction.y * Speed.y * Time.deltaTime,
				100 * Direction.z * Speed.z * Time.deltaTime
			);
			
			//this.transform.Translate (force);
			this.rigidbody.AddForce(force);
		}
	}
}
