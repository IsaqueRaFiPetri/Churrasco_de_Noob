using UnityEngine;

public class LittleEnemy : EnemyStatus
{
    private void Update()
    {
        if (isDead || isExploding) return;

        FindPlayers();
        FollowClosestPlayer();
        UpdateAnimation();

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
