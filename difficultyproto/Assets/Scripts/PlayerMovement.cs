using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
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
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) movement += Vector3.up;
        if (Input.GetKey(KeyCode.S)) movement += Vector3.down;
        if (Input.GetKey(KeyCode.A)) movement += Vector3.left;
        if (Input.GetKey(KeyCode.D)) movement += Vector3.right;

        transform.Translate(movement.normalized * speed * Time.deltaTime);

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
