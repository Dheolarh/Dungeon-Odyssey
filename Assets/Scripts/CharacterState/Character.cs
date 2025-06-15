using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public static Character Instance;
    private StateMachine _stateMachine;
    public Animator animator;
    public Vector2 facingDirection { get; private set; } = Vector2.down;
    public bool isAttacking { get;  set; }
    public float attackDuration = 0.5f; // Duration of the attack animation


    
    [Header("Character Components")]
    private SpriteRenderer _character;
    private BoxCollider2D _boxCollider2D;
    
    [Header("Character Movement")]
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _attackAction;
    public float moveSpeed = 5f;
    public Vector2 moveInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitPlayer();
        PlayerInputActions();
    }
    
    private void InitPlayer()
    {
        _character = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerInput = GetComponent<PlayerInput>();
        
        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(new IdleState(this, _stateMachine));
    }
    
    private void PlayerInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _attackAction = _playerInput.actions["Attack"];
        _attackAction.performed += OnAttack;
        _attackAction.canceled += OnAttack;
    }
    
    public void SpawnPlayerIntoScene()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            transform.position = GameManager.Instance.portal2.transform.position;
        }
    }
    
    void Update()
    {
        _stateMachine.Update();
        animator.SetFloat("Speed", moveInput.magnitude);

    }

    void FixedUpdate()
    {
      _stateMachine.FixedUpdate();
    }
    
    public void UpdateAnimatorDirection(Vector2 direction)
    {
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            facingDirection = direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            facingDirection = direction.y > 0 ? Vector2.up : Vector2.down;
        }

        animator.SetFloat("MoveX", facingDirection.x);
        animator.SetFloat("MoveY", facingDirection.y);
    }

    
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
        {
            UpdateAnimatorDirection(moveInput);
            if (!(_stateMachine.currentState is RunState))
            {
                _stateMachine.ChangeState(new RunState(this, _stateMachine, moveSpeed));
            }
        }
        else
        {
            animator.SetBool("IsIdle", true);
            if (!(_stateMachine.currentState is IdleState))
            {
                _stateMachine.ChangeState(new IdleState(this, _stateMachine));
            }
        }
        
    }

    
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        animator.SetBool("IsIdle", false);
        _stateMachine.ChangeState(new AttackState(this, _stateMachine));
    }
    
    public void SwitchToIdleState()
    {
        animator.ResetTrigger("OnAttack");
        isAttacking = false;
        _stateMachine.ChangeState(new IdleState(this, _stateMachine));
        animator.SetBool("IsIdle", true);
    }
    
    
}