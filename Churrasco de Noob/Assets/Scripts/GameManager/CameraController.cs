using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Camera mainCamera;

    public float minDistance = 10f;
    public float maxDistance = 30f;
    public float zoomLimiter = 10f;
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0, 10, -10);

    private Vector3 velocity;

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        MoveCamera();
        AdjustZoom();
    }

    void MoveCamera()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        Vector3 targetPosition = midpoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(midpoint);
    }

    void AdjustZoom()
    {
        float distance = Vector3.Distance(player1.position, player2.position);
        float newDistance = Mathf.Lerp(minDistance, maxDistance, distance / zoomLimiter);
        Vector3 direction = (transform.position - (player1.position + player2.position) / 2f).normalized;
        transform.position = Vector3.SmoothDamp(transform.position, (player1.position + player2.position) / 2f + direction * newDistance, ref velocity, smoothTime);
    }
}
