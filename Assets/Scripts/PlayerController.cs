using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _rigidbody;
    private Vector2 movementInput;
    private Animator _animator;
    private bool lookingRight = true;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UnityEngine.Cursor.visible = true;
    }
    void FixedUpdate()
    {
        _rigidbody.linearVelocity = movementSpeed * movementInput.normalized;
        if(movementInput.x == 0 && movementInput.y == 0)
        {
            _animator.SetBool("isMoving", false);
            _animator.SetBool("lookingUp", true);
            _animator.SetBool("lookingDown", false);
            _animator.SetBool("movingSideways", false);
        }
        else if (movementInput.y>0 )
        {
            _animator.SetBool("isMoving", true);
            _animator.SetBool("lookingUp", true);
            _animator.SetBool("lookingDown", false);
            _animator.SetBool("movingSideways", false);
        }
        else if (movementInput.y<0 )
        {
            _animator.SetBool("isMoving", true);
            _animator.SetBool("lookingUp", false);
            _animator.SetBool("lookingDown", true);
            _animator.SetBool("movingSideways", false);
        }
        else if (movementInput.x!=0 )
        {
            _animator.SetBool("isMoving", true);
            _animator.SetBool("lookingUp", false);
            _animator.SetBool("lookingDown", false);
            _animator.SetBool("movingSideways", true);
            if(movementInput.x>0 && !lookingRight)
            {
                _spriteRenderer.flipX = false;
                lookingRight = true;
            }
            else if(movementInput.x<0 && lookingRight)
            {
                _spriteRenderer.flipX = true;
                lookingRight = false;
            }
        }
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void Stop()
    {
        _animator.SetBool("isMoving", false);
        _animator.SetBool("lookingUp", true);
        _animator.SetBool("lookingDown", false);
        _animator.SetBool("movingSideways", false);
    }
}
