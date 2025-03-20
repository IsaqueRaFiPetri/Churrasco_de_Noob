using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject particleEffect;

    private void OnCollisionEnter(Collision collision)
    {
        SpawnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            SpawnParticleEffect();
        }

        Destroy(gameObject); 
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
