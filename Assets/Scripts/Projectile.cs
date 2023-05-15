using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Item item;

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hit something");

        // Case: Collision with enemy
        if(c.gameObject.tag == "Enemy")
        {
            // Some code effecting enemy
            
            Destroy(gameObject);
            return;
        }

        // Case: Collision with non-enemy
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid))
        {
            var v = rigid.velocity;
            v = new Vector2(0f, 5f);
            rigid.velocity = v;

            var pickable = gameObject.AddComponent<Pickable>();
            pickable.itemData = item;
            Destroy(this);
        }   
    }
}
