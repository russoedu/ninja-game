using Unity.VisualScripting;
using UnityEngine;

public class PayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 10;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake () {
        // Get references to the components we need
        body = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        boxCollider = GetComponent<BoxCollider2D> ();
    }

    private void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocityX = horizontalInput * speed;

        // Flip the sprite if the player is moving left-right
        if (horizontalInput > 0.01f) {
            transform.localScale = new Vector2(1, 1);
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector2(-1, 1);
        }

        // Jump if the player is pressing the space bar
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded()) {
            Jump();
        }

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
    }

    /**
     * Jump function
     */
    private void Jump () {
        body.linearVelocityY = speed;
        animator.SetTrigger("jump");
    }

    private void OnCollisionEnter2D (Collision2D collision) {
    }

    private bool isGrounded () {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall () {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
