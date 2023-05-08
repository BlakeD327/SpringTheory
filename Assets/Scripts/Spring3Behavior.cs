using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring3Behavior : MonoBehaviour
{
    public float maxVelocity = 15f; // Set the maximum velocity here

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rb.velocity.y > 0f && rb.velocity.y > maxVelocity)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
            }
        }
    }
}
