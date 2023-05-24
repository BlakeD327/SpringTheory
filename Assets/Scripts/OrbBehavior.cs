using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehavior : MonoBehaviour
{
    public static int totalOrbs = 0;

    void Awake()
    {
        totalOrbs++;
    }

    void OnDestroy()
    {
        totalOrbs--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ScoreManager.IncrementScore(15); // Increment the score by 15
            Destroy(gameObject);

            // Update Score Display here
            Debug.Log("Score: " + ScoreManager.score);

        }
    }
}
