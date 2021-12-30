using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    [SerializeField] Slider health;
    [SerializeField] Transform enemySpawner;


    void Start()
    {
        health = health.GetComponent<Slider>();
        health.maxValue = maxHealth;
        currentHealth = maxHealth;
        health.value = currentHealth;
    }



    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        health.value = currentHealth;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            //gameObject.SetActive(false);
            GameObject particle = Instantiate(GameObject.Find("die_effect"), transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            Destroy(particle, 1f);

            GameObject.Find("hit_sound").GetComponent<AudioSource>().Play();
            transform.position = enemySpawner.position;

            FindObjectOfType<AIPath>().maxSpeed = 0f;

            Invoke("Die", 1f);
        }
    }

    void Die()
    {
        FindObjectOfType<Gate>().isGateOpen = false;
        health.maxValue = maxHealth;
        currentHealth = maxHealth;
        health.value = currentHealth;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "lightStar")
        {
            TakeDamage(1f);
            GameObject particle = Instantiate(GameObject.Find("die_effect"), transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            Destroy(particle, 1f);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "gateLock")
        {

            Invoke("doorClose", .5f);


        }
    }

    void doorClose()
    {
        FindObjectOfType<Gate>().isGateOpen = true;
    }
}
