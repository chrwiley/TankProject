using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{



    void OnCollisionEnter(Collision collision) //when the bullet hits an enemy, the enemy takes damage
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<TankHealth>();
        if (health != null)
        {
            health.TakeDamage(10);
        }

        if (collision.gameObject.tag == "Player")
        {

            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
}



