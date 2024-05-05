using System.Collections;
using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    [SerializeField] private TargetBox _attackBox;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _waitTimeNextAttack = 1f;
    
    private bool _isWait = false;

    public Vector2 Target { get; private set; }
    public bool IsWork { get; private set; } = false;
    public bool IsAttack { get; private set; } = false;

    public void Attack()
    {        
        IsAttack = _attackBox.Target != null;

        if (!IsAttack && !_isWait)
        {
            IsWork = false;
        }
       
        if (IsAttack && !_isWait)
        {
            Target = _attackBox.Target.position;
            _attackBox.Target.GetComponent<Heallth>()?.TakeDamage(_attackDamage);

            StartCoroutine(Wait());
        }      
    }

    private IEnumerator Wait()
    {       
        _isWait = true;

        WaitForSeconds waitTimeNextAttack = new(_waitTimeNextAttack);

        yield return waitTimeNextAttack;

        _isWait = false;        
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
