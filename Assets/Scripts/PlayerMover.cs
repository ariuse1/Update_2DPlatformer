using UnityEngine;

[RequireComponent(typeof(MoveObject))]

public class PlayerMover : MonoBehaviour
{
    private const string StringHorizontal = "Horizontal";
    private const string StringJump = "Jump";
    private const string StringAttack = "Fire1";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private MoveObject _moveObject;
    private bool _isFlipX = false;
    private bool _isJump = false;
    private bool _isOnGround;

    public bool isDownHorizontal { get; private set; }
    public bool isDownJump { get; private set; }
    public bool isDownAttack { get; private set; }

    private void Awake()
    {
        _moveObject = GetComponent<MoveObject>();
    }

    private void FixedUpdate()
    {
       _isOnGround = _moveObject.IsOnGround();
    }

    private void Update()
    {
        if (_isOnGround && _isJump)
        {
            _isJump = false;
        }

        isDownHorizontal = Input.GetButton(StringHorizontal);
        isDownJump = _isOnGround && Input.GetButton(StringJump) && !_isJump;        
        isDownAttack = _isOnGround && Input.GetButton(StringAttack);
    }

    public void Jump()
    {        
        _moveObject.Jump(_jumpForce, ForceMode2D.Force);
        _isJump = true;
    }

    public void Run()
    {
        Vector3 vector = transform.right * Input.GetAxisRaw(StringHorizontal);
        _moveObject.Move(transform.position + vector, _speed);
        _moveObject.Flip(vector.x, _isFlipX);
        _isFlipX = _moveObject.IsFlip;
    }
}
