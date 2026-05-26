using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Anakin : MonoBehaviour
{
    [SerializeField]
    private int life = 1;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead;
    private bool isGrounded = true;
    public int blueKyberCrystalsCollected = 0;
    public int purpleKyberCrystalsCollected = 0;
    public bool hologramCollected = false;
    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int JumpParam = Animator.StringToHash("Jump");
    private static readonly int DieParam = Animator.StringToHash("Die");

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    public bool jumpPressed = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        bool isInAir = Mathf.Abs(rb.linearVelocity.y) > 0.01f;
        float horizontalSpeed = Mathf.Abs(moveInput.x * moveSpeed);
        animator.speed = !isInAir && horizontalSpeed > 0f ? horizontalSpeed : 1f;

        if (!isInAir && horizontalSpeed > 0f)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayWalk();
            }
        }
        else
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.StopWalk();
            }
        }

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
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
        animator.SetFloat(SpeedParam, Mathf.Abs(moveInput.x));
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            jumpPressed = true;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
            {
                if (!isGrounded && SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlayLand();
                }

                isGrounded = true;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectsCollectibles"))
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayHologram();
            }

            hologramCollected = true;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ObjectCollectibleBlue") || collision.gameObject.CompareTag("ObjectCollectiblePurple"))
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayCrystal();
            }

            if (collision.gameObject.CompareTag("ObjectCollectibleBlue"))
            {
                blueKyberCrystalsCollected++;
            }
            else if (collision.gameObject.CompareTag("ObjectCollectiblePurple"))
            {
                purpleKyberCrystalsCollected++;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("HazardRed") || collision.gameObject.CompareTag("HazardGreen"))
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
            Invoke(nameof(ShowGameOver), 2f);
        }
    }

    void ShowGameOver()
    {
        if (SceneManage.Instance != null)
        {
            SceneManage.Instance.ShowCustomScene("GameOver");
        }
        else
        {
            Debug.LogError("Anakin: SceneManage.Instance is null when trying to load GameOver");
        }
        Debug.Log("Player has died");
    }
}