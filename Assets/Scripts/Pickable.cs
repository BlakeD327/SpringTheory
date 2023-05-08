using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Item itemData;
    
    // Caching Components
    private CircleCollider2D circle;
    private BoxCollider2D box;

    void Awake()
    {
        // Initialize component cache
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hit");

        // Case: player contact with this object and is able to collect it
        if(c.gameObject.tag == "Player" && Inventory.addItem(itemData))
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Enter circle");
        // Some code makes the item flies to the player
    }
}
