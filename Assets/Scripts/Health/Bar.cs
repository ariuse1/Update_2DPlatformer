using UnityEngine;

abstract public class Bar : MonoBehaviour
{
    abstract public void SetStartParameters(float maxHealth, float currentHealth);
    abstract public void SetMaxHealth(float maxHealth);

    abstract public void SetHealth(float currentHealth);
}
