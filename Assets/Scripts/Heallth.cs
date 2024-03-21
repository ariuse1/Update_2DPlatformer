using UnityEngine;

public class Heallth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth;

    private IDie _iDie;
    private float _minHealth;

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
        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);

        if (_currentHealth == _minHealth)
        {
            _iDie?.Die();          
        }
    }

    public void TakeHealth(float health)
    {    
        _currentHealth = Mathf.Clamp(_currentHealth + health, _minHealth, _maxHealth);
    }
}
