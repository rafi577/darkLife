using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    float prevPosition;
    float curPosition;
    bool isRight;



    void Update()
    {
        curPosition = gameObject.transform.position.x;
        if (prevPosition <= curPosition && isRight)
        {
            FlipCharacter();
        }
        else if (prevPosition > curPosition && !isRight)
        {
            FlipCharacter();
        }
        prevPosition = curPosition;
    }
    void FlipCharacter()
    {
        isRight = !isRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
