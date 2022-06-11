using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void setCounter(bool x){}

    public virtual void setStunned(bool x){}

    public virtual bool isStunned(){
        return false;
    }

    public virtual int getHits(){
        return -1;
    }

    public virtual void setHits(int x){}

    public virtual void changeHits(){}

    public virtual void setAction(string x){}

    public virtual void changeTimesDown(){}

    public virtual int getTimesDown(){
        return -1;
    }

    public virtual int getHealth(){
        return -1;
    }

    public virtual void setHealth(int x){}

    public virtual void changeHealth(int x){}

    public virtual bool canOneShot(){
        return false;
    }

    public virtual bool canCounter(){
        return false;
    }

    public virtual bool isSpecialing(){
        return false;
    }

    public virtual bool isblockingLow(){
        return false;
    }

    public virtual bool isblockingHigh(){
        return false;
    }

    public virtual void hitAfterDodge(){}

    public virtual void knockDown(){}

    public virtual void getUp(){}

    public virtual void win(){}

    public virtual void blockLow(){}

    public virtual void blockHigh(){}

    public virtual void hitLow(){}

    public virtual void rightHit(){}

    public virtual void leftHit(){}
}
