using UnityEngine;

public class Pangolin : Monster
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void FixedUpdate()
    {           
        Move();
    }
}

