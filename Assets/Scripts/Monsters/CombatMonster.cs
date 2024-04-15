using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    [SerializeField] private TargetBox _attackBox;
    [SerializeField] protected float _attackDamage;

    private float _timeNextAttack;
    private float _timeWait;  

    public Vector2 Target { get; private set; }
    public bool IsWork { get; private set; } = false;
    public bool IsAttack { get; private set; } = false;

    public void Attack()
    {     
        float waitTimeAttack = 1f;
        float delay = 1.5f;

        IsAttack = _attackBox.Target != null;

        if(!IsAttack && _timeWait <= Time.time)
        {
            IsWork = false;
        }

        if (IsAttack && _timeNextAttack <= Time.time)
        {
            Target = _attackBox.Target.position;
            _attackBox.Target.GetComponent<Heallth>()?.TakeDamage(_attackDamage);
            _timeNextAttack = Time.time + waitTimeAttack;
            _timeWait = Time.time + delay;
        }
    }

    private void isAttack()
    {
        IsWork = true;
    }

    private void OnEnable()
    {
        _attackBox.Worked += isAttack;
    }

    private void OnDisable()
    {
        _attackBox.Worked -= isAttack;
    }
}
