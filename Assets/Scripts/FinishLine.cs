using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{



    void Update()
    {
        if (FindObjectOfType<peopleCollected>().peopleCount == FindObjectOfType<peopleCollected>().maxPeople)
        {

            Invoke("LevelEnd", 1f);
        }
    }

    void LevelEnd()
    {
        FindObjectOfType<GameManager>().Win();
    }


}
