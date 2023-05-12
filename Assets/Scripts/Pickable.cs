using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Item itemData;
    
    // Caching
    private Inventory inventory;
    private CircleCollider2D circle;
    private BoxCollider2D box;

    void Start()
    {
        // Initialize component cache
        inventory = Inventory.inventory;
        circle = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hit");

        // Case: player contact with this object and is able to collect it
        if(c.gameObject.tag == "Player" && inventory.AddItem(itemData))
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Enter circle");
        // Some code makes the item flies to the player
    }
}
