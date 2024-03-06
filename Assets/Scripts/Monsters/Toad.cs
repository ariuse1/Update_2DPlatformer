using UnityEngine;

public class Toad : Monster
{
    private void Start()
    {
        _isFlipX = true;
    }

    private void FixedUpdate()
    {
        Action();       
    }

    private void Update()
    {        
        _animationObject.Run(_state);
    }

        
}
