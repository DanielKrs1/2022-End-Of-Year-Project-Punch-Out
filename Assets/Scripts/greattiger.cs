using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greattiger : enemy
{
    // Start is called before the first frame update
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normalup;
    public Sprite normalup2;
    public Sprite normalup3;
    public Sprite prepunch;
    public Sprite prepunch2;
    public Sprite prepunch3;
    public Sprite prepunch4;
    public Sprite punch;
    public Sprite preupper;
    public Sprite midupper;
    public Sprite uppercut;
    public Sprite special1;
    public Sprite special2;
    public Sprite special3;
    public Sprite special4;
    public Sprite blockhigh;
    public Sprite blockhigh2;
    public Sprite blocklow;
    public Sprite blocklow2;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite hitlow;
    public Sprite hitlow2;
    public Sprite dodgeHit;
    public Sprite knockdown1;
    public Sprite knockdown2;
    public Sprite getup1;
    public Sprite getup2;

    public bool punching = false;
    public bool blockinglow = true;
    public bool blockinghigh = false;
    public bool specialing = false;
    public bool onehit = false;
    public bool counter = false;
    public littlemac lm;
    public mario mar;
    public Timer tim;
    public Health heal;
    public int time;
    public Transform for1;
    public Transform for2;
    public Transform for3;

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 310;

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
        tim = GameObject.Find("canvas").GetComponent("Timer") as Timer;
        heal = GameObject.Find("health2").GetComponent("Health") as Health;
        for1 = GameObject.Find("one").GetComponent("Transform") as Transform;
        for2 = GameObject.Find("two").GetComponent("Transform") as Transform;
        for3 = GameObject.Find("three").GetComponent("Transform") as Transform;
        blockinglow = true;
        blockinghigh = true;
    }

    // Update is called once per frame
    int temp;
    void FixedUpdate()
    {
        heal.scale(health/310f);
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
        time = tim.getTime();
        if(frame%10==0&&action.Length<2){
            var randint = Random.Range(0, 100);
            // if(randint == 1){
            //     action = "special";
            // }else if(randint<2){
            //     action = "upper";
            // }else if(randint<6){
            //     action = "normalPunch";
            // }
            if(time>420){
                action = "upper";
            }else if(time >300){
                if(randint < 20){
                    action = "normalPunch";
                }
            }else if(time >100){
                action = "upper";
            }else{
                if(randint < 20){
                    action = "normalPunch";
                }
            }
        }

        if(time == 10){//time==230||time==300||time==430){
            action = "special";
        }

        if(frame%10==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
            }else if(action.Equals("special")){
                specialing = true;
                special();
                rb.MovePosition(rb.position+movement);//*Time.deltaTime);  
            }else if(action.Equals("blockLow")){
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
                if(temp == 1){
                    spriteRenderer.sprite = normalup2;
                    temp++;
                }else if(temp == 2){
                    spriteRenderer.sprite = normalup3;
                    temp = 0;
                }else{
                    spriteRenderer.sprite = normalup;
                    temp++;
                }
                onehit = false;
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
            hits = 1;
            //specialing = true;
            spriteRenderer.sprite = prepunch;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = prepunch2;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = prepunch3;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = prepunch4;
            count++;
        }else if( count == 4){
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
            }if(lm.blocking){
                counter = true;
                hits = 5;
            }
        }else if (count < 8){
            specialing = false;
            punching = false;
            //counter = false;
            count++;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 8){
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
            hits = 7;
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = preupper;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = midupper;
            blockinglow = false;
            specialing = false;
            count++;
        }else if (count == 2){
            punching = true;
            blockinglow = true;
            spriteRenderer.sprite = uppercut;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=15f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else if(lm.blockBroken){
                lm.health-=7f;
            }
        }else if (count < 6){
            specialing = false;
            punching = false;
            counter = true;
            count++;      
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

    void special(){
        if(count == 0){
            spriteRenderer.sprite = special1;
            count++;
        }else if(count>=64){
            counter = true;
            onehit = true;
            specialing = false;
            punching = false;
            blockinglow = true;
            count++;
            if(count>69){
               spriteRenderer.sprite = normal; 
               count = 0;
               action = ""; 
               onehit = false;
            }
            movement.x = 0f;
            movement.y = 0f;
        }else if(count%16 == 0){
            specialing = true;
            punching = false;
            counter = false;
            count++; 
            spriteRenderer.sprite = special4;
            rb.position = for1.position;
            count++;
        }else if (count%16 == 4){
            specialing = true;
            spriteRenderer.sprite = special4;
            rb.position = for2.position;
            count++;
        }else if (count%16 == 8){
            specialing = false;
            punching = false;
            count++; 
            spriteRenderer.sprite = special3;
            rb.position = for3.position;
        }else if (count%16 == 12){
            count++;
            punching = true;
            specialing = true;
            spriteRenderer.sprite = punch;
            rb.position = fp;
            if(!lm.blocking){
                lm.health -=13;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.rb.position = lm.fp;
                count-=16;
            }
        }else{
            movement.x = 0f;
            movement.y = 0f;
            count++;
        }
    }

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
        specialing = false;
        lowhits++;
        action = "hitLow";
        if(count <= 2){
            spriteRenderer.sprite = hitlow;
            count++;
        }else if( count < 4){
            spriteRenderer.sprite = hitlow2;
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
            spriteRenderer.sprite = dodgeHit;
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
        onehit = false;
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
            spriteRenderer.sprite = getup1;
        }else if (spriteRenderer.sprite == getup1&&randint<80){
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
        //mar.action = "wait";
        mar.enwin = true;
        // if(count <= 3){
        //     spriteRenderer.sprite = victory;
        //     count++;
        // }else{
        //     spriteRenderer.sprite = victory2;
        // }
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
        return 10000;
    }
}
