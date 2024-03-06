using UnityEngine;

[RequireComponent(typeof(MoveObject))]

public class AnimationObject : MonoBehaviour
{
    private Animator _animator;     
    private MoveObject _moveObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveObject = GetComponent<MoveObject>();
    }

    public void Run(StatesAnim state )
    {
        int stateHash = Animator.StringToHash("State");
      
        if (state != StatesAnim.Jump && _moveObject.CheckGround() == false)        
            state = StatesAnim.Jump;
        _animator.SetInteger(stateHash, (int)state);
    }    
}

public enum StatesAnim
{
    Idle,
    Run,
    Jump,
    Attack
}
