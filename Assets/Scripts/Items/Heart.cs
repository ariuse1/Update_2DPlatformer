using UnityEngine;

public class Heart : Item, IActionItem
{
    [SerializeField] float hit = 1;

    public void AddItem(Player hero)
    {
        Heallth heallth = hero.GetComponent<Heallth>();
        heallth?.TakeHealth(hit);
    }
}
