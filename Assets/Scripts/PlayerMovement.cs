using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    GrappleHook gh;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    Vector2 inputDirection;
    public Vector2 InputDirection { get { return inputDirection; } }

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gh = GetComponent<GrappleHook>();
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>().normalized;
    }
    
    private void OnJump()
    {
        if (isGrounded && !gh.retracting)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!gh.retracting)
        {
            rb.linearVelocity = new Vector2(inputDirection.x * speed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
