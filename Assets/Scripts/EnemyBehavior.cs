using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Color color;
    public int hit = 0;

    public Transform player; // Reference to the player's transform
    public float flipThreshold = 0.1f; // Minimum distance for flipping the sprite

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Renderer>().material.color;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = color;
        if (hit >= 4f)
        {
            Destroy(gameObject); // Destroy enemy object if hit 4   times
            // increase score
            ScoreManager.IncrementScore(50);
            FindObjectOfType<AudioManager>().Play("EnemyDeath"); // enemy death sound
        }

        if (player != null)
        {
            // Calculate the direction vector from the enemy to the player
            Vector3 direction = player.position - transform.position;

            // Flip the sprite on the x-axis if the player is on the left side
            if (direction.x > -flipThreshold)
            {
                spriteRenderer.flipX = true;
            }
            // Flip the sprite back to its original orientation
            else if (direction.x < flipThreshold)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Orb")
        {
            hit++;
            color.a = GetComponent<Renderer>().material.color.a * 0.8f;            
            Destroy(other.gameObject); // Destroy egg object on contact with enemy
            FindObjectOfType<AudioManager>().Play("EnemyHit"); // enemy hit sound
        }
    }
}
