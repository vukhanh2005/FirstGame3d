using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;
    Vector2 moveInput;
    Rigidbody rb;
    Animator anim;
    public float speedWalk;
    public float speedRun;
    public float speedRotate;
    public PlayerState currentPlayerState;
    public enum PlayerState
    {
        Walking,
        Attacking,
        Idle,
        Running,
        Hurt,
        Rest,
        FreeTime,
        Stunning
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerState = PlayerState.Idle;
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlAnimation();
        if (currentPlayerState != PlayerState.Stunning)
        {
            ChangeCurrentPlayerState(PlayerState.Walking);
            Move();
        }
        Debug.Log(moveInput.x + "-" + moveInput.y);
    }
    
    void Move()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // Di chuyển
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = moveDirection.normalized * speedWalk + new Vector3(0, rb.linearVelocity.y, 0);

        // Xoay theo hướng di chuyển
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotate * Time.deltaTime);
        }

        // Trạng thái
        if (moveInput == Vector2.zero)
        {
            ChangeCurrentPlayerState(PlayerState.Idle);
        }
        else
        {
            ChangeCurrentPlayerState(PlayerState.Walking);
        }
    }

    void ChangeCurrentPlayerState(PlayerState state)
    {
        currentPlayerState = state;
    }
    void ControlAnimation()
    {
        if (currentPlayerState == PlayerState.Idle)
        {
            anim.Play("Idle");
        }
        if (currentPlayerState == PlayerState.Walking)
        {
            anim.Play("Walking_B");
        }
    }
}
