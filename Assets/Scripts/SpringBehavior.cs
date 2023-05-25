using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBehavior : MonoBehaviour
{
    public float maxVelocity; // Set the maximum velocity here

    // Start is called before the first frame update
    void Start()
    {
        // Check the name of the prefab and set maxVelocity accordingly
        if (gameObject.name == "Spring")
        {
            maxVelocity = 20f; // Set to the desired max velocity for PrefabA
        }
        else if (gameObject.name == "Spring2")
        {
            maxVelocity = 30f; // Set to the desired max velocity for PrefabB
        }
        else if (gameObject.name == "Spring3")
        {
            maxVelocity = 15f; // Set to the desired max velocity for PrefabB
        }
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
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
        }
    }
}
