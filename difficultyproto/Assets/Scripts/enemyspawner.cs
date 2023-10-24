using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    const float LEFT_SPAWN = -10f;
    const float RIGHT_SPAWN = 10f;
    const float TOP_SPAWN = 5f;
    const float BOT_SPAWN = -5f;
    const float SPAWNRATE = 3f;
    const float INCREMENTTIMER = 3f;

    int spawnRateMulti = 0;

    float vertSpeedMulti = 1f;
    float horiSpeedMulti = 1.8f;

    Vector2 upDown = Vector2.down;
    Vector2 leftRight = Vector2.right;
    Vector2 downUp = Vector2.up;
    Vector2 rightLeft = Vector2.left;

    float spawnTimer = 0f;
    float incTimer = 0f;

    public GameObject enemyDark;
    public GameObject enemyLight;
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
            if (spawnRateMulti < 10)
            {
                spawnRateMulti++;
            }
        }

        if (spawnTimer > SPAWNRATE - 0.2 * spawnRateMulti)
        {
            spawnTimer = 0f;
            switch (Random.Range(0, 4))
            {
                case 0: //top spawn
                    for (int i = 0; i <= 20; i++)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN + i, TOP_SPAWN);
                            GameObject enemyIns = Instantiate(enemyDark, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(upDown);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(vertSpeedMulti + 0.1f * spawnRateMulti);
                        }
                        else
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN + i, TOP_SPAWN);
                            GameObject enemyIns = Instantiate(enemyLight, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(upDown);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(vertSpeedMulti + 0.1f * spawnRateMulti);
                        }
                    }
                    break;
                case 1: //bottom spawn
                    for (int i = 0; i <= 20; i++)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN + i, BOT_SPAWN);
                            GameObject enemyIns = Instantiate(enemyDark, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(downUp);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(vertSpeedMulti + 0.1f * spawnRateMulti);
                        }
                        else
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN + i, BOT_SPAWN);
                            GameObject enemyIns = Instantiate(enemyLight, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(downUp);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(vertSpeedMulti + 0.1f * spawnRateMulti);
                        }
                    }
                    break;
                case 2: //left spawn
                    for (int i = 0; i <= 10; i++)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN, BOT_SPAWN + i);
                            GameObject enemyIns = Instantiate(enemyDark, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(leftRight);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(horiSpeedMulti + 0.18f * spawnRateMulti);
                        }
                        else
                        {
                            Vector2 pos = new Vector2(LEFT_SPAWN, BOT_SPAWN + i);
                            GameObject enemyIns = Instantiate(enemyLight, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(leftRight);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(horiSpeedMulti + 0.18f * spawnRateMulti);
                        }
                    }
                    break;
                case 3: //right spawn
                    for (int i = 0; i <= 10; i++)
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            Vector2 pos = new Vector2(RIGHT_SPAWN, BOT_SPAWN + i);
                            GameObject enemyIns = Instantiate(enemyDark, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(rightLeft);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(horiSpeedMulti + 0.18f * spawnRateMulti);
                        }
                        else
                        {
                            Vector2 pos = new Vector2(RIGHT_SPAWN, BOT_SPAWN + i);
                            GameObject enemyIns = Instantiate(enemyLight, pos, Quaternion.identity);
                            enemyIns.GetComponent<enemyMovement>().setDirection(rightLeft);
                            enemyIns.GetComponent<enemyMovement>().setSpeed(horiSpeedMulti + 0.18f * spawnRateMulti);
                        }
                    }
                    break;
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
