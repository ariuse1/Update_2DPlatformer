using System.Collections;
using UnityEngine;

public class Vampir : Spell
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AttributeSpell _attribute;
    [SerializeField] private float _radiusEffect;
    [SerializeField] private float _attackDamage;
    [SerializeField] private LayerMask _enemyLayers;   

    private Heallth _heallthOwner;

    public override AttributeSpell AttributeSpell
    {
        get { return _attribute; }        
    }

    public override void ActivateSpell()
    {
        NexTimeCasting = Time.time + _attribute.Cooldown;
        _heallthOwner = GetComponentInParent<Heallth>();  
        
        StartCoroutine(CastSpell());
    }

    private IEnumerator CastSpell()
    {
        WaitForSeconds second = new WaitForSeconds(1);
        ParticleSystem newParticleSystem = Instantiate(_effect, transform.position, Quaternion.identity);
        newParticleSystem.transform.SetParent(transform);

        for (int i = 0; i < _attribute.RunTime; i++)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position,
         _radiusEffect, _enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                
                if(enemy.TryGetComponent(out Heallth enemyHeallth))
                {
                    float currentHealth = enemyHeallth.CurrentHealth;

                    if (currentHealth > _attackDamage)
                    {
                        _heallthOwner.Treat(_attackDamage);
                    }
                    else
                    {
                        _heallthOwner.Treat(currentHealth);
                    }

                    enemyHeallth.TakeDamage(_attackDamage);
                }
            }

            yield return second;
        }

        Destroy(newParticleSystem);

        yield return new WaitWhile(() => NexTimeCasting >= Time.time);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radiusEffect);
    }
}


