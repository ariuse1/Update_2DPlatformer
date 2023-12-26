using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    public void Run(Animator animator, StatesAnim state)
    {
        int _state = Animator.StringToHash("State");
        animator.SetInteger(_state, (int)state);
    }

    public bool CheckGround(Transform transform)
    {       
        float radius = 0.3f;

        Collider2D[] coladers = Physics2D.OverlapCircleAll(transform.position, radius);

        return coladers.Length > 1;
    }
}

public enum StatesAnim
{
    Idle,
    Run,
    Jump
}
