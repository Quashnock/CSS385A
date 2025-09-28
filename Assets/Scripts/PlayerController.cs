using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 

    private float movementX;
    private float movementY;
    private float movementZ;

    public float jumpHeight = 0;
    public float speed = 0;
    public int tokenTotal = 0;
    public float fallThreshold = 0;

    private int count;

    private bool isPaused = false;
    private bool isGrounded = false;

    public TextMeshProUGUI countText;

    public GameObject centerTextObject;
    public UnityEngine.AI.NavMeshAgent enemyAgent;

    void Start()
    {
        Cursort.visible = false;

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        centerTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if (isGrounded && !isPaused)
        {
            movementZ = jumpHeight;
        }
    }

    public void OnPause()
    {
        if (!isPaused)
        {
            DisplayCenterText("Pause");

            rb.linearVelocity = Vector3.zero;

            enemyAgent.isStopped = true;
            isPaused = true;
        }
        else 
        {
            Cursor.visible = false;

            centerTextObject.gameObject.SetActive(false);

            enemyAgent.isStopped = false;
            isPaused = false;
        }
    }

    void FixedUpdate()
    {   
        if (!isPaused)
        {
            Vector3 movement = new Vector3(movementX, movementZ, movementY);
            movementZ = 0;

            rb.AddForce(movement * speed);
        }

        if (rb.position.y <= fallThreshold)
        {
            DisplayCenterText("You fell off!");
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            DisplayCenterText("You lose!");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= tokenTotal) 
        {
            DisplayCenterText("You win!");

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    void DisplayCenterText(string text)
    {
        Cursor.visible = true;

        centerTextObject.SetActive(true);
        centerTextObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}