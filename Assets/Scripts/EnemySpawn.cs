using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Start()
    {
        InvokeRepeating("Spawn", 2f, 3f);
    }

    void Spawn()
    {
        Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }
}
