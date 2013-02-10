using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    /// <summary>
    /// Moving speed
    /// </summary>
    public Vector3 Speed;

    void Start()
    {

    }


    void Update()
    {
        // Move towards the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Find the direction
            Vector3 direction = (player.transform.localPosition - this.transform.localPosition);
            direction.Normalize();

            // Add a speed vector
            float xTranslation = direction.x * (Speed.x * Time.deltaTime);
            float yTranslation = direction.y * (Speed.y * Time.deltaTime);
            float zTranslation = direction.z * (Speed.z * Time.deltaTime);

            // Move
            this.transform.Translate(xTranslation, yTranslation, zTranslation);
        }
    }
}
