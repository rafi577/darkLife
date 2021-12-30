using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] float moveSpeed = 500f;
    [SerializeField] float flyForce = 500f;



    [SerializeField] float maxFlyTime = 20f;
    float flyTime;
    Fuel fuelBar;
    float currentFuel;





    float currentHealth;
    Health healthBar;

    PlayerMovement playerMovement;
    Rigidbody2D rb2d;
    public bool onAblities = false;

    bool isGround = true;



    //if manipulator die then camera will follow player
    CameraSwtiching cameraSwitching;

    //Animator anim;





    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        healthBar = FindObjectOfType<Health>();
        healthBar.setMaxHealth(100f);
        currentHealth = 100f;
        cameraSwitching = FindObjectOfType<CameraSwtiching>();
        flyTime = maxFlyTime;
        //anim = GetComponent<Animator>();
        fuelBar = FindObjectOfType<Fuel>();
        fuelBar.setMaxHealth(maxFlyTime);
        currentFuel = maxFlyTime;


    }
    void Update()
    {
        onAblities = playerMovement.hasAbility;
        if (!isGround)
        {

        }

    }

    void FixedUpdate()
    {
        //bool ablitie = FindObjectOfType<PlayerMovement>().hasAbility;
        rb2d.velocity = new Vector2(0, 0);

        if (onAblities)
        {
            if (!isGround)
            {

                flyTime -= Time.deltaTime;
            }
            if (flyTime > 0)
            {
                Fly();
                currentFuel = flyTime;
                fuelBar.setHealth(currentFuel);
                //GameObject.Find("fly_sound").GetComponent<AudioSource>().Play();
            }
            else
            {
                Move();
                rb2d.velocity = new Vector2(0, 0);
            }

            if (isGround)
            {
                flyTime += Time.deltaTime;
                if (flyTime >= maxFlyTime)
                {
                    flyTime = maxFlyTime;
                }
                currentFuel = flyTime;
                fuelBar.setHealth(currentFuel);
                //GameObject.Find("fly").GetComponent<AudioSource>().Stop();
            }
        }
    }
    void Move()
    {
        // Movement

        float horizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, rb2d.velocity.y);
    }
    void Fly()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, vertical * flyForce * Time.fixedDeltaTime);



        /*
        if (Input.GetAxis("Jump") > 0 && isGround)
        {
            rb2d.velocity = Vector2.up * flyForce;
            isGround = false;

        }
        */
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            //anim.SetBool("isJump", false);
        }
        if (other.gameObject.tag == "Enemy")
        {
            //transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(5f, 0, 0), 10f * Time.deltaTime);
            //TakeDamage(2f);
            //HitEffect();
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameObject.Find("manipulated_die_sound").GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
            FindObjectOfType<TargetToKill>().isPeopleEffected = false;
            GameObject particle = Instantiate(GameObject.Find("die_effect"), transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            FindObjectOfType<OtherPeople>().isDieManipulated = true;
            Invoke("Die", 1f);
            Destroy(particle, 1f);
        }
        healthBar.setHealth(currentHealth);
    }

    void Die()
    {
        playerMovement.thereIsManipulatedPeople = false;
        cameraSwitching.isSwtichManipulator = false;
        playerMovement.hasAbility = false;
        playerMovement.isOneManipulate = false;

        FindObjectOfType<OtherPeople>().isEffected = false;
        Destroy(gameObject);
        FindObjectOfType<peopleCollected>().totalPeople--;
    }


    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        if (other.gameObject.tag == "Enemy")
        {

            TakeDamage(2f);
            HitEffect();

        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "darkStar")
        {

            TakeDamage(2f);
            HitEffect();

        }
    }

    void HitEffect()
    {
        GameObject particle = Instantiate(GameObject.Find("hit_effect"), transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, 1f);


        GameObject.Find("hit_sound").GetComponent<AudioSource>().Play();

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }



}
