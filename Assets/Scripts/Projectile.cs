using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int bounce;
    public Item item;
    
    
    // Show effected radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }


    void Awake()
    {
        bounce = item.maxBounce;
        Debug.Log(item.color);
        GetComponent<Renderer>().material.color = item.color;
    }

    void Start()
    {
        Debug.Log("spawned");
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hit something");
        Bounce();
    }

    void Bounce()
    {
        if(bounce-- == 0)
            Destroy(this.gameObject);
    }
}
