using UnityEngine;
using UnityEngine.Events;

public class Heallth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _isMoveHPBar;
    [SerializeField] private Bar _healthBar;
    [SerializeField] private Vector3 _offsetBar;

    private UnityEvent _die = new UnityEvent();

    private float _minHealth;

    public event UnityAction Die
    {
        add => _die.AddListener(value);
        remove => _die.RemoveListener(value);
    }

    private void Start()
    {
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

        if (_healthBar != null)
            _healthBar.SetStartParameters(_maxHealth, _currentHealth);
    }

    private void Update()
    {
        if (_isMoveHPBar)
            _healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + _offsetBar);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);
        _healthBar.SetHealth(_currentHealth);

        if (_currentHealth == _minHealth)
            _die.Invoke();
    }

    public void TakeHealth(float health)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + health, _minHealth, _maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }
}
