using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Player : MonoBehaviour
{
    public float speed = 10f;
    public bool grounded = true;
    private Rigidbody2D rb2d;
    public float jumpPower;
    private BoxCollider2D boxCollider2D;
    public GameObject levelCompleteUI;


    public static int inventory = 0;

    [SerializeField] private LayerMask platformsLayerMask;

    // Start is called before the first frame update
    void Start()
    {        
        rb2d = rb2d = GetComponent<Rigidbody2D> ();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
 