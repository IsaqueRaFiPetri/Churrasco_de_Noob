using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;
    [HideInInspector]public float damage = 1;
    private float nextFireTime = 0f;

    PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        stats.fireRate = fireRate;
        stats.bulletSpeed = bulletSpeed;
        stats.damage = damage;

        Debug.Log(damage);
    }

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        PlayerFireball fire = bullet.GetComponent<PlayerFireball>();
        if (rb != null && fire != null)
        {
            rb.linearVelocity = shootPoint.forward * bulletSpeed;
            stats.damage = damage;
        }
    }
}
