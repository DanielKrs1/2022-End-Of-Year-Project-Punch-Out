using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinghippo : enemy
{
    // Start is called before the first frame update
    public Sprite appear;
    public Sprite appear2;
    public Sprite appear3;
    public Sprite move;
    public Sprite move2;
    public Sprite move3;
    public Sprite move4;
    public Sprite move5;
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normal4;
    public Sprite prepunch;
    public Sprite prepunch2;
    public Sprite punch;
    public Sprite prefullpunch;
    public Sprite cluefullpunch;
    public Sprite fullpunch;
    public Sprite pantshigh;
    public Sprite pantshigh2;
    public Sprite pants;
    public Sprite punched;
    public Sprite pulluppants;
    public Sprite blocklow;
    public Sprite blocklow2;
    public Sprite blockhigh;
    public Sprite blockhigh2;
    public Sprite knockout1;
    public Sprite knockout2;
    public Sprite knockout3;
    public Sprite knockout4;
    public Sprite knockout5;
    public Sprite knockout6;
    public Sprite knockout7;
    public Sprite victory1;
    public Sprite victory2;
    public Sprite victory3;
    public Sprite victory4;
    public Sprite victory5;
    public Sprite victory6;
    public Sprite victory7;

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
    public int health = 290;

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
        heal.scale(health/290f);
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
            }else */if(randint<10){
                if(Random.Range(0,10)<5){
                    action = "fullpunchl";    
                }else{
                    action = "fullpunchr";
                }                
            }else if(randint<16){
                if(Random.Range(0,10)<5){
                    action = "normalpunchl";    
                }else{
                    action = "normalpunchr";
                } 
            }
            blockinglow = true;
            blockinghigh = true;
        }

        if(frame%10==0){
            if(action.Equals("normalpunchl")){
                normalPunchL();
            }else if (action.Equals("normalpunchr")){
                normalPunchR();   
            }else if (action.Equals("fullpunchl")){
                fullPunchL();
            }else if(action.Equals("fullpunchr")){
                fullPunchR();
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
                knockDown();
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
                }else if (spriteRenderer.sprite == normal3){
                    spriteRenderer.sprite = normal4;
                }else {
                    spriteRenderer.sprite = normal;
                }
                specialing = false;
                counter = false;
                blockinglow = true;
                blockinghigh = true;
            }  
             
        }
         
        frame++;  
    }

    public int count = 0;
    private bool temp1;
    private bool temp2;
    void normalPunchL(){
        if(count < 2){
            specialing = true;
            spriteRenderer.sprite = prepunch;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count < 4){
            spriteRenderer.sprite = prepunch2;
            count++;
        }else if( count < 6){
            punching = true;
            spriteRenderer.sprite = punch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else {
            specialing = false;
            counter = false;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";  
            blockinghigh = true;
            blockinglow = true;  
        }
    }

    void normalPunchR(){
        if(count < 2){
            spriteRenderer.flipX = true;
            specialing = true;
            spriteRenderer.sprite = prepunch;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count < 4){
            spriteRenderer.sprite = prepunch2;
            count++;
        }else if( count < 6){
            punching = true;
            spriteRenderer.sprite = punch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else {
            spriteRenderer.flipX = false;
            specialing = false;
            counter = false;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";  
            blockinghigh = true;
            blockinglow = true;  
        }
    }

    void fullPunchL(){
        if(count < 3){
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = prefullpunch;
            count++;
            specialing = false;
            punching = false;
            counter = false;     
            blockinghigh = false;
            blockinglow = true;
        }else if (count == 4){
            spriteRenderer.sprite = cluefullpunch;
            count++;
        }else if (count == 5){
            spriteRenderer.sprite = prefullpunch;
            count++;
        }else if (count ==6){
            punching = true;
            spriteRenderer.sprite = fullpunch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=48f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else if(lm.blocking){
                lm.health-=40f;
            }
        }else if(count == 7){
            counter = false;
            punching = false;
            specialing = false;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";    
            blockinghigh = true;
            blockinglow = true;
        }
    }

    void fullPunchR(){
        spriteRenderer.flipX = true;
        if(count < 3){
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = prefullpunch;
            count++;
            specialing = false;
            punching = false;
            counter = false;
            blockinghigh = false;
            blockinglow = true;
        }else if (count == 4){
            spriteRenderer.sprite = cluefullpunch;
            count++;
        }else if (count == 5){
            spriteRenderer.sprite = prefullpunch;
            count++;
        }else if (count ==6){
            punching = true;
            spriteRenderer.sprite = fullpunch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=48f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else if(lm.blocking){
                lm.health-=40f;
            }
        }else if(count == 7){
            counter = false;
            punching = false;
            specialing = false;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";    
            blockinghigh = true;
            blockinglow = true;
            spriteRenderer.flipX = false;
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
        }if(count == 1){
            spriteRenderer.sprite = blocklow2;
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
        }if(count == 1){
            spriteRenderer.sprite = blockhigh2;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public override void hitLow(){
        if(spriteRenderer.sprite = pantshigh){
            count = 0;
        }
        specialing = false;
        lowhits++;
        action = "hitLow";
        if(count < 4){
            spriteRenderer.sprite = pantshigh2;
            count++;
            counter = true;
        }else if( count < 8){
            counter = true;
            spriteRenderer.sprite = pants;
            count++;
        }else if (count == 8){
            spriteRenderer.sprite = pulluppants;
            count++;
            counter = false;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public override void rightHit(){
        if(action.Equals("fullpunchl")||action.Equals("fullpunchr")){
            count = 0;
        }
        spriteRenderer.flipX = false;
        highhits++;
        action = "rightHit";
        //spriteRenderer.flipX = true;
        if(count < 5){
            spriteRenderer.sprite = pantshigh;
            blockinghigh = true;
            blockinglow = false;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            spriteRenderer.flipX = false;
            count = 0;
            action = "";
        }
    }

    public override void leftHit(){
        if(action.Equals("fullpunchl")||action.Equals("fullpunchr")){
            count = 0;
        }
        spriteRenderer.flipX = false;
        highhits++;
        action = "rightHit";
        //spriteRenderer.flipX = true;
        if(count < 5){
            spriteRenderer.sprite = pantshigh;
            blockinghigh = true;
            blockinglow = false;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            spriteRenderer.flipX = false;
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
            spriteRenderer.sprite = punched;
        }else{
            action = "";
            spriteRenderer.sprite = normal;
            hits = 7;
            stunned = false;
            counter = false;
        }
    }

    public override void knockDown(){
        stunned = false;
        hits = 7;
        action = "knockDown";
        if(count == 0){
            spriteRenderer.sprite = knockout1;
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = knockout2;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = knockout3;
            count++;
        }else if(spriteRenderer.sprite = knockout4){
            spriteRenderer.sprite = knockout5;
            count++;
        }else if(spriteRenderer.sprite = knockout5){
            spriteRenderer.sprite = knockout6;
            count++;
        }else if(spriteRenderer.sprite = knockout6){
            spriteRenderer.sprite = knockout7;
            count++;
        }else{
            spriteRenderer.sprite = knockout4;
        }
    }

    public override void getUp(){
        // mar.action = "wait";
        // action = "getUp";
        // var randint = Random.Range(0, 100);
        // if(spriteRenderer.sprite == knockeddown2&&randint<40){
        //     action = "getUp";
        //     spriteRenderer.sprite = getup;
        // }else if (spriteRenderer.sprite == getup&&randint<80){
        //     spriteRenderer.sprite = getup2;
        // }else if(spriteRenderer.sprite == getup2){
        //     health = 210;
        //     spriteRenderer.sprite = normal;
        //     count = 0;
        //     action = "";
        //     lm.action = "";
        //     mar.action = "";
        //     mar.count = 0;
        // }else{
        //     action = "wait";
        //     spriteRenderer.sprite = knockeddown2;
        //     mar.action = "ecount";
        // }
        knockDown();
    }
        

    public override void win(){
        action = "win";
        mar.action = "ko";
        mar.enwin = true;
        //mar.action = "wait";
        mar.enwin = true;
        if(count == 0){
            spriteRenderer.sprite = victory1;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = victory2;
            count++;  
        }else if (count == 2){
            spriteRenderer.sprite = victory3;
            count++;  
        }else if (count == 3){
            spriteRenderer.sprite = victory4;
            count++;  
        }else if (count == 4){
            spriteRenderer.sprite = victory5;
            count++;  
        }else if (count == 5){
            spriteRenderer.sprite = victory6;
            count++;  
        }else if (count == 6){
            spriteRenderer.sprite = victory7;
            count = 0;
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
        spriteRenderer.sprite = knockout7;
    }

    public override int pointsForKnockout()
    {
        return 999999;
    }
}
