using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Pathfinding;

public class OtherPeople : MonoBehaviour
{
    [SerializeField] float gravity = 5f;
    public bool isEffected = false;
    public bool isDieManipulated = true;
    CinemachineVirtualCamera vcam;
    PlayerMovement playerMovement;








    Rigidbody2D rb2d;
    bool isRight;
    float curPosition, prevPosition;
    void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {



    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player");
            Debug.Log(playerMovement.isOneManipulate);
            Debug.Log(isEffected);
            Debug.Log(isDieManipulated);
            if (!playerMovement.isOneManipulate && !isEffected && isDieManipulated)
            {
                Debug.Log("worked");
                isEffected = true;
                playerMovement.isOneManipulate = true;
                GameObject effecetedPeople = Instantiate(playerMovement.healthBarForManipulatedThings, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                effecetedPeople.transform.parent = gameObject.transform;

                GameObject fuelBar = Instantiate(playerMovement.fuelBarForManipulatedThings, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
                fuelBar.transform.parent = gameObject.transform;


                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                gameObject.GetComponent<RandomMovement>().enabled = false;
                rb2d.gravityScale = gravity;

                playerMovement.manipulator = gameObject.transform;
                playerMovement.thereIsManipulatedPeople = true;
                gameObject.AddComponent<PlayerPower>();
                FindObjectOfType<TargetToKill>().isPeopleEffected = true;
            }
        }

        if (other.gameObject.tag == "Finish")
        {
            FinishLineParticle();
            Destroy(gameObject);
            FindObjectOfType<peopleCollected>().peopleCount++;
            isEffected = false;
            isDieManipulated = false;
            FindObjectOfType<TargetToKill>().isPeopleEffected = false;
            playerMovement.thereIsManipulatedPeople = false;





            FindObjectOfType<CameraSwtiching>().isSwtichManipulator = false;

            playerMovement.hasAbility = false;
            playerMovement.isOneManipulate = false;
            Invoke("wait1secToSwitchCamera", 2f);



            FindObjectOfType<AIPath>().maxSpeed = 5f;

        }
    }

    void FinishLineParticle()
    {
        GameObject particle = Instantiate(GameObject.Find("finish_line_effect"), transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<OtherPeople>().isDieManipulated = true;
        Destroy(particle, 1f);
    }

}
/*
if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player founded");
            if (!playerPower.notDieYet)
            {
                gameObject.AddComponent<PlayerPower>();
                playerPower.notDieYet = true;
                FindObjectOfType<TargetToKill>().isPeopleEffected = true;
                vcam.Follow = gameObject.transform;
                FindObjectOfType<PlayerMovement>().thereIsManipulatedPeople = true;
                rb2d.gravityScale = gravity;
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                gameObject.GetComponent<RandomMovement>().enabled = false;
            }
            //gameObject.AddComponent<CinemachineVirtualCamera>();
            isEffected = true;
        }
*/