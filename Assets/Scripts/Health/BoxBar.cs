using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxBar : Bar
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _image;
    [SerializeField] private bool _isSlowly;
    [SerializeField] private float _fillingSpeed;
    [SerializeField] private bool _isMoveHPBar;
    [SerializeField] private Vector3 _offsetBar;
    [SerializeField] private Heallth _heallth;

    private float _newHealth;
    private bool _isStart = true;
    private bool _isWork = false;
    private Coroutine _coroutine;

    private void Update()
    {
        if (_isStart)
            SetHealth(_heallth.MaxHealth, _heallth.CurrentHealth);

        if (_isMoveHPBar)
            this.transform.position = Camera.main.WorldToScreenPoint(_heallth.transform.position + _offsetBar);

        if (_heallth.CurrentHealth != _currentHealth)
            SetHealth(_heallth.MaxHealth, _heallth.CurrentHealth);
    }

    public override void SetHealth(float maxHealth, float currentHealth)
    {
        _maxHealth = maxHealth;
        _newHealth = currentHealth;

        if (_isSlowly && _isStart == false)
        {
            if (_isWork == false)
            {
                _isWork = true;

                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(SetSlowHealth());
            }
        }
        else
        {
            _currentHealth = currentHealth;
            SetGradient();
        }

        _isStart = false;
    }

    private IEnumerator SetSlowHealth()
    {
        while (_currentHealth != _newHealth)
        {
            _currentHealth = Mathf.MoveTowards(_currentHealth, _newHealth, _fillingSpeed * Time.deltaTime);
            SetGradient();
            yield return null;
        }

        _isWork = false;
    }

    private void SetGradient()
    {
        _image.fillAmount = _currentHealth / _maxHealth;
        _image.color = _gradient.Evaluate(_image.fillAmount);
    }
}
