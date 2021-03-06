using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baldbull : enemy
{
    // Start is called before the first frame update
    public Sprite move1;
    public Sprite move2;
    public Sprite move3;
    public Sprite move4;
    public Sprite move5;
    public Sprite move6;
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite prepunch;
    public Sprite prepunch2;
    public Sprite prepunch3;
    public Sprite punch;
    public Sprite prehook;
    public Sprite prehook2;
    public Sprite prehook3;
    public Sprite midhook;
    public Sprite righthook;
    public Sprite preupper1;
    public Sprite preupper2;
    public Sprite midupper;
    public Sprite uppercut;
    public Sprite blockhigh;
    public Sprite blocklow;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite hitlow;
    public Sprite stunned1;
    public Sprite stunned2;
    public Sprite knockdown1;
    public Sprite knockdown2;
    public Sprite knockdown3;
    public Sprite getup;
    public Sprite getup2;
    public Sprite victory1;
    public Sprite victory2;

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

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 350;
    public Transform for1;
    public Transform for2;
    public Transform for3;
    public Transform for4;

    public int lowhits = 0;
    public int highhits = 0;

    public int timesdown = 0;
    int tempcount;
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
        for4 = GameObject.Find("four").GetComponent("Transform") as Transform;
        blockinglow = true;
        blockinghigh = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heal.scale(health/350f);
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
            }else */if(randint<8){
                action = "upper";
            }else if (randint<16){
                action = "hook";
            }else if(randint<30){
                action = "normalPunch";
            }
        }

        time = tim.getTime();

        if(time == 200||time == 230|| time ==430){
            count = 0;
            action = "special";
        }

        if(frame%10==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
            }else if (action.Equals("hook")){
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
                if(tempcount == 1){
                    spriteRenderer.sprite = normal2;
                    tempcount = 2;
                }else if(tempcount == 2){
                    spriteRenderer.sprite = normal3;
                    tempcount = 0;
                }else{
                    spriteRenderer.sprite = normal;
                    tempcount = 1;
                }
                counter = false;
                stunned = false;
                blockinghigh = true;
                blockinglow = true;
                onehit = false;
            }  
             
        }
        movement.x = 0f;
        movement.y = 0f;
         
        frame++;  
    }

    public int count = 0;
    private bool temp1;
    private bool temp2;
    void normalPunch(){
        if(count == 0){
            specialing = true;
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
        }else if( count == 3){
            punching = true;
            spriteRenderer.sprite = punch;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=15f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count < 7){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 7){
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
            //specialing = true;
            spriteRenderer.sprite = prehook;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
        }else if(count == 1){
            spriteRenderer.sprite = prehook2;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = prehook3;
            count++;
        }else if (count == 3){
            blockinghigh = false;
            spriteRenderer.sprite = midhook;
            count++;
        }else if( count == 4){
            blockinghigh = true;
            punching = true;
            spriteRenderer.sprite = righthook;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=16f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count < 8){
            hits = 3;
            specialing = false;
            punching = false;
            counter = true;
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
            temp1 = blockinghigh;
            temp2 = blockinglow;
            specialing = true;
            spriteRenderer.sprite = preupper1;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = preupper2;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = midupper;
            count++;
        }else if (count ==3 ){
            punching = true;
            spriteRenderer.sprite = uppercut;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=24f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }if(lm.blocking){
                lm.health -= 16f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
            }
        }else if (count < 7){
            hits =7;
            specialing = false;
            punching = false;
            counter = true;
            count++;      
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 7){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
                blockinghigh = true;
                blockinglow = true;
        }
    }

    void special(){
        if(count < 4){
            punching = false;
            specialing= true;
            if(spriteRenderer.sprite = preupper1){
                spriteRenderer.sprite = preupper2;
            }else{
                spriteRenderer.sprite = preupper1;
            }
            //movement.y = moveSpeed;
            if(count == 0){
                rb.position = for1.position;
            }else if (count == 1){
                rb.position = for2.position;
            }else if (count == 2){
                rb.position = for3.position;
            }else if (count == 3){
                rb.position = for4.position;
            }
            count++;
        }else if(count<7){
            if(spriteRenderer.sprite ==preupper1){
                spriteRenderer.sprite = preupper2;
            }
            count++;
        }else if (count<11){
            if(spriteRenderer.sprite = preupper1){
                spriteRenderer.sprite = preupper2;
            }else{
                spriteRenderer.sprite = preupper1;
            }
            if(count == 10){
                rb.position = fp;
            }else if (count == 9){
                rb.position = for1.position;
            }else if (count == 8){
                rb.position = for2.position;
            }else if (count == 7){
                rb.position = for3.position;
            }
            if(count == 10){
                counter = true;
                blockinglow = false;
                specialing = false;
                onehit = true;
            }else{
                counter = false;
                blockinglow = true;
                specialing = true;
                onehit = false;
            }
            count++;
        }else if(count == 11){
            onehit = false;
            counter = false;
            blockinglow = true;
            spriteRenderer.sprite = uppercut;
            punching = true;
            if(!lm.dodging){
                lm.health -=100f;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else{
                count = 0;
                punching = false;
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
            if(spriteRenderer.sprite == stunned1){
                spriteRenderer.sprite = stunned2;
            }else{
                spriteRenderer.sprite = stunned1;
            }
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
        stunned = false;
        onehit = false;
        hits = 7;
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockdown1;
            count++;
        }else if(count<=5){
            count++;
            spriteRenderer.sprite = knockdown2;
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
        specialing = true;
        mar.action = "wait";
        action = "getUp";
        var randint = Random.Range(0, 100);
        if(spriteRenderer.sprite == knockdown3&&randint<40){
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
            spriteRenderer.sprite = knockdown3;
            mar.action = "ecount";
        }
    }
        

    public override void win(){
        action = "win";
        //mar.action = "wait";
        mar.enwin = true;
        mar.action = "ko";
        mar.enwin = true;
        if(count <= 3){
            spriteRenderer.sprite = victory1;
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
        spriteRenderer.sprite = knockdown3;
    }

    public override int pointsForKnockout()
    {
        return 999999;
    }
}
