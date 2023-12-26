using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private const string StringHorizontal = "Horizontal";
    private const string StringJump = "Jump";

    private Animator _animator;
    private AnimationObject _animationObject;
    private Rigidbody2D _rigidbody2D;

    private bool _isGrounded = false;
    private StatesAnim _stateAnim; 
    

    private void Awake()
    {
        _animationObject = new();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGrounded = _animationObject.CheckGround(transform);
    }

    private void Update()
    {
        _stateAnim = StatesAnim.Idle;

        if (Input.GetButton(StringHorizontal))
        {
            Run();
        }

        if (_isGrounded && Input.GetButtonDown(StringJump))
        {
            Jump();
        }

        if (_isGrounded == false)
        {
            _stateAnim = StatesAnim.Jump;
        }
        
        _animationObject.Run(_animator, _stateAnim);
    } 

    private void Run()
    {        
        Vector3 vector = transform.right * Input.GetAxisRaw(StringHorizontal);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vector, _speed * Time.deltaTime);

        GetComponent<SpriteRenderer>().flipX = vector.x < 0;
        _stateAnim = StatesAnim.Run;
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);        
    }    
}
