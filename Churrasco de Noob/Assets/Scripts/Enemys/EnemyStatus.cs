using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyStatus : MonoBehaviour
{
    [Header("Enemy Life Variables")]
    [SerializeField] float maxLife;
    float currentLife;
    [SerializeField] float gain;

    [Header("Enemy Variables")]
    NavMeshAgent agent;
    GameObject[] players;
    [SerializeField] Animator animator;

    [Header("Explosion Variables")]
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float explosionDelay = 1f;
    [SerializeField] GameObject explosionEffect;

    Vector3 originalScale;
    bool isExploding = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentLife = maxLife;
    }

    #region Life_And_Dead Cycle
    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SkillBar.Instance.IncreaseSlider(gain);
        Destroy(gameObject);
    }
    #endregion Life_And_Dead Cycle

    #region PlayerLocation
    void FindPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void FollowClosestPlayer()
    {
        if (players.Length == 0)
            return;

        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null)
        {
            agent.SetDestination(closestPlayer.transform.position);
        }
    }

    void UpdateAnimation()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);
    }
    #endregion PlayerLocation


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
