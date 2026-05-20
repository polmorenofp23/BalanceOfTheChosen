using UnityEngine;
using UnityEngine.InputSystem;

public class Anakin : MonoBehaviour
{
    [SerializeField]
    private int life = 1;

    private Animator animator;
    private bool isDead;
    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int JumpParam = Animator.StringToHash("Jump");
    private static readonly int DieParam = Animator.StringToHash("Die");

    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool jumpPressed = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat(SpeedParam, Mathf.Abs(moveInput.x));
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger(JumpParam);
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
        if (isDead)
        {
            return;
        }

        life -= damage;
        Debug.Log("Player life: " + life);
        if (life <= 0)
        {
            isDead = true;
            animator.SetTrigger(DieParam);
            Invoke(nameof(LoadGameOver), 0.25f);
        }
    }

    void LoadGameOver()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManage>().LoadCustomScene("GameOver");
        Debug.Log("Player has died");
        Destroy(gameObject);
    }
}

