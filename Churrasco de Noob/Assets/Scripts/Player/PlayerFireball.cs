using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    [SerializeField] int maxBounces = 5;
    [SerializeField] float lifetime = 5f; 
    private int bounceCount = 0;
    [SerializeField] float damage;
    [SerializeField] GameObject particleEffect;


    void Start()
    {
        Destroy(gameObject, lifetime);
        Physics.IgnoreLayerCollision(7, 3);
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
            AudioManager.Instance.fireball.Play();
            SpawnParticleEffect();
            Destroy(gameObject);
        }
    }

    void SpawnParticleEffect()
    {
        if (particleEffect != null)
        {
            GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
