using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vonkaiser : enemy
{
    // Start is called before the first frame update
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normal4;
    public Sprite normalup;
    public Sprite normalup2;
    public Sprite normalup3;
    public Sprite punchclue;
    public Sprite punch;
    public Sprite preupper;
    public Sprite upperclue;
    public Sprite midupper;
    public Sprite upperend;
    public Sprite blocklow;
    public Sprite blocklow2;
    public Sprite blockhigh;
    public Sprite blockhigh2;
    public Sprite dodgeHit;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite hitlow;
    public Sprite hitlow2;
    public Sprite knockeddown;
    public Sprite knockeddown2;
    public Sprite getup;
    public Sprite getup2;
    public Sprite dodge;
    public Sprite dodge2;

    public bool punching = false;
    public bool blockinglow = true;
    public bool blockinghigh = false;
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
    public int health = 310;

    public int lowhits = 0;
    public int highhits = 0;
    public int secondupper = 0;

    public int timesdown = 0;
    public Health heal;
    void Start()
    {
        heal.scale(health/310f);
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
        blockinghigh = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heal.scale(health/310f);
        if(lowhits == 2){
            blockinglow = true;
            blockinghigh = false;
            lowhits = 0;
        }
        if(highhits == 2){
            blockinghigh = true;
            blockinglow = false;
            highhits = 0;
        }
        //var rand = new Random();
        moveSpeed = 1f;
        //lastPos = transform.position;
        if(frame == 119){
            frame = 0;
        }
        if(frame%10==0&&action.Length<2){
            var randint = Random.Range(0, 100);
            if(randint<45){
                action = "normalPunch";
            }
        }

        if(frame%10==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
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
                if(blockinghigh){
                    if(spriteRenderer.sprite == normalup){
                        spriteRenderer.sprite = normalup2;
                    }else if(spriteRenderer.sprite == normalup2){
                        spriteRenderer.sprite = normalup3;
                    }else{
                        spriteRenderer.sprite = normalup;
                    }    
                }else{
                    if(spriteRenderer.sprite == normal){
                        spriteRenderer.sprite = normal2;
                    }else if(spriteRenderer.sprite == normal2){
                        spriteRenderer.sprite = normal3;
                    }else{
                        spriteRenderer.sprite = normal;
                    }  
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
            spriteRenderer.sprite = normal;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = normal2;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = normalup;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = punchclue;
            count++;
        }else if (count == 4){
            spriteRenderer.sprite = normalup3;
            count++;
        }else if( count == 5){
            punching = true;
            spriteRenderer.sprite = punch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=100.0F/9;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count < 12){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 12){
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
            secondupper++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = preupper;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = upperclue;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = midupper;
            count++;
        }else if (count ==3 ){
            punching = true;
            spriteRenderer.sprite = upperend;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=100.0F/7;
                if(lm.health<=0F){
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
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 10){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                if(secondupper <2){
                    secondupper++;     
                }else{
                    secondupper = 0;
                }
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
            if(secondupper==1){
                action = "upper";
            }else{
                secondupper =0;
            }
        }
    }

    public override void knockDown(){
        stunned = false;
        hits = 7;
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockeddown;
            count++;
        }else{
            spriteRenderer.sprite = knockeddown2;
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
        action = "getUp";
        var randint = Random.Range(0, 100);
        if(spriteRenderer.sprite == knockeddown2&&randint<40){
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
            action = "upper";
        }else{
            action = "wait";
            spriteRenderer.sprite = knockeddown2;
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
        spriteRenderer.sprite = knockeddown2;
    }
    public override int pointsForKnockout()
    {
        return 8000;
    }
}
