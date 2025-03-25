using UnityEngine;
using UnityEngine.AI;

public class EnemyExplodeFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] players;
    [SerializeField] private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FindPlayers();
        FollowClosestPlayer();
        UpdateAnimation();
    }

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
}
