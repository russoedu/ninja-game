using Unity.VisualScripting;
using UnityEngine;

public class PayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 10;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    private void Awake () {
        // Get references to the components we need
        body = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
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
        if (Input.GetKeyDown (KeyCode.Space) && grounded) {
            Jump();
        }

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", grounded);
    }

    /**
     * Jump function
     */
    private void Jump () {
        body.linearVelocityY = speed;
        animator.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}
