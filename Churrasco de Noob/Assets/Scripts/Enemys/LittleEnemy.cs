using UnityEngine;

public class LittleEnemy : EnemyStatus
{
    private void Update()
    {
        FindPlayers();
        FollowClosestPlayer();
        UpdateAnimation();

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
}
