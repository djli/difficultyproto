using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class enemyspawner : MonoBehaviour
{
    const float LEFT_SPAWN = -10f;
    const float RIGHT_SPAWN = 10f;
    const float TOP_SPAWN = 5f;
    const float BOT_SPAWN = -5f;
    const float SPAWNRATE = 3f;
    const float INCREMENTTIMER = 3f;
    const int SPAWN_MULTI_CAP = 5;
    const int HOMING_SPAWN_CAP = 5;
    const float HOMING_SPAWN = 5f;
    const int THIRD_WAVE_CAP = 10;  // New constant to control when the third wave starts spawning

    int spawnRateMulti = 0;
    int homingSpawnMulti = 0;
    public int scoreCount = 0;

    float vertSpeedMulti = 1f;
    float horiSpeedMulti = 1.8f;

    Vector2 upDown = Vector2.down;
    Vector2 leftRight = Vector2.right;
    Vector2 downUp = Vector2.up;
    Vector2 rightLeft = Vector2.left;

    float spawnTimer = 0f;
    float incTimer = 0f;
    float homingTimer = 0f;

    private int prevWave = 0;
    private int nextWave = 0;
    private int thirdWave = 0;

    public GameObject enemyDark;
    public GameObject enemyLight;
    public GameObject bulletDark;
    public GameObject bulletLight;

    public GameManager gameManager;
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
            if (spawnRateMulti < SPAWN_MULTI_CAP)
            {
                spawnRateMulti++;
            }
        }

        if (spawnTimer > SPAWNRATE - 0.2 * spawnRateMulti)
        {
            spawnTimer = 0f;
            scoreCount++;
            gameManager.AddScore();

            prevWave = Random.Range(0, 4);
            SpawnWave(prevWave);

            if (spawnRateMulti == SPAWN_MULTI_CAP)
            {
                nextWave = Random.Range(0, 4);
                while (nextWave == prevWave)
                {
                    nextWave = Random.Range(0, 4);
                }
                SpawnWave(nextWave);
            }

            // Spawn the third wave if the scoreCount has reached THIRD_WAVE_CAP
            if (scoreCount >= THIRD_WAVE_CAP)
            {
                thirdWave = Random.Range(0, 4);
                while (thirdWave == prevWave || thirdWave == nextWave)
                {
                    thirdWave = Random.Range(0, 4);
                }
                SpawnWave(thirdWave);
            }
        }

        if (spawnRateMulti == SPAWN_MULTI_CAP)
        {
            homingTimer += Time.deltaTime;
            if (homingTimer > HOMING_SPAWN - homingSpawnMulti * 0.5f)
            {
                homingTimer = 0f;
                if (homingSpawnMulti < HOMING_SPAWN_CAP)
                {
                    homingSpawnMulti++;
                }
                Vector2 pos = Vector2.zero;
                switch (Random.Range(0, 4))
                {
                    case 0: //top left
                        pos = new Vector2(LEFT_SPAWN, TOP_SPAWN);
                        break;
                    case 1: //top right
                        pos = new Vector2(RIGHT_SPAWN, TOP_SPAWN);
                        break;
                    case 2: //bottom left
                        pos = new Vector2(LEFT_SPAWN, BOT_SPAWN);
                        break;
                    case 3: //bottom right
                        pos = new Vector2(RIGHT_SPAWN, BOT_SPAWN);
                        break;
                }
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(bulletDark, pos, Quaternion.identity);
                }
                else
                {
                    Instantiate(bulletLight, pos, Quaternion.identity);
                }
            }
        }
    }

    void SpawnWave(int wave)
    {
        switch (wave)
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
