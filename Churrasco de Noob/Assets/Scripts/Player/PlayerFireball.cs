using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public int maxBounces = 5; 
    public float lifetime = 5f; 
    private int bounceCount = 0;
    public float damage;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        bounceCount++;
        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLife life = other.gameObject.GetComponent<EnemyLife>();
            life.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
