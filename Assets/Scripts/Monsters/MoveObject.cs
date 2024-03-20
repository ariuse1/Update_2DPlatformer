using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Transform _checkGroundPoint;

    private Rigidbody2D _rigidbody2D;   

    public bool IsFlip { get; private set; } = false;
    public bool IsJump { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();     
    }

    public void Flip(float direction, bool facingDirection)
    {
        IsFlip = facingDirection;

        if ((direction < 0 && !IsFlip) || (direction > 0 && IsFlip))
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            IsFlip = !IsFlip;
        }
    }

    public bool IsOnGround()
    {
        float radius = 0.4f;
        Transform checkGroundPoint;

        if (_checkGroundPoint == null)
            checkGroundPoint = transform;
        else checkGroundPoint = _checkGroundPoint;

        Collider2D[] coladers = Physics2D.OverlapCircleAll(checkGroundPoint.position, radius);
        return coladers.Length > 1;        
    }

    public void Move(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void Jump(float jumpForce, ForceMode2D forceMode2D)
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce, forceMode2D);
    }

    public void MoveToTarget(Vector2 target, float speed, float jumpForce, bool isCanJump = true)
    {
        float positionYShift = 1;
        float acceleration = 5;
        IsJump = false;

        if (target == null)
        {
            return;
        }

        if ((target.y - transform.position.y > positionYShift) && isCanJump)
        {           
            Jump(jumpForce, ForceMode2D.Force);
            Move(target, speed + acceleration);
            IsJump = true;
        }
        else
        {            
            Move(target, speed);            
        }
    }

    public void MoveToTargetNotJump(Vector2 target, float speed)
    {
        MoveToTarget(target, speed, 0, false);
    }

    private void OnDrawGizmosSelected()
    {
        Transform checkGroundPoint;

        if (_checkGroundPoint == null)
            checkGroundPoint = transform;
        else checkGroundPoint = _checkGroundPoint;

        Gizmos.DrawWireSphere(checkGroundPoint.position, 0.4f);
    }
}
