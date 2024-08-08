using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions action;
    private Vector2 movementValue;
    public float speed = 5f;
    public Rigidbody2D rb;
    #region InputManager
    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }
    #endregion
    private void Awake()
    {
        action = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        //action.Player.Jump.performed += _ => Jump();
    }
    private void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
    }
    /*private void Jump()
    {
        rb.AddForce(Vector3.up,ForceMode2D.Impulse);
    }*/
    private void Update()
    {
        transform.position += (new Vector3(movementValue.x, movementValue.y)) * (speed * Time.deltaTime);
    }
}
