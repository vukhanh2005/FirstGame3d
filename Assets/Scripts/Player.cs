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

        // Hướng của camera (chỉ dùng trục XZ)
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Tính hướng di chuyển theo camera
        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;

        // Di chuyển
        rb.linearVelocity = new Vector3(moveDirection.normalized.x * speedWalk, rb.linearVelocity.y, moveDirection.normalized.z * speedWalk);


        // Xoay theo hướng di chuyển
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotate * Time.deltaTime);
        }

        // Đổi trạng thái
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
