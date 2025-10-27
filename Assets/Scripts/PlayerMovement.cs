using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IDataPersistence
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
    private bool isPaused = false;

    public GameObject gameEndUI;
    public Text scoreText;
    public Text pauseText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gh = GetComponent<GrappleHook>();
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }
    
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
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
    
    private void OnCrouch()
    {
        isPaused = !isPaused;
        gameEndUI.SetActive(isPaused);

        int score = ScoreManager.Instance.GetScore();

        pauseText.text = "Paused";
        scoreText.text = "Enemies Defeated: " + score.ToString();
        
        // Pause/unpause the game when UI is shown
        Time.timeScale = isPaused ? 0f : 1f;
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
