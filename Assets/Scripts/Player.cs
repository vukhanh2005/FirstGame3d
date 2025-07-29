using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Action")]
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;
    Vector2 moveInput;
    
    [Header("SPEED")]
    public float speedWalk;
    public float speedRun;
    public float speedRotate;
    [Header("COMPONENT")]
    Rigidbody rb;
    Animator anim;
    [Header("STATE")]
    public PlayerState currentPlayerState;
    public AttackType attackType;
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
    public enum AttackType
    {
        Melee,
        Gun,
        Archer,
        Hand
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Init first value
        currentPlayerState = PlayerState.Idle;
        //Get input
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        //Get component
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CursorController.isLocked)
        {
            return;
        }
        ControlAnimation();
        if (currentPlayerState != PlayerState.Stunning)
        {
            ChangeCurrentPlayerState(PlayerState.Walking);
            Move();
        }
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
