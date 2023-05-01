using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Color color;
    public int hit = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = color;
        if (hit >= 4f)
        {
            Destroy(gameObject); // Destroy enemy object if hit 4   times
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Orb")
        {
            hit++;
            color.a = GetComponent<Renderer>().material.color.a * 0.8f;            
            Destroy(other.gameObject); // Destroy egg object on contact with enemy
        }
    }
}
