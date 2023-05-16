using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Item itemData;
    private bool test = false;
    
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

        if(!TryGetComponent<SpriteRenderer>(out SpriteRenderer render))
            render = gameObject.AddComponent<SpriteRenderer>();

        render.sprite = itemData.sprite;
        Physics2D.IgnoreCollision(box, Player.instance.boxCollider2D);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Enter circle");
        // Some code makes the item flies to the player

        if(c.tag == "Player" && test && inventory.AddItem(itemData))
        {
            Debug.Log("reached second trigger");
            Destroy(this.gameObject);
            return; 
        }  
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if(c.tag == "Player")
            test = true;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Debug.Log("Exit circle");
        // Some code makes the item flies to the player
        if(c.tag == "Player")
            test = false;
    }
}
