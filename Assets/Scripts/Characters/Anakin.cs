using UnityEngine;
using UnityEngine.InputSystem;

public class Anakin : MonoBehaviour
{
    [SerializeField]
    private int life = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool jumpPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectCollectible"))
        {
            Debug.Log("Collectible has been collided with " + collision.gameObject.name);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hazard has been collided with " + collision.gameObject.name);
            Destroy(collision.gameObject);
            DamagePlayer(1);
        }
    }

    void DamagePlayer(int damage = 1)
    {
        life -= damage;
        Debug.Log("Player life: " + life);
        if (life <= 0)
        {
            GameObject.Find("SceneManager").GetComponent<SceneManage>().LoadCustomScene("GameOver");
            Debug.Log("Player has died");
            Destroy(gameObject);
        }
    }
}

