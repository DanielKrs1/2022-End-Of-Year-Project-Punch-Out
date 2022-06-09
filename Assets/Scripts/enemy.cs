using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemy : MonoBehaviour
{
    public bool punching;
    public bool blockinglow;
    public bool blockinghigh;
    public bool specialing;
    public bool onehit;
    public bool counter;
    public int health;
    public littlemac lm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hitAfterDodge(){}

    public void knockDown(){}

    public void getUp(){}

    public void win(){}

    public void blockLow(){}

    public void blockHigh(){}

    public void hitLow(){}

    public void rightHit(){}

    public void leftHit(){}
}
