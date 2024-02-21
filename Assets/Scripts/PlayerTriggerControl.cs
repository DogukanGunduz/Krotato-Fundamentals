using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerControl : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private async void OnTriggerEnter2D(Collider2D collision)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "Player")
            {
                Die();
            }
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (gameObject.tag == "Player")
            {
                Die();
            }

        }

    }

    void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    void DieEnemy()
    {
        GameObject.Destroy(gameObject);
    }

}