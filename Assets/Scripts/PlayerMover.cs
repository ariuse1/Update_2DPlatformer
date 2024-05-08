using UnityEngine;

[RequireComponent(typeof(MoveObject))]

public class PlayerMover : MonoBehaviour
{
    private const string StringHorizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;    

    private MoveObject _moveObject;
    private bool _isFlipX = false;

    private void Awake()
    {
        _moveObject = GetComponent<MoveObject>();
    }

    public void Jump()
    {
        _moveObject.Jump(_jumpForce, ForceMode2D.Impulse);        
    }

    public void Run()
    {
        Vector3 vector = transform.right * Input.GetAxisRaw(StringHorizontal);
        _moveObject.Move(transform.position + vector, _speed);
        _moveObject.Flip(vector.x, _isFlipX);
        _isFlipX = _moveObject.IsFlip;
    }   
}
