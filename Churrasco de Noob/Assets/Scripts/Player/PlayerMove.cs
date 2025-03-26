using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;
    [SerializeField] private Color color;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource footstepAudio;

    private Rigidbody rb;
    private float inputHorizontal;
    private float inputVertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(3, 3);
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        inputVertical = Input.GetAxisRaw(inputNameVertical);

        bool isMoving = inputHorizontal != 0 || inputVertical != 0;
        animator.SetBool("isRunning", isMoving);

        // Controla o som dos passos
        /*if (isMoving && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
        else if (!isMoving && footstepAudio.isPlaying)
        {
            footstepAudio.Pause();
        }*/
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(inputHorizontal, 0f, inputVertical).normalized;

        if (moveDirection.magnitude > 0)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }
}
