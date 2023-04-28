using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerPosition = other.transform.position;
            Vector3 boundaryPosition = transform.position;
            Vector3 boundarySize = GetComponent<BoxCollider2D>().size;

            float minX = boundaryPosition.x - boundarySize.x / 2f;
            float maxX = boundaryPosition.x + boundarySize.x / 2f;
            float minY = boundaryPosition.y - boundarySize.y / 2f;
            float maxY = boundaryPosition.y + boundarySize.y / 2f;

            playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);
            playerPosition.y = Mathf.Clamp(playerPosition.y, minY, maxY);

            other.transform.position = playerPosition;
        }
    }
}
