using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyExplode : MonoBehaviour
{
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float explosionDelay = 1f;
    [SerializeField] GameObject explosionEffect;

    NavMeshAgent agent;
    Vector3 originalScale;
    bool isExploding = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isExploding) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                StartCoroutine(ExplodeSequence());
                break;
            }
        }
    }

    IEnumerator ExplodeSequence()
    {
        isExploding = true;
        agent.isStopped = true;
        agent.speed = 0f;

        transform.localScale = new Vector3(originalScale.x * 1.5f, originalScale.y * 0.5f, originalScale.z);

        yield return new WaitForSeconds(0.5f);

        transform.localScale = originalScale;

        yield return new WaitForSeconds(explosionDelay);

        if (explosionEffect)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        AudioManager.Instance.explosion.Play();
        Destroy(gameObject);
    }
}
