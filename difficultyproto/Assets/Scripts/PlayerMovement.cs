using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10.0f;
    private bool isBlack = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    void Update()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Change color
        if (Input.GetKeyDown(KeyCode.Space))
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
            Destroy(gameObject);
        }
        else if (other.tag == "White" && isBlack)
        {
            // Handle collision with white bullet when player is black
            Destroy(gameObject);
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
}
