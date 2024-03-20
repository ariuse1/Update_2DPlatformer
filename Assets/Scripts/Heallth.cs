using UnityEngine;

public class Heallth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    private IDie _iDie;

    private void Awake()
    {
        _iDie = GetComponent<IDie>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
      
        if (_currentHealth <= 0)
        {
            _iDie?.Die();          
        }
    }

    public void TakeHealth(float health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
