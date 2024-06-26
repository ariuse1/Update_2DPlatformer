using UnityEngine;

abstract public class Spell : MonoBehaviour
{
    public float NexTimeCasting { get; protected set; }
    public virtual AttributeSpell AttributeSpell { get; protected set; }

    public virtual void ActivateSpell()
    {
        Debug.Log("cAST");
    }
}
