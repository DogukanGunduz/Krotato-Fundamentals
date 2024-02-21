using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime = 1f; // Mermi ömrü (saniye cinsinden)
    public int damageAmount = 10; // Mermi hasar miktarý
    public float knockbackForce = 5f; // Geri itme kuvveti

    void Start()
    {
        // Mermiyi belirli bir süre sonra yok et
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Mermi düþmana çarparsa
        if (other.CompareTag("Enemy"))
        {
            // Düþmanýn üzerindeki EnemyHealth bileþenini al
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Eðer düþmanýn saðlýk bileþeni varsa hasar ver
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            // Düþmaný geri itme etkisi uygula
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;

                // Knockback etkisini düþmaný uzaklaþtýracak þekilde uygula
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }

            // Mermiyi çarpýlan düþmanla birlikte yok et
            Destroy(gameObject);
        }
    }
}
