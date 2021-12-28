using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class TargetToKill : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform player;





    AIDestinationSetter destinationSetter;


    PlayerPower playerPower;
    PlayerMovement playerMovement;



    public bool isPeopleEffected = false;


    public bool diolougeEnd;


    void Start()
    {
        playerPower = FindObjectOfType<PlayerPower>();
        destinationSetter = FindObjectOfType<AIDestinationSetter>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        diolougeEnd = false;

    }






    void Update()
    {
        if (diolougeEnd)
        {
            if (isPeopleEffected)
            {
                destinationSetter.target = playerMovement.manipulator;
            }
            else
            {
                destinationSetter.target = player;
            }
        }
    }
}
