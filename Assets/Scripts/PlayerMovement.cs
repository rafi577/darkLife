using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    Rigidbody2D rb2d;
    bool facingRight = true;
    public bool hasAbility = false;
    bool isGround = false;
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    [SerializeField] Health healthBar;
    public bool thereIsManipulatedPeople;



    [Header("Animation For Player")]
    [SerializeField] Animator anim;
    bool isLand = false;










    [Header("Manipulator")]
    public GameObject healthBarForManipulatedThings;
    public GameObject fuelBarForManipulatedThings;
    [HideInInspector]
    public Transform manipulator;//reference for CameraSwtiching
    [HideInInspector]
    public bool isOneManipulate = false;



    //CameraSwitching
    CameraSwtiching cameraSwitching;






    PlayerPower playerPower;


    public bool isDiolougeEnd;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cameraSwitching = FindObjectOfType<CameraSwtiching>();
        healthBar.setMaxHealth(maxHealth);
        currentHealth = maxHealth;
        playerPower = FindObjectOfType<PlayerPower>();
        isDiolougeEnd = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDiolougeEnd)
        {
            if (!hasAbility)
            {

                Jump();
            }
            PlayerAbilities();
            if (isGround)
            {
                anim.SetBool("isJump", false);

            }
            if (isLand)
            {

                anim.SetBool("isLanded", true);
                isLand = false;
            }
            else
            {
                anim.SetBool("isLanded", false);
            }
        }
    }

    void PlayerAbilities()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.F) && thereIsManipulatedPeople)
            {
                Debug.Log("F key was pressed");
                hasAbility = !hasAbility;
                cameraSwitching.isSwtichManipulator = !cameraSwitching.isSwtichManipulator;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDiolougeEnd)
        {
            if (!hasAbility)
            {
                PlayerMove();
                if (facingRight && rb2d.velocity.x < 0)
                {
                    Flip();
                }
                else if (!facingRight && rb2d.velocity.x > 0)
                {
                    Flip();
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PlayerMove()
    {
        // Movement

        float horizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, rb2d.velocity.y);
    }


    void Jump()
    {
        if (Input.GetAxis("Jump") > 0 && isGround)
        {
            rb2d.velocity = Vector2.up * jumpSpeed;
            isGround = false;
            anim.SetBool("isJump", true);
            isLand = true;
        }
    }



    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameObject particle = Instantiate(GameObject.Find("die_effect"), transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            Destroy(particle, 1f);
            Invoke("Die", 1f);
        }
    }
    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(5);
        }
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        if (other.gameObject.tag == "manipulator" && !isOneManipulate)
        {
            //manipulator = other.gameObject.transform;
            // GameObject effecetedPeople = Instantiate(healthBarForManipulatedThings, manipulator.position + new Vector3(0, .7f, 0), Quaternion.identity);
            // effecetedPeople.transform.parent = manipulator;
            //isOneManipulate = true;
            FindObjectOfType<OtherPeople>().isDieManipulated = false;
            FindObjectOfType<TargetToKill>().isPeopleEffected = true;
            thereIsManipulatedPeople = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "manipulator" && !isOneManipulate)
        {


        }
    }



}
