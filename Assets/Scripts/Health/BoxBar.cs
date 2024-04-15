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
    [SerializeField] private bool _isPosition;

    private float _newHealth;

    private void Start()
    {
        SetStartParameters(_maxHealth, _currentHealth);
    }   

    public override void SetStartParameters(float maxHealth, float currentHealth)
    {
        SetMaxHealth(maxHealth);
        _currentHealth = currentHealth;
        SetGradient();
    }

    public override void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        SetGradient();
    }

    public override void SetHealth(float currentHealth)
    {
        _newHealth = currentHealth;

        if (_isSlowly)
        {
            StopCoroutine(SetSlowHealth());
            StartCoroutine(SetSlowHealth());
        }
        else
        {
            _currentHealth = currentHealth;
            SetGradient();
        }
    }

    private IEnumerator SetSlowHealth()
    {


        while (_currentHealth != _newHealth)
        {
            _currentHealth = Mathf.MoveTowards(_currentHealth, _newHealth, _fillingSpeed * Time.deltaTime);
            SetGradient();
            yield return null;
        }
    }

    private void SetGradient()
    {
        _image.fillAmount = _currentHealth / _maxHealth;
        _image.color = _gradient.Evaluate(_image.fillAmount);
    }
}
