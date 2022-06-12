using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistonhonda : enemy
{
    // Start is called before the first frame update
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normal4;
    public Sprite normal5;
    public Sprite normal6;
    public Sprite normalr;
    public Sprite normalr2;
    public Sprite normalr3;
    public Sprite normalup;
    public Sprite normalup2;
    public Sprite normalup3;
    public Sprite blockup;
    public Sprite blocklow;
    public Sprite dodge;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite dodgeHit;
    public Sprite hitlow;
    public Sprite preright;
    public Sprite clueright;
    public Sprite punchright;
    public Sprite followright;
    public Sprite preup;
    public Sprite clueup;
    public Sprite midup;
    public Sprite midup2;
    public Sprite up;
    public Sprite punchclue;
    public Sprite punchclue2;
    public Sprite punch;
    public Sprite falldown;
    public Sprite falldown2;
    public Sprite down;
    public Sprite getup;
    public Sprite forspecial;
    public Sprite forspecial2;
    public Sprite forspecial3;
    public Sprite forspecial4;

    public bool punching = false;
    public bool blockinglow = true;
    public bool blockinghigh = true;
    public bool specialing = false;
    public bool onehit = false;
    public bool counter = false;
    public littlemac lm;
    public mario mar;

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 1;//210;

    public int lowhits = 0;
    public int highhits = 0;

    public int timesdown = 2;
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
        blockinglow = true;
        blockinghigh = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            if(randint == 1){
                action = "special";
            }else if(randint<2){
                action = "upper";
            }else if(randint<6){
                action = "right";
            }else if (randint<8){
                action = "normalPunch";
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
                if(spriteRenderer.sprite == normalup){
                    spriteRenderer.sprite = normalup3;
                }else if(spriteRenderer.sprite == normalup3){
                    spriteRenderer.sprite = normal3;
                }else{
                    spriteRenderer.sprite = normalup;
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
            specialing = true;
            spriteRenderer.sprite = normal2;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = punchclue;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = punchclue2;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = normalup3;
            count++;
        }else if (count == 4){
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
        }else if (count < 11){
            specialing = false;
            punching = false;
            counter = true;
            count++;
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

    void hook(){
        if(count == 0){
            temp1 = blockinghigh;
            temp2 = blockinglow;
            spriteRenderer.sprite = preright;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = clueright;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = punchright;
            count++;
        }else if (count ==3 ){
            punching = true;
            spriteRenderer.sprite = followright;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count < 10){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            blockinghigh = false;
            blockinglow = true;     
        }else if(count == 10){
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
            spriteRenderer.sprite = preup;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = clueup;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = midup;
            count++;
        }else if (count == 3){
            spriteRenderer.sprite = midup2;
            count++;
        }else if (count ==4 ){
            punching = true;
            spriteRenderer.sprite = up;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }
        }else if (count < 10){
            specialing = false;
            punching = false;
            counter = true;
            count++;      
            temp1 = blockinghigh;
            temp2 = blockinglow;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 10){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
                blockinghigh = true;
                blockinglow = true;
        }
    }

    void special(){
        if(count==80){
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
        }
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
            spriteRenderer.sprite = blockup;
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
            spriteRenderer.sprite = dodgeHit;
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
            spriteRenderer.sprite = falldown;
            count++;
        }else if (count <=5){
            spriteRenderer.sprite = falldown2;
            count++;
        }else{
            spriteRenderer.sprite = down;
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
        if(spriteRenderer.sprite == down&&randint<40){
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
            spriteRenderer.sprite = down;
            mar.action = "ecount";
        }
    }
        

    public override void win(){
        action = "win";
        mar.action = "wait";
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
        spriteRenderer.sprite = down;
    }
}
