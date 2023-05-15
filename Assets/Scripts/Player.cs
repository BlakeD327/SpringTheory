using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    // Const
    public const int MAX_HEALTH = 100;
    
    // Player related stats
    public float speed = 10f;
    public int currentHealth;
    public float jumpPower = 10f;

    // Internal use of components
    private Rigidbody2D rb2d;
    public BoxCollider2D boxCollider2D;
    private AudioSource audioSource;
    public HealthBar healthBar;
    public GameObject levelCompleteUI;
    [SerializeField] private LayerMask platformsLayerMask;

    // Projectile info
    public GameObject Orb;
    public float projectileSpeed = 10f;

    // Fall-damage variables
    public float fallHeight = 20f;
    private float previousY;

    // Create make the player accessible from other class
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        
        
        if(instance != null)
        {
            Debug.LogError("There's more than one player!");
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {         
        currentHealth = MAX_HEALTH;
        healthBar.SetMaxHealth(MAX_HEALTH);

        //fall damage
        previousY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangeSelectedItem();


        // If left click is pressed
        if(Input.GetMouseButtonDown(0))
            Shoot();

        // For item removal test
        if(Input.GetKeyDown(KeyCode.U))
            Inventory.inventory.UseSelectedItem(out Item i);
            
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
        if(Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            v2.y = jumpPower;
            audioSource.Play();
        }
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

    void ChangeSelectedItem()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.inventory.SelectLeft();
        if(Input.GetKeyDown(KeyCode.Alpha3))
            Inventory.inventory.SelectRight();
    }

    void Shoot()
    {
        // Use item to create projectile
        if(!Inventory.inventory.UseSelectedItem(out Item item))
        {
            Debug.Log("No items to shoot!");
            return;
        }

        // Obtaining relative position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rV = mousePos - transform.position;
        rV.Normalize(); 

        var projectile = CreateProjectile(item, transform.position, rV);
    }

    GameObject CreateProjectile(Item item, Vector3 position, Vector2 direction)
    {
        // Create object and set position
        GameObject projectile = new GameObject("Projectile");
        projectile.layer = 3;
        projectile.transform.position = position + new Vector3(0f, 0.01f, 0f);

        // Create components and initialize data
        var renderer = projectile.AddComponent<SpriteRenderer>();
        renderer.sprite = item.sprite;
        var rigid = projectile.AddComponent<Rigidbody2D>();
        rigid.velocity = direction * new Vector2(projectileSpeed, projectileSpeed);

        // Create Collider
        var boxC = projectile.AddComponent<BoxCollider2D>();
        boxC.size = Vector2.one;
        Physics2D.IgnoreCollision(boxC, boxCollider2D);
        
        var circleC = projectile.AddComponent<CircleCollider2D>();
        circleC.radius = 2f;
        circleC.isTrigger = true;
        
        var boxCTrigger = projectile.AddComponent<BoxCollider2D>();
        boxC.size = Vector2.one;
        boxCTrigger.isTrigger = true;

        // Add Projectile scripts
        var pScripts = projectile.AddComponent<Projectile>();
        pScripts.item = item;

        return projectile;
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
 