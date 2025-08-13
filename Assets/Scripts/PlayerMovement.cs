using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;
    private Vector2 lastMoveDir = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Capture raw WASD input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.sqrMagnitude > 1f)
        {
            input = input.normalized;
        }

        // Update animator parameters for movement
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetBool("IsMoving", input != Vector2.zero);

        if (input != Vector2.zero)
        {
            lastMoveDir = input;
            animator.SetFloat("LastMoveX", input.x);
            animator.SetFloat("LastMoveY", input.y);
        }
    }

    private void FixedUpdate()
    {
        // Apply movement in FixedUpdate for physics consistency
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }
}

