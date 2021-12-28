using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject gate;
    public bool isGateOpen;
    void Start()
    {
        isGateOpen = true;
    }


    void Update()
    {
        gate.SetActive(isGateOpen);
    }
}
