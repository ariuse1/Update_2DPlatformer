using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    [SerializeField] private List<Spell> _spells;  

    private List<Spell> _spellsCast = new();
    private int _spellIndex = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {           
            _spellIndex = 0;
            ActivateSpell(_spellIndex);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {            
            _spellIndex = 1;
            ActivateSpell(_spellIndex);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {            
            _spellIndex = 2;
            ActivateSpell(_spellIndex);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {            
            _spellIndex = 3;
            ActivateSpell(_spellIndex);
        }        
    }

    public Spell GetSpell(int number)
    {
        return _spells[number];
    }

    public int GetCountSpells()
    {
        return _spells.Count;
    }

    public Spell GetSpellCast(int number)
    {
        return _spellsCast[number];
    }

    public int GetCountSpellsCast()
    {
        return _spellsCast.Count;
    }

    private void ActivateSpell(int index)
    {
        _spellsCast = _spellsCast.Where(i => i != null).ToList();

        if (_spells.Count > index)
        {
            if (FindSpell(_spells[index]) == false)
            {
                Spell newSpell = Instantiate(_spells[index], transform.position, Quaternion.identity);
                newSpell.transform.SetParent(transform);
                newSpell.ActivateSpell();
                _spellsCast.Add(newSpell);                
            }
        }      
    }

    private bool FindSpell(Spell requiredSpell )
    {
        foreach (Spell spell in _spellsCast)
        {            
            if (spell.AttributeSpell.Name == requiredSpell.AttributeSpell.Name)
            {
                return true;
            }
        }

        return false;
    }
}
