using System.Collections;
using UnityEngine;

public class CombatPlayer : MonoBehaviour
{
    [SerializeField] private AttackPoint _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackDamage = 20;
    [SerializeField] float _waitTimeAttack = 1f;
    [SerializeField] private LayerMask _enemyLayers;

    private float _timeNextAttack;
    private float _delayTime;

    public bool IsWork { get; private set; } = false;
    public bool IsAttack { get; private set; } = false;

    private void Awake()
    {
        _enemyLayers = LayerMask.GetMask("Monsters");
    }

    public void Attack()
    {
        float waitTime = 1f;

        if (waitTime > _waitTimeAttack)
        {
            waitTime = _waitTimeAttack;
        }

        IsAttack = false;

        if (_timeNextAttack <= Time.time && !IsWork)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.transform.position,
           _attackRange, _enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Heallth>()?.TakeDamage(_attackDamage);
            }

            IsAttack = true;
            _timeNextAttack = Time.time + _waitTimeAttack;
            _delayTime = Time.time + waitTime;

            if (!IsWork)
            {
                StartCoroutine(Wait());
            }
        }
    }

    private IEnumerator Wait()
    {
        IsWork = true;
        yield return new WaitWhile(() => _delayTime >= Time.time);
        IsWork = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.transform.position, _attackRange);
    }
}
