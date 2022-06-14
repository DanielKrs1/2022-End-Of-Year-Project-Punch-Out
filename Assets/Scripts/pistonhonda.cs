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
    public Timer tim;
    public Health heal;
    public int time;
    public Transform for1;
    public Transform for2;
    public Transform for3;
    public Transform for4;
    public Transform for5;

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    public SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 330;

    public int lowhits = 0;
    public int highhits = 0;

    public int timesdown = 0;
    public int punchcount = 0;
    public int hookupper = 0;

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
        for5 = GameObject.Find("five").GetComponent("Transform") as Transform;
        blockinglow = true;
        blockinghigh = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        heal.scale(health/330f);
        if(lowhits == 1){
            blockinglow = true;
            blockinghigh = false;
            lowhits = 0;
        }
        if(highhits == 1){
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
            if(randint<40){
                action = "normalPunch";
            }
        }

        time = tim.getTime();

        if(time == 100|| time == 240||time==320){
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
                    }else if(spriteRenderer.sprite == normal3){
                        spriteRenderer.sprite = normal4;
                    }else if(spriteRenderer.sprite == normal4){
                        spriteRenderer.sprite = normal5;
                    }else if(spriteRenderer.sprite == normal5){
                        spriteRenderer.sprite = normal6;
                    }else{
                        spriteRenderer.sprite = normalup;
                    }
                    specialing = false;
                    counter = false;
                }                
            }  
             
        }
        movement.y = 0F;
        movement.x = 0F;
        frame++;  
    }

    public int count = 0;
    private bool temp1;
    private bool temp2;
    void normalPunch(){
        if(count == 0){
            punchcount++;
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
                lm.health -=100F/9;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
                lm.action = "hit";
                lm.rb.position = lm.fp;
            }
        }else if (count < 8){
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
                blockinghigh = true;
                blockinglow = true;  
                if(punchcount == 1){
                    count = 0;
                    action = "normalPunch";
                }else if (punchcount == 2){
                    punchcount = 0;
                    var randint = Random.Range(0,100);
                    if(randint<75){
                        action = "hook";
                    }else{
                        action = "";
                    }
                }
        }
    }

    void hook(){
        if(count == 0){
            hookupper++;
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
                lm.health -=100F/9;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count < 7){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            blockinghigh = false;
            blockinglow = false;     
        }else if(count == 7){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";  
                blockinghigh = temp1;
                blockinglow = temp2;  
                if(hookupper == 1){
                    count = 0;
                    var  randint = Random.Range(0,100);
                    if(randint<75){
                        action = "hook";
                    }else{
                        action = "upper";
                    
                    }
                }else if (hookupper == 2){
                    punchcount = 0;
                    hookupper = 0;
                    var randint = Random.Range(0,100);
                    if(randint<75){
                        action = "";
                        
                    }else if(randint<90){
                        action = "upper";
                    }else{
                        action = "hook";
                    }
                }
                    
        }
    }

    void upper(){
        if(count == 0){
            hookupper++;
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
                lm.health -=20F;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.action = "hit";
                lm.hit();
                lm.rb.position = lm.fp;
            }else if(lm.blocking){
                lm.health -=100F/9;
                if(lm.health<=0){
                    lm.knockeddown();
                }
            }
        }else if (count < 8){
            specialing = false;
            punching = false;
            counter = true;
            count++;      
            temp1 = blockinghigh;
            temp2 = blockinglow;
            blockinghigh = false;
            blockinglow = false;
        }else if(count == 8){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
                blockinghigh = true;
                blockinglow = true;
                if(hookupper == 1){
                    count = 0;
                    var  randint = Random.Range(0,100);
                    if(randint<75){
                        action = "hook";
                    }else{
                        action = "upper";
                    
                    }
                }else if (hookupper == 2){
                    punchcount = 0;
                    hookupper = 0;
                    var randint = Random.Range(0,100);
                    if(randint<75){
                        action = "";
                        
                    }else if(randint<90){
                        action = "upper";
                    }else{
                        action = "hook";
                    }
                }
        }
    }

    void special(){
        onehit = false;
        if(count <= 0){
            //movement.y = moveSpeed;
            rb.position = for1.position;
            count++;
        }else if(count <= 2){
            //movement.y = moveSpeed;
            rb.position = for2.position;
            count++;
        }else if(count <= 4){
            //movement.x = moveSpeed;
            spriteRenderer.sprite = forspecial3;
            rb.position = for3.position;
            count++;
        }else if (count <=6){
            spriteRenderer.sprite = normal;
            // movement.x = -1*moveSpeed;
            rb.position = for4.position;
            spriteRenderer.sprite = forspecial;
            count++;
        }else if(count <= 8){
            spriteRenderer.sprite = normal;
            //movement.x = moveSpeed;
            rb.position = for3.position;
            count++;
        }else if(count <= 10){
            spriteRenderer.sprite = normal;
            // movement.x = -1*moveSpeed;
            rb.position = for5.position;
            spriteRenderer.sprite = forspecial3;
            count++;
        }else if(count <= 12){
            rb.position = for3.position;
            count++;
        }else if(count <= 14){
            rb.position = for2.position;
            onehit = true;
            blockinglow = false;
            //movement.y = -1*moveSpeed;
            count++;
        }else if(count == 15){
            rb.position = for1.position;
            count = 16;
            onehit = false;
            blockinghigh = true;
            blockinglow = true;
            special();
        }
        if(count==48){
            counter = false;
            specialing = false;
            punching = false;
            blockinglow = true;
            onehit = false;
            blockinghigh = false;
            spriteRenderer.sprite = normal;
            count = 0;
            action = ""; 
        }else if(count%8 == 0){
            specialing = false;
            punching = false;
            counter = true;
            blockinglow = false;
            count++; 
            spriteRenderer.sprite = forspecial;
        }else if (count%8 == 2){
            specialing = true;
            spriteRenderer.sprite = forspecial2;
            count++;
            punching = true;
            blockinglow = true;
            spriteRenderer.sprite = up;
            if(!lm.blocking && !lm.dodging){
                lm.health -=8F;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count%8 == 4){
            specialing = false;
            punching = false;
            counter = true;
            blockinglow = false;
            count++; 
            spriteRenderer.sprite = forspecial3;
        }else if (count%8 == 6){
            spriteRenderer.sprite = forspecial4;
            count++;
            punching = true;
            specialing = true;
            blockinglow = true;
            if(!lm.blocking && !lm.dodging){
                lm.health -=8F;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else{
            count++;
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
            counter = false;
            stunned = false;
            if(punchcount == 1){
                count = 0;
                action = "normalPunch";
            }else if (punchcount == 2){
                punchcount = 0;
                var randint = Random.Range(0,100);
                if(randint<75){
                    action = "hook";
                }else{
                    action = "";
                }
            }
            if(hookupper == 1){
                count = 0;
                var  randint = Random.Range(0,100);
                if(randint<75){
                    action = "hook";
                }else{
                    action = "upper";
                
                }
            }else if (hookupper == 2){
                punchcount = 0;
                hookupper = 0;
                var randint = Random.Range(0,100);
                if(randint<75){
                    action = "";
                    
                }else if(randint<90){
                    action = "upper";
                }else{
                    action = "hook";
                }
            }
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
        specialing = true;
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
        mar.action = "ko";
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
        spriteRenderer.sprite = down;
    }

    public override int pointsForKnockout()
    {
        return 999999;
    }
}
