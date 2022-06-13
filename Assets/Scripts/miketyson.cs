using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miketyson : enemy
{
    // Start is called before the first frame update
    public Sprite move1;
    public Sprite move2;
    public Sprite move3;
    public Sprite move4;
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normal4;
    public Sprite prepunch;
    public Sprite punch;
    public Sprite prehook;
    public Sprite midhook;
    public Sprite endhook;
    public Sprite stunned1;
    public Sprite hitlow;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite knockdown1;
    public Sprite knockdown2;
    public Sprite knockdown3;
    public Sprite getup;
    public Sprite preupper;
    public Sprite preupper2;
    public Sprite startupper;
    public Sprite midupper;
    public Sprite endupper;
    public Sprite win1;
    public Sprite win2;
    public Sprite win3;
    public Sprite blockhigh;
    public Sprite blocklow;

    public bool punching = false;
    public bool blockinglow = true;
    public bool blockinghigh = true;
    public bool specialing = false;
    public bool onehit = false;
    public bool counter = false;
    public littlemac lm;
    public mario mar;
    public Timer tim;
    public Health heal;
    public int time;

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 400;

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
        heal.scale(health/400f);
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
            // /*if(randint == 1){
            //     action = "special";
            // }else */if(randint<2){
            //     action = "upper";
            // }else if(randint<6){
            //     action = "right";
            // }else if (randint<8){
            //     action = "normalPunch";
            // }
            if(time>330){
                if(randint<60){
                    action = "right";
                }else if(randint<65){
                    action = "upper";
                }
            }else if(time>300){
                action = "normalPunch";
            }else if(time>130){
                if(randint<75){
                    action = "right";
                }
            }else if(time>0){
                if(randint<75){
                    action = "upper";
                }
            }
        }

        if(frame%10==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
            }else if (action.Equals("right")){
                hook();
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
                if(spriteRenderer.sprite == normal){
                    spriteRenderer.sprite = normal2;
                }else if(spriteRenderer.sprite == normal2){
                    spriteRenderer.sprite = normal3;
                }else if (spriteRenderer.sprite == normal3){
                    spriteRenderer.sprite = normal4;
                }else{
                    spriteRenderer.sprite = normal;
                }
            }  
             
        }
         
        frame++;  
    }

    public int count = 0;
    private bool temp1;
    private bool temp2;
    void normalPunch(){
        if(count == 0){
            //specialing = true;
            spriteRenderer.sprite = prepunch;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if (count == 1){
            punching = true;
            spriteRenderer.sprite = punch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=17f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }if(lm.blocking){
                blockinglow = false;
            }
        }else if (count < 8){
            specialing = false;
            punching = false;
            //counter = true;
            count++;
        }else if(count == 8){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";  
                blockinghigh = true;
                blockinglow = true;  
        }
    }

    void hook(){
        if(count == 0){
            temp1 = blockinghigh;
            temp2 = blockinglow;
            spriteRenderer.sprite = prehook;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = midhook;
            count++;
        }else if (count ==2 ){
            punching = true;
            spriteRenderer.sprite = endhook;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=19f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count < 9){
            hits = 2;
            specialing = false;
            punching = false;
            counter = true;
            count++;
            blockinghigh = false;
            blockinglow = true;     
        }else if(count == 9){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";  
                blockinghigh = temp1;
                blockinglow = temp2;  
        }
    }

    void upper(){
        if(count == 0){
            specialing = true;
            spriteRenderer.sprite = preupper;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = preupper2;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = startupper;
            count++;
        }else if (count == 3){
            spriteRenderer.sprite = midupper;
            count++;
        }else if (count ==4 ){
            punching = true;
            spriteRenderer.sprite = endupper;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=100f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }if(lm.blocking){
                lm.health -=90f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
            }
        }else if (count < 11){
            hits = 2;
            specialing = false;
            punching = false;
            counter = true;
            count++;      
            temp1 = blockinghigh;
            temp2 = blockinglow;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 11){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
                blockinghigh = true;
                blockinglow = true;
        }
    }

    void special(){
        /*if(count==80){
            counter = false;
            specialing = false;
            punching = false;
            blockinglow = true;
            spriteRenderer.sprite = normal;
            count = 0;
            action = ""; 
        }else if(count%16 == 0){
            specialing = false;
            punching = false;
            counter = true;
            blockinglow = false;
            count++; 
            spriteRenderer.sprite = forspecial;
        }else if (count%16 == 4){
            specialing = true;
            spriteRenderer.sprite = forspecial2;
            count++;
            punching = true;
            blockinglow = true;
            spriteRenderer.sprite = up;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count%16 == 8){
            specialing = false;
            punching = false;
            counter = true;
            blockinglow = false;
            count++; 
            spriteRenderer.sprite = forspecial3;
        }else if (count%16 == 12){
            spriteRenderer.sprite = forspecial4;
            count++;
            punching = true;
            specialing = true;
            blockinglow = true;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }*/
    }

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
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = normal;
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
            stunned = false;
        }
    }

    public override void knockDown(){
        stunned = false;
        hits = 7;
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockdown1;
            count++;
        }else if (count <=5){
            spriteRenderer.sprite = knockdown2;
            count++;
        }else{
            spriteRenderer.sprite = knockdown3;
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
        mar.action = "wait";
        var randint = Random.Range(0, 100);
        if(spriteRenderer.sprite == knockdown3&&randint<40){
            action = "getUp";
            spriteRenderer.sprite = getup;
        }else if (spriteRenderer.sprite == getup&&randint<80){
            health = 210;
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
            lm.action = "";
            mar.action = "";
            mar.count = 0;
        }else{
            action = "wait";
            spriteRenderer.sprite = knockdown3;
            mar.action = "ecount";
        }
    }
        

    public override void win(){
        action = "win";
        //mar.action = "wait";
        mar.enwin = true;
        if(count == 0){
            spriteRenderer.sprite = win1;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = win2;
            count++;
        }else{
            spriteRenderer.sprite = win3;
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
        spriteRenderer.sprite = knockdown3;
    }

    public override int pointsForKnockout()
    {
        return 5000;
    }
}
