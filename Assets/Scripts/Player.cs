using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public bool grounded = true;
    private Rigidbody2D rb2d;
    public float jumpPower;
    private BoxCollider2D boxCollider2D;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public static int inventory = 0;

    [SerializeField] private LayerMask platformsLayerMask;

    // Start is called before the first frame update
    void Start()
    {        
        rb2d = rb2d = GetComponent<Rigidbody2D> ();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //if for testing healthbar slider set to 'k' key
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(5);
        }
    }

    private void Movement()
    {
        var x = Input.GetAxis("Horizontal");
        var v2 = Vector2.zero;
        v2.x = x * speed;

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            v2.y = jumpPower;
        }
        else
            v2.y = rb2d.velocity.y;

        rb2d.velocity = v2; 
    }

    void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private bool IsGrounded()
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
}
 