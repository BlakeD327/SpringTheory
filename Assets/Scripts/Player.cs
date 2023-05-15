using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Player : MonoBehaviour
{
    // Const
    public const int maxHealth = 100;
    
    // Player related stats
    public float speed = 10f;
    public int inventory = 0;
    public int currentHealth;
    public float jumpPower = 10f;

    // Internal use of components
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    public HealthBar healthBar;
    public GameObject levelCompleteUI;
    [SerializeField] private LayerMask platformsLayerMask;

    // Projectile info
    public GameObject Orb;
    public float projectileSpeed = 10f;

    // Fall-damage variables
    public float fallHeight = 20f;
    private float previousY;

    // Start is called before the first frame update
    void Start()
    {        
        rb2d = GetComponent<Rigidbody2D> ();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //fall damage
        previousY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            SceneManager.LoadScene("GameOver");
        }

        // If left click is pressed
        if(Input.GetMouseButtonDown(0))
            Shoot();

        //if for testing healthbar slider set to 'k' key
        if(Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5);
        }   
    }

    void Movement()
    {
        var x = Input.GetAxis("Horizontal");
        var v2 = Vector2.zero;
        v2.x = x * speed;        
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w")) && IsGrounded())
            v2.y = jumpPower;
        else
            v2.y = rb2d.velocity.y;

        rb2d.velocity = v2;      

        //This movement script fixes the angled spring bug,
        //but is not ideal 
        // Vector3 pos = transform.position;
        // if (Input.GetKey("d"))
        // {
        //     pos.x += speed * Time.deltaTime;
        // }
        // if (Input.GetKey("a"))
        // {
        //     pos.x -= speed * Time.deltaTime;
        // }
        // if(Input.GetKeyDown(KeyCode.Space)) {
        //     rb2d.velocity = Vector2.up * jumpPower;
        // }
        // transform.position = pos;
    }

    void Shoot()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction vector between the player and the mouse position
        Vector2 direction = mousePos - transform.position;
        // Normalize the direction vector to have a magnitude of 1
        direction.Normalize();
        // Calculate the angle in degrees between the direction vector and the x-axis
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Get the bounds of the player's box collider
        Bounds bounds = GetComponent<Collider2D>().bounds;
        // Calculate the spawn point of the projectile based on the direction vector
        Vector3 spawnPos = bounds.center + new Vector3(direction.x, direction.y, 0f) * (bounds.extents.x + Orb.GetComponent<Collider2D>().bounds.extents.x);
        // Create a new projectile object at the spawn point
        GameObject projectile = Instantiate(Orb, spawnPos, Quaternion.identity);
        // Set the velocity of the projectile to be in the direction of the mouse click
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        // Rotate the projectile to face the direction of the mouse click
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, 
            boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        Debug.Log("raycastHit2D.collider: " + raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //When collide with a gold orb
        if (other.gameObject.tag == "Orb")
        {
            ++inventory;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Door")
        {
            // Enable the level complete UI
            levelCompleteUI.SetActive(true);

            // Disable the player's movement
            GetComponent<Player>().enabled = false;

            // End the level after 2 seconds
            StartCoroutine(EndLevel(2f));
        }
        
        //fall damage
        if (col.gameObject.tag == "Ground") {
            float fallDistance = previousY - transform.position.y;
            if (fallDistance > fallHeight) {
                TakeDamage(10);
            }
            previousY = transform.position.y;
        }
    }

    IEnumerator EndLevel(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the next level or do something else to end the game
        // You can replace the line below with your own code to end the game or load the next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
 