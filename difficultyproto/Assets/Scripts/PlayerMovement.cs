using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 7.0f;
    private bool isBlack = true;
    private SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public Camera mainCamera;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    void Update()
    {
        // Movement
        Vector3 movement = Vector3.zero;

        // Keyboard controls
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) movement += Vector3.up;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) movement += Vector3.down;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) movement += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) movement += Vector3.right;

        // Xbox Controller Joystick controls
        if(movement == Vector3.zero)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            movement += new Vector3(horizontal, vertical, 0);
            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)  // Add a small deadzone
            {
                movement += new Vector3(horizontal, vertical, 0);
            }
        }

        transform.Translate(movement.normalized * speed * Time.deltaTime);

        // Change color
        // Keyboard control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBlack = !isBlack;
            SetColor();
        }

        // Xbox Controller A button control
        if (Input.GetButtonDown("Fire1"))
        {
            isBlack = !isBlack;
            SetColor();
        }
    }


    void SetColor()
    {
        if (isBlack)
        {
            spriteRenderer.color = Color.black;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Black" && !isBlack)
        {
            // Handle collision with black bullet when player is white
            gameManager.PlayDeathSound();
            StartCoroutine(PlayerDeathSequence());
        }
        else if (other.tag == "White" && isBlack)
        {
            // Handle collision with white bullet when player is black
            gameManager.PlayDeathSound();
            StartCoroutine(PlayerDeathSequence());
        }
        else if (other.tag == "Black" && isBlack)
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "White" && !isBlack)
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator PlayerDeathSequence()
    {
        // Slow down time
        Time.timeScale = 0.5f;

        // Screen shake
        Vector3 originalCamPos = mainCamera.transform.localPosition;
        float elapsed = 0.0f;
        float duration = 0.5f; // Duration of the shake in seconds

        while (elapsed < duration)
        {
            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);
            mainCamera.transform.localPosition = originalCamPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset camera position
        mainCamera.transform.localPosition = originalCamPos;

        // Destroy the player GameObject
        Destroy(gameObject);

        gameManager.RestartGame();

    }
}
