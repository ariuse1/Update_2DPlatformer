using UnityEngine;

public class Heart : Item, IActionItem
{
    [SerializeField] private float hit = 1;

    public void AddItem(Player hero)
    {
        if(hero.TryGetComponent<Heallth>(out Heallth heallth))
            heallth.TakeHealth(hit);
    }
}
