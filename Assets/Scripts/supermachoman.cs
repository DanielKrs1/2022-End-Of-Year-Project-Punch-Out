using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supermachoman : enemy
{
    // Start is called before the first frame update
    public Sprite move1;
    public Sprite move2;
    public Sprite move3;
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite lefthook;
    public Sprite lefthook2;
    public Sprite lefthook3;
    public Sprite leftup;
    public Sprite leftup2;
    public Sprite leftup3;
    public Sprite punch1;
    public Sprite punch2;
    public Sprite punch3;
    public Sprite punch4;
    public Sprite punch5;
    public Sprite punch6;
    public Sprite blockhigh;
    public Sprite blocklow;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite hitlow;
    public Sprite stunned1;
    public Sprite knockdown1;
    public Sprite knockdown2;
    public Sprite getup;
    public Sprite getup2;
    public Sprite victory;
    public Sprite victory2;


    public bool punching = false;
    public bool blockinglow = true;
    public bool blockinghigh = false;
    public bool specialing = false;
    public bool onehit = false;
    public bool counter = false;
    public littlemac lm;
    public mario mar;
    public Health heal;

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 375;

    public int lowhits = 0;
    public int highhits = 0;

    public int timesdown = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.sprite == null){
            spriteRenderer.sprite = normal;
        }
        fp = rb.position;
        moveSpeed = 1f;
        movement.x = 0f;
        movement.y = 0f;
        lm = GameObject.Find("lm").GetComponent("littlemac") as littlemac;
        mar = GameObject.Find("mario").GetComponent("mario") as mario;
        heal = GameObject.Find("health2").GetComponent("Health") as Health;
        blockinglow = true;
        blockinghigh = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heal.scale(health/375f);
        // if(lowhits == 3){
        //     blockinglow = true;
        //     blockinghigh = false;
        //     lowhits = 0;
        // }
        // if(highhits == 3){
        //     blockinghigh = true;
        //     blockinglow = false;
        //     highhits = 0;
        // }
        //var rand = new Random();
        moveSpeed = 1f;
        //lastPos = transform.position;
        if(frame == 119){
            frame = 0;
        }
        if(frame%10==0&&action.Length<2){
            var randint = Random.Range(0, 100);
            /*if(randint == 1){
                action = "special";
            }else */if(randint<7){
                action = "upper";
            }else if (randint<15){
                action = "hook";
            }else if(randint<16){
                action = "normalPunch";
            }
        }

        if(frame%10==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
            }else if (action.Equals("hook")){
                hook();
            }/*else if(action.Equals("special")){
                specialing = true;
                special();
                rb.MovePosition(rb.position+movement);//*Time.deltaTime);  
            }*/else if(action.Equals("blockLow")){
                counter = false;
                blockLow();
            }else if(action.Equals("blockHigh")){
                counter = false;
                blockHigh();
            }else if(action.Equals("hitLow")){
                counter = false;
                hitLow();
            }else if(action.Equals("rightHit")){
                counter = false;
                rightHit();
            }else if(action.Equals("leftHit")){
                counter = false;
                leftHit();
            }else if(action.Equals("hitAfterDodge")){
                hitAfterDodge();
            }else if(action.Equals("knockDown")){
                counter = false;
                knockDown();
            }else if(action.Equals("getUp")){
                counter = false;
                getUp();
            }else if(action.Equals("win")){
                win();
            }else if (action.Equals("wait")){

            }else{
                counter = false;
                specialing = false;
                if(spriteRenderer.sprite == normal){
                    spriteRenderer.sprite = normal2;
                }else if(spriteRenderer.sprite == normal2){
                    spriteRenderer.sprite = normal3;
                }else{
                    spriteRenderer.sprite = normal;
                }
                specialing = false;
                counter = false;
                blockinghigh = true;
                blockinglow = true;
            }  
             
        }
         
        frame++;  
    }

    public int count = 0;
    private bool temp1;
    private bool temp2;
    void normalPunch(){
        
        if(count == 0){
            specialing = true;
            spriteRenderer.sprite = punch1;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = punch2;
            count++;
        }else if(count > 24){
            if(count>=28){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";  
                blockinghigh = true;
                blockinglow = true; 
            }else{
                specialing = false;
                punching = false;
                counter = true;
                blockinghigh = false;
                blockinglow = true;
            } 
        }else if( count %4 == 3){
            punching = true;
            spriteRenderer.sprite = punch3;
            count++;
            if(!lm.dodging){
                lm.health -=96;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count %4 ==0){
            spriteRenderer.sprite = punch4;
            count++;
        }else if (count %4==1){
            spriteRenderer.sprite = punch5;
            count++;
        }else if (count %4 == 2){
            spriteRenderer.sprite = punch6;
            count++;
        }
    }

    void hook(){
        if(count == 0){
            specialing = true;
            spriteRenderer.sprite = lefthook;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = lefthook2;
            count++;
        }else if( count == 2){
            punching = true;
            spriteRenderer.sprite = lefthook3;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=22f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count < 6){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            hits = 4;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 6){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";  
                blockinghigh = true;
                blockinglow = true;  
        }
    }

    void upper(){
        if(count == 0){
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = leftup;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = leftup2;
            count++;
        }else if (count == 2){
            punching = true;
            spriteRenderer.sprite = leftup3;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=30;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else if (lm.blocking){
                lm.health -=22;
                if(lm.health<=0){
                    lm.knockeddown();
                }
            }
        }else if (count < 6){
            specialing = false;
            punching = false;
            counter = true;
            count++;    
            hits = 5;  
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 6){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
                blockinghigh = true;
                blockinglow = true;
        }
    }

    /*void special(){
        if(count == 0){
            specialing = true;
            movement.x = 0f;
            movement.y = 1*moveSpeed;
            spriteRenderer.sprite = normal3;
            count++;
        }else if (count == 1){
            movement.y = 0f;
            spriteRenderer.sprite = normal;
            count++;
        }else if (count ==2){
            spriteRenderer.sprite = normal3;
            count++;
        }else if (count == 3){
            spriteRenderer.sprite = move6;
            count++;
        }else if (count == 4){
            spriteRenderer.sprite = normal3;
            count++;
        }else if (count == 5){
            spriteRenderer.sprite = prepunch;
            count++;
        }else if(count == 6){
            spriteRenderer.sprite = midpunch;
            count++;
        }else if (count == 7){
            spriteRenderer.sprite = prepunch;
            count++;
        }else if (count ==8){
            spriteRenderer.sprite = normal3;
            count++;
        }else if (count == 9){
            specialing = false;
            onehit = true;
            spriteRenderer.sprite = normald;
            count++;
            movement.x = 0f;
            movement.y = -1*moveSpeed;
            rb.position  = fp;
            spriteRenderer.sprite = normal3;
            count++;
        }else{
            //specialing = false;
            specialing = false;
            movement.y = 0f;
            onehit = false;
            count-=10;
            upper();
            if(count!=0){
                count+=10;
            }
        }
    }*/

    public override void blockLow(){
        action = "blockLow";
        if(count == 0){
            spriteRenderer.sprite = blocklow;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public override void blockHigh(){
        action = "blockHigh";
        specialing = false;
        if(count == 0){
            spriteRenderer.sprite = blockhigh;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public override void hitLow(){
        specialing = false;
        lowhits++;
        action = "hitLow";
        if(count <= 2){
            spriteRenderer.sprite = hitlow;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public override void rightHit(){
        highhits++;
        action = "rightHit";
        spriteRenderer.flipX = true;
        if(count == 0){
            spriteRenderer.sprite = hithigh;
            count++;
        }if(count == 1){
            spriteRenderer.sprite = hithigh2;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            spriteRenderer.flipX = false;
            count = 0;
            action = "";
        }
    }

    public override void leftHit(){
        highhits++;
        action = "leftHit";
        if(count == 0){
            spriteRenderer.sprite = hithigh;
            count++;
        }if(count == 1){
            spriteRenderer.sprite = hithigh2;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public int hits = 7;
    public bool stunned;
    public override void hitAfterDodge(){
        action = "hitAfterDodge";
        stunned = true;
        if(hits>0){
            spriteRenderer.sprite = stunned1;
        }else{
            action = "";
            spriteRenderer.sprite = normal;
            hits = 7;
            counter = false;
            stunned = false;
            blockinghigh = true;
            blockinglow = true;
        }
    }

    public override void knockDown(){
        blockinghigh = true;
        blockinglow = true;
        stunned = false;
        hits = 7;
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockdown1;
            count++;
        }else{
            spriteRenderer.sprite = knockdown2;
            action = "getUp";
            lm.action = "";
            if(timesdown >=3){
                action = "wait";
                mar.tkod();
                lm.win();
                lm.rb.position = lm.fp;
            }
        }
    }

    public override void getUp(){
        specialing = true;
        mar.action = "wait";
        action = "getUp";
        var randint = Random.Range(0, 100);
        if(spriteRenderer.sprite == knockdown2&&randint<40){
            action = "getUp";
            spriteRenderer.sprite = getup;
        }else if (spriteRenderer.sprite == getup&&randint<80){
            spriteRenderer.sprite = getup2;
        }else if(spriteRenderer.sprite == getup2){
            health = 210;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
            lm.action = "";
            mar.action = "";
            mar.count = 0;
        }else{
            action = "wait";
            spriteRenderer.sprite = knockdown2;
            mar.action = "ecount";
        }
    }
        

    public override void win(){
        action = "win";
        mar.action = "ko";
        mar.enwin = true;
        if(count <= 3){
            spriteRenderer.sprite = victory;
            count++;
        }else{
            spriteRenderer.sprite = victory2;
        }
    }

    public void redo(){
        count = 0;
        action = "";
    }

    public override bool isblockingLow()
    {
        return blockinglow;
    }

    public override bool isblockingHigh()
    {
        return blockinghigh;
    }

    public override bool isSpecialing()
    {
        return specialing;
    }

    public override bool canOneShot()
    {
        return onehit;
    }

    public override bool canCounter()
    {
        return counter;
    }

    public override int getHealth()
    {
        return health;
    }

    public override void setHealth(int x)
    {
        health = x;
    }

    public override void changeHealth(int x)
    {
        health-=x;
    }

    public override int getTimesDown()
    {
        return timesdown;
    }

    public override void changeTimesDown()
    {
        timesdown++;
    }

    public override void setAction(string x)
    {
        action = x;
    }

    public override int getHits()
    {
        return hits;
    }

    public override void changeHits()
    {
        hits--;
    }

    public override void setHits(int x)
    {
        hits = x;
    }

    public override bool isStunned()
    {
        return stunned;
    }

     public override void setStunned(bool x)
    {
        stunned = x;
    }

    public override void setCounter(bool x)
    {
        counter = x;
    }

    public override void setKnockedOut()
    {
        action = "wait";
        spriteRenderer.sprite = knockdown2;
    }

    public override int pointsForKnockout()
    {
        return 999999;
    }
}
