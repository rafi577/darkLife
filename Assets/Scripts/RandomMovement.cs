using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float startWaitTime = 1f;
    float waitTime;

    [SerializeField] Transform movePosition;
    [SerializeField] float minX;
    [SerializeField] float maxX;


    void Start()
    {
        waitTime = startWaitTime;
        movePosition.position = new Vector2(Random.Range(minX, maxX), transform.position.y);
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePosition.position, moveSpeed * Time.deltaTime);
        if (waitTime <= 0)
        {
            waitTime = startWaitTime;
            movePosition.position = new Vector2(Random.Range(minX, maxX), transform.position.y);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
