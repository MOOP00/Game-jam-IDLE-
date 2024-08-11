using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    private bool facingRight = true; // Track if player is facing right
    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Default movement direction
        float moveX = 0f;
        float moveY = 0f;

        // Detect A (left) and D (right) key presses
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed; // Move right
        }

        // Detect W (up) and S (down) key presses
        if (Input.GetKey(KeyCode.W))
        {
            moveY = moveSpeed; // Move up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -moveSpeed; // Move down
        }

        // Create Vector3 for movement direction
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f) * Time.deltaTime;

        // Move the character in the specified direction
        transform.Translate(moveDirection, Space.World);

        // Flip the character based on the mouse position
        FlipCharacter();
    }

    // Flip the player based on the mouse position
    private void FlipCharacter()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the z position is zero for 2D

        // Determine if the player should face right or left
        if (mousePosition.x > transform.position.x && !facingRight)
        {
            // Mouse is to the right, but player is facing left
            Flip();
        }
        else if (mousePosition.x < transform.position.x && facingRight)
        {
            // Mouse is to the left, but player is facing right
            Flip();
        }
    }

    // Flip the player direction
    private void Flip()
    {
        facingRight = !facingRight;

        // Flip the player's local scale
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
