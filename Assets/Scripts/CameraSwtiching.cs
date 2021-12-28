using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwtiching : MonoBehaviour
{
    public bool isSwtichManipulator = false;
    [SerializeField] Transform player;
    [SerializeField] Transform manipulatorTarget;
    CinemachineVirtualCamera vcam;
    PlayerMovement playerMovement;

    void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        manipulatorTarget = playerMovement.manipulator;
        if (isSwtichManipulator)
        {
            vcam.Follow = manipulatorTarget;
        }
        else
        {
            vcam.Follow = player;
        }
    }
}
