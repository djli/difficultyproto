using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed = 3.0f;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
