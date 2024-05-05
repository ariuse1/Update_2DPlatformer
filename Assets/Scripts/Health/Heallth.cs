using UnityEngine;
using UnityEngine.Events;

public class Heallth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;   

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    private float _minHealth;

    public UnityAction Die;

    private void Start()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = Mathf.Clamp(_currentHealth, _minHealth, MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, _minHealth, MaxHealth);

        if (CurrentHealth == _minHealth)
            Die.Invoke();       
    }

    public void Treat(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, _minHealth, MaxHealth);
    }
}

