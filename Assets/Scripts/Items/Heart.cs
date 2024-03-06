using UnityEngine;

public class Heart : Item, IActionItem
{
    [SerializeField] float hit = 1;

    public void Action(Hero hero)
    {
        hero.GetComponent<Heallth>()?.TakeHealth(hit);
    }
}
