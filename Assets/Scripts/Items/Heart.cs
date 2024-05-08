using UnityEngine;

public class Heart : Item
{
    [SerializeField] private float _hit = 1;

    public float Hit { get; private set; }  

    private void Start()
    {
        Hit = _hit;
    }
}
