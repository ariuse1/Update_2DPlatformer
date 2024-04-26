using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName = "Spell/Attribute")]
public class AttributeSpell : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float RunTime;
    public float Cooldown;
    public ParticleSystem Effect;
}
