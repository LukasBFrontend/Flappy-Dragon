using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    /* public GameObject deathEffect; */

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        /* Instantiate(deathEffect, transform.position, Quaternion.identity); */
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
