using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    const float LEFT_SPAWN = -9f;
    const float RIGHT_SPAWN = 9f;
    const float TOP_SPAWN = 5f;
    const float BOT_SPAWN = -5f;
    const float SPAWNRATE = 3f;
    const float INCREMENTTIMER = 10f;

    int spawnRateMulti = 0;

    float vertSpeed = 1f;
    float horiSpeed = 1.8f;

    Vector2 upDown = Vector2.down;
    Vector2 leftRight = Vector2.right;
    Vector2 downUp = Vector2.up;
    Vector2 rightLeft = Vector2.left;

    float spawnTimer = 0f;
    float incTimer = 0f;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        incTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (incTimer > INCREMENTTIMER)
        {
            incTimer = 0f;

        }

        if (spawnTimer > SPAWNRATE)
        {
            spawnTimer = 0f;

        }
    }
}
