using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public bool grounded = true;
    private Rigidbody2D rb2d;
    public float jumpPower;
    private BoxCollider2D boxCollider2D;

    public static float inventory = 0f;

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
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }
        transform.position = pos;

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb2d.velocity = Vector2.up * jumpPower;
        }

        
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, 
            boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        Debug.Log("raycastHit2D.collider: " + raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    //When collide with a gold orb
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Orb")
        {
            ++inventory;
        }
    }
}
 