using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float mSpeed = 3f;
    private Vector2 mDirection = Vector2.zero;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(mDirection * mSpeed * Time.deltaTime);
    }

    public void setSpeed(float speed)
    {
        mSpeed *= speed;
    }

    public void setDirection(Vector2 direction)
    {
        mDirection = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
