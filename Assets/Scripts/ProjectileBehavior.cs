using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private float lifetime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // Destroy the projectile after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the projectile when it collides with another object
        Destroy(gameObject);
    }
}
