using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
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
        if (objectsToSpawn != null && objectsToSpawn.Length > 0)
        {
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            SpawnParticleEffect();
        }
        AudioManager.Instance.box.Play();
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
