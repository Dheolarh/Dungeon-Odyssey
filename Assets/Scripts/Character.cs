using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public static Character Instance;
    private SpriteRenderer _character;
    private BoxCollider2D _boxCollider2D;
    private PlayerInput _playerInput;
    private InputAction _inputAction;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 _moveInput;
    private bool _sceneReposition;

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
        _character = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _playerInput = GetComponent<PlayerInput>();
        _inputAction = _playerInput.actions["Move"];
        _inputAction.performed += OnMove;
        _inputAction.canceled += OnMove;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1" && !_sceneReposition)
        {
            _sceneReposition = true;
            transform.position = GameManager.Instance.portal2.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (_moveInput != Vector2.zero)
        {
            Vector2 movement = _moveInput * moveSpeed * Time.fixedDeltaTime;
            transform.position += (Vector3)movement;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        if (_moveInput.x > 0)
            _character.flipX = false;
        else if (_moveInput.x < 0)
            _character.flipX = true;
    }
    
    
}