using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoShooting : MonoBehaviour
{
    public Transform weaponTransform; // Silahýn Transform bileþeni
    public float shootingRange = 10f; // Ateþ etme menzili
    public float fireRate = 0.5f; // Ateþ hýzý (saniye cinsinden)
    public GameObject bulletPrefab; // Mermi prefab'ý
    public float bulletSpeed = 10f; // Mermi hýzý
    public float leadAmount = 0.5f; // Düþmanýn önüne geçme miktarý

    private float nextFireTime = 0f;
    private GameObject nearestEnemy;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nearestEnemy = FindNearestEnemy();

            if (nearestEnemy != null)
            {
                Vector3 targetPosition = nearestEnemy.transform.position; // Hedef düþmanýn pozisyonu

                // Dönme açýsýný hesapla
                Vector2 direction = targetPosition - transform.position;
                float distance = direction.magnitude;

                // Düþmanýn hareketine göre hedeflenen konumu güncelle
                Vector3 leadPosition = targetPosition + (Vector3)(nearestEnemy.GetComponent<Rigidbody2D>().velocity * (distance / bulletSpeed) * leadAmount);

                // Dönme açýsýný tekrar hesapla
                direction = leadPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Mermiyi oyuncu objesinin konumunda ve dönme açýsýnda oluþtur
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

                // Mermi hýzýný ayarla
                bulletRb.velocity = direction.normalized * bulletSpeed;

                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
