using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float laneDistance = 2.5f;
    public float laneChangeSpeed = 12f;

    public float jumpForce = 12f;
    public float gravity = -50f;

    public float speedIncreaseRate = 1.2f;
    public float maxSpeed = 30f;

    private int targetLane = 1;
    private Rigidbody rb;
    private Animator anim;

    private bool isGrounded = true;
    private bool isGameOver = false;

    private float verticalVelocity = 0f;

    private ScoreManager scoreManager;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (isGameOver)
            return;

        // Increase speed gradually
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += speedIncreaseRate * Time.fixedDeltaTime;
            if (forwardSpeed > maxSpeed)
                forwardSpeed = maxSpeed;
        }

        // Lane movement
        float targetX = (targetLane - 1) * laneDistance;
        float newX = Mathf.Lerp(rb.position.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);

        // Gravity
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.fixedDeltaTime;
        }

        // Apply movement
        rb.linearVelocity = new Vector3(
            (newX - rb.position.x) / Time.fixedDeltaTime,
            verticalVelocity,
            forwardSpeed
        );
    }

    void Update()
    {
        if (isGameOver)
            return;

        // Lane controls
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && targetLane > 0)
            targetLane--;

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && targetLane < 2)
            targetLane++;

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.W) ||
             Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;

            // Trigger jump animation
            anim.SetBool("isJumping", true);

            AudioManager.instance.PlaySound(AudioManager.instance.jumpSound);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && verticalVelocity <= 0)
        {
            isGrounded = true;
            verticalVelocity = 0f;

            anim.SetBool("isJumping", false);
        }

        if (collision.gameObject.CompareTag("CarObstacle") ||
            collision.gameObject.CompareTag("FenceObstacle"))
        {
            isGameOver = true;
            rb.linearVelocity = Vector3.zero;

            AudioManager.instance.PlaySound(AudioManager.instance.crashSound);

            // CAMERA SHAKE
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.2f, 0.25f));

            if (scoreManager != null)
                scoreManager.StopScore();

            if (gameManager != null)
                gameManager.TriggerGameOver();
        }
    }
}