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
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("Triangle has been collided with " + collision.gameObject.name);
            Destroy(collision.gameObject);
            DamagePlayer(10);
        }
    }

    void DamagePlayer(int damage = 10)
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

