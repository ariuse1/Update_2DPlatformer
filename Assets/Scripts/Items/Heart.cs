using UnityEngine;

public class Heart : Item, IActionItem
{
    [SerializeField] private float _hit = 1;

    public void AddItem(Player hero)
    {
        if(hero.TryGetComponent<Heallth>(out Heallth heallth))
            heallth.Treat(_hit);
    }
}
