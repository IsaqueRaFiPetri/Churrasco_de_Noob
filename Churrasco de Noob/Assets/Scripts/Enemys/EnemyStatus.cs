using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyStatus : MonoBehaviour, IDamagable
{
    [Header("Enemy Life Variables")]
    [SerializeField] float maxLife;
    float currentLife;
    [SerializeField] protected float gain;

    [Header("Enemy Variables")]
    NavMeshAgent agent;
    GameObject[] players;
    Animator animator;

    [Header("Explosion Variables")]
    [SerializeField] protected float detectionRange;
    [SerializeField] float explosionDelay;
    [SerializeField] GameObject explosionEffect;

    Vector3 originalScale;
    protected bool isExploding = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentLife = maxLife;
    }

    #region PlayerLocation
    protected void FindPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    protected void FollowClosestPlayer()
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

    protected void UpdateAnimation()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);
    }
    #endregion PlayerLocation

    #region ExplodeSequence
    protected IEnumerator ExplodeSequence()
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


    #endregion ExplodeSequence

    public void TakeDamage(float amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        SkillBar.Instance.IncreaseSlider(gain);
        Destroy(gameObject);
    }
}