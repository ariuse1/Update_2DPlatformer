using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHUB : MonoBehaviour
{
    [SerializeField] private List<Image> _slotsIcon;
    [SerializeField] private List<Image> _reloadIcone;
    [SerializeField] private SpellSystem _spellSystem;

    private void Update()
    {
        SetSpells();
    }

    private void SetSpells()
    {       
        int count = _spellSystem.GetCountSpells();

        if (count > _slotsIcon.Count)
            count = _slotsIcon.Count;

        for (int i = 0; i < count; i++)
        {
            Spell spell = _spellSystem.GetSpell(i);

            _slotsIcon[i].sprite = spell.AttributeSpell.Sprite;
            _reloadIcone[i].fillAmount = 0;
            UpdateReloudIcone(spell, _reloadIcone[i]);
        }        
    }

    private void UpdateReloudIcone(Spell spell, Image reloadIcone)
    {       
        int countSpellCast = _spellSystem.GetCountSpellsCast();

        for (int i = 0; i < countSpellCast; i++)
        {
            Spell castSpell = _spellSystem.GetSpellCast(i);           

            if (spell.AttributeSpell.Name == castSpell.AttributeSpell.Name)
            {                
                if (castSpell.NexTimeCasting >= Time.time)
                {
                    reloadIcone.fillAmount = (castSpell.NexTimeCasting - Time.time )/ castSpell.AttributeSpell.Cooldown;                    
                }
            }
        }
    }
}