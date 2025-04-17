using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    [SerializeField] string inputNameHorizontal;
    [SerializeField] string inputNameVertical;
    [SerializeField] Color color;
    [SerializeField] Animator animator;

     Rigidbody rb;
     float inputHorizontal;
     float inputVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(3, 3);
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        inputVertical = Input.GetAxisRaw(inputNameVertical);

        bool isMoving = inputHorizontal != 0 || inputVertical != 0;
        animator.SetBool("isRunning", isMoving);
    }

    void FixedUpdate()
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
