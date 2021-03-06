using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littlemac : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1f;

    public Sprite normal;
    public Sprite normal2;
    public Sprite leftPunch;
    public Sprite leftPunchMid;
    public Sprite leftPunchStart;
    public Sprite rightPunch;
    public Sprite rightPunchMid;
    public Sprite rightPunchStart;
    public Sprite rightUppercutStart;
    public Sprite rightUppercutMid;
    public Sprite rightUppercutEnd;
    public Sprite leftUppercutStart;
    public Sprite leftUppercutMid;
    public Sprite leftUppercutEnd;
    public Sprite blockStart;
    public Sprite block;
    public Sprite blockBroken;
    public Sprite dodgeLeft;
    public Sprite dodgeLeftStart;
    public Sprite dodgeLeftEnd;
    public Sprite dodgeRight;
    public Sprite dodgeRightStart;
    public Sprite dodgeRightEnd;
    public Sprite hit1;
    public Sprite hit2;
    public Sprite knockedDown;
    public Sprite win1;
    public Sprite win2;

    public enemy en;
    public mario mar;
    public Health heal;
    public PointKeeper pk;

    public float health = 100.0F;

    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "wait";
    private SpriteRenderer spriteRenderer;
    public Vector2 fp;

    public bool punchlow = false;
    public bool punchhigh = false;
    public bool dodging = false;
    public bool blocking = false;
    
    public int knockdowned;
    public int countzx;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.sprite == null){
            spriteRenderer.sprite = normal;
        }
        fp = rb.position;
        bool found = false;
        moveSpeed = 1f;
        en = GameObject.Find("enemy").GetComponent("glassjoe") as enemy; 
        if(en!=null){
            en = GameObject.Find("enemy").GetComponent("glassjoe") as glassjoe;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("vonkaiser") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("vonkaiser") as vonkaiser;
            found = true;
        }else if(found == false){
            en = GameObject.Find("enemy").GetComponent("pistonhonda") as enemy;
        }
        if(en != null&&found==false){
            en = GameObject.Find("enemy").GetComponent("pistonhonda") as pistonhonda;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("donflamenco") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("donflamenco") as donflamenco;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("kinghippo") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("kinghippo") as kinghippo;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("greattiger") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("greattiger") as greattiger;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("baldbull") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("baldbull") as baldbull;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("sodapopinski") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("sodapopinski") as sodapopinski;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("mrsandman") as enemy;
        }
        if (en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("mrsandman") as mrsandman;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("supermachoman") as enemy;
        }
        if(en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("supermachoman") as supermachoman;
            found = true;
        }else if(found==false){
            en = GameObject.Find("enemy").GetComponent("miketyson") as enemy;
        }
        if(en!=null&&found==false){
            en = GameObject.Find("enemy").GetComponent("miketyson") as miketyson;
            found = true;
        }
        mar = GameObject.Find("mario").GetComponent("mario") as mario;
        heal = GameObject.Find("health1").GetComponent("Health") as Health;
        pk = GameObject.Find("canvas").GetComponent("PointKeeper") as PointKeeper;
    }

    // Update is called once per frame
    public int fram2;
    void FixedUpdate()
    {
        heal.scale(health/100F);
        if(health<=0){
            heal.scale(0);
            if(frame%5==0){
                knockeddown();    
            }
            
        }
        moveSpeed = 1f;
        //lastPos = transform.position;
        if(frame == 119){
            frame = 0;
        }
        if(fram2 == 119){
            fram2 = 0;
        }
        
        if(action.Length<1){
            if (Input.GetKey(KeyCode.RightArrow))
            {
                action = "dodgeRight";
                rightDodge();
                frame = 11;
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                action = "dodgeLeft";  
                leftDodge();
                frame = 11;     
            }else if (Input.GetKey(KeyCode.UpArrow))
            {
                if(Input.GetKey(KeyCode.Z)){
                    action = "upPunchLeft";
                    uppercutLeft();
                    frame = 11;
                }else if(Input.GetKey(KeyCode.X)){
                    action = "upPunchRight";
                    uppercutRight();
                    frame = 11;
                }
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                action = "block";
                bloc();
                frame = 11;
            }else if(Input.GetKey(KeyCode.Z)){
                action = "punchLeft";
                punchLeft();
                frame = 11;
            }else if(Input.GetKey(KeyCode.X)){
                action = "punchRight";
                punchRight();
                frame = 11;
            }
        }

        if(frame%5==0){
            if(action.Equals("dodgeLeft")){
                leftDodge();
            }else if(action.Equals("dodgeRight")){
                rightDodge();
            }else if(action.Equals("upPunchLeft")){
                uppercutLeft();
            }else if(action.Equals("upPunchRight")){
                uppercutRight();
            }else if(action.Equals("block")){
                bloc();
            }else if(action.Equals("punchLeft")){
                punchLeft();
            }else if(action.Equals("punchRight")){
                punchRight();
            }else if (action.Equals("hit")){
                hit();
            }else if (action.Equals("knockeddown")){
                knockeddown();
            }else if (action.Equals("win")){
                win();
            }else if (action.Equals("knockedout")){

            }else if (en.getTimesDown()>=3){
                win();
                en.setKnockedOut();
                //en.spriteRenderer.sprite = en.knockdown3;
                en.setAction("wait");
                mar.action = "tko";
            }else if (health <= 0.0){
                 knockeddown();
            }else if (action.Equals("wait")){

            }else{
                if(spriteRenderer.sprite == normal){
                    spriteRenderer.sprite = normal2;
                }else{
                    spriteRenderer.sprite = normal;
                }
                movement.x = 0f;
                movement.y = 0f;
                rb.position = fp;
            }  
            rb.MovePosition(rb.position+movement);//*Time.deltaTime);  
            if(health<=0f){
                knockeddown();
            }            
        }
        if(en.isStunned() && fram2%37==0){
            en.changeHits();
            if(en.getHits()<=0){
                en.setAction("");
                //en.spriteRenderer.sprite = en.normal;
                en.setHits(7);
                en.setStunned(false);
                en.setCounter(false);
                fram2 = 0;
            }
        }
        movement.x = 0f;
        movement.y = 0f;
        frame++;  
        fram2++;
    }

    public bool back = false;
    int dodgecount;
    void leftDodge(){
        dodging = true;
        action = "dodgeLeft";
        if(dodgecount ==0){
            spriteRenderer.sprite = dodgeLeftStart;
            dodgecount++;
        }else if (dodgecount == 1){
            spriteRenderer.sprite = dodgeLeft;
            dodgecount++;
            movement.x = -1*moveSpeed/2;
            movement.y = 0f;
        }else if (dodgecount == 2){
            spriteRenderer.sprite = dodgeLeftEnd;
            movement.x = -1*moveSpeed/2;    
            dodgecount++;
            movement.y = 0f;
        }else if (dodgecount == 3){
            spriteRenderer.sprite = dodgeLeft;
            movement.x = moveSpeed/2;
            movement.y = 0f;
            dodgecount++;
        }else if (dodgecount == 4){
            spriteRenderer.sprite = dodgeLeftStart;
            movement.x = moveSpeed/2;
            movement.y = 0f;
            dodgecount++;
        }else if (dodgecount == 5){
            spriteRenderer.sprite = normal;
            dodging = false;
            action = "";
            dodgecount = 0;
        }
    }

    void rightDodge(){
        dodging = true;
        action = "dodgeRight";
        spriteRenderer.flipX = true;
        if(dodgecount ==0){
            spriteRenderer.sprite = dodgeLeftStart;
            dodgecount++;
        }else if (dodgecount == 1){
            spriteRenderer.sprite = dodgeLeft;
            dodgecount++;
            movement.x = moveSpeed/2;
            movement.y = 0f;
        }else if (dodgecount ==2 ){
            spriteRenderer.sprite = dodgeLeftEnd;
            movement.x = moveSpeed/2;  
            dodgecount++;
            movement.y = 0f;
        }else if (dodgecount == 3){
            spriteRenderer.sprite = dodgeLeft;
            movement.x = -1*moveSpeed/2;
            movement.y = 0f;
            dodgecount++;
        }else if (dodgecount == 4){
            spriteRenderer.sprite = dodgeLeftStart;
            movement.x = -1*moveSpeed/2;
            movement.y = 0f;
            dodgecount++;
        }else if (dodgecount == 5){
            spriteRenderer.sprite = normal;
            spriteRenderer.flipX = false;
            dodging = false;
            action = "";
            dodgecount = 0;
        }
    }

    void uppercutLeft(){
        movement.x = 0;
        movement.y = 0;
        if(spriteRenderer.sprite == leftUppercutMid){
            if(back){
                spriteRenderer.sprite = leftUppercutStart;
            }else{
                punchhigh = true;
                spriteRenderer.sprite = leftUppercutEnd;
                movement.y = moveSpeed;
                movement.x = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }
        }else if(spriteRenderer.sprite == leftUppercutEnd){
            back = true;
            spriteRenderer.sprite = leftUppercutMid;
            movement.y = -1*moveSpeed;
            movement.x = 0f;
            punchhigh = false;
            if(en.isblockingHigh()){
                en.blockHigh();
            }else if (!(en.isSpecialing())){
                en.changeHealth(10);
                pk.updatePoints(10);
                if(en.canOneShot()){
                    en.setHealth(0);
                    en.knockDown();
                    pk.updatePoints(1000);
                    mar.action = "ecount";
                    en.changeTimesDown();
                }else{
                    if(en.canCounter()){
                        en.hitAfterDodge();
                        en.changeHits();
                    }else{
                        en.leftHit();
                    }
                } 
                if(en.getHealth()<=0){
                    en.knockDown();
                    pk.updatePoints(1000);
                    mar.action = "ecount";
                    en.changeTimesDown();
                }     

            }
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            movement.x = 0;
            movement.y = 0;
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp; 
            }else if(spriteRenderer.sprite == leftUppercutStart){
                spriteRenderer.sprite = leftUppercutMid;
            }else{
                spriteRenderer.sprite = leftUppercutStart;
            }
        }
    }

    void uppercutRight(){
        spriteRenderer.flipX = true;
        movement.x = 0;
        movement.y = 0;
        if(spriteRenderer.sprite == leftUppercutMid){
            if(back){
                spriteRenderer.sprite = leftUppercutStart;
            }else{
                punchhigh = true;
                spriteRenderer.sprite = leftUppercutEnd;
                movement.y = moveSpeed;
                movement.x = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }
        }else if(spriteRenderer.sprite == leftUppercutEnd){
            back = true;
            spriteRenderer.sprite = leftUppercutMid;
            movement.y = -1*moveSpeed;
            movement.x = 0f;
            punchhigh = false;
            if(en.isblockingHigh()){
                en.blockHigh();
            }else if (!(en.isSpecialing())){
                en.changeHealth(10);
                pk.updatePoints(10);
                if(en.canOneShot()){
                    en.setHealth(0);
                    en.knockDown();
                    mar.action = "ecount";
                    en.changeTimesDown();
                    pk.updatePoints(1000);
                }else{
                    if(en.canCounter()){
                        en.hitAfterDodge();
                        en.changeHits();
                    }else{
                        en.rightHit();
                    }
                } 
                if(en.getHealth()<=0){
                    en.knockDown();
                    pk.updatePoints(1000);
                    mar.action = "ecount";
                    en.changeTimesDown();
                }     
            }
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            movement.x = 0;
            movement.y = 0;
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp;
                spriteRenderer.flipX = false;
            }else if(spriteRenderer.sprite == leftUppercutStart){
                spriteRenderer.sprite = leftUppercutMid;
            }else{
                spriteRenderer.sprite = leftUppercutStart;
            }
        }
    }

    void bloc(){
        if(spriteRenderer.sprite == block){
            if(!(Input.GetKey(KeyCode.DownArrow))){
                blocking = false;
                spriteRenderer.sprite = normal;
                action = "";
                return;
            }
        }else{
            if(spriteRenderer.sprite == blockStart){
                spriteRenderer.sprite = block;
                blocking = true;
            }else{
                spriteRenderer.sprite= blockStart;
            }
        }
    }

    void punchLeft(){
        if(spriteRenderer.sprite == leftPunchMid){
            if(back){
                spriteRenderer.sprite = leftPunchStart;
            }else{
                spriteRenderer.sprite = leftPunch;
                punchlow = true;
            }
        }else if(spriteRenderer.sprite == leftPunch){
            back = true;
            spriteRenderer.sprite = leftPunchMid;
            punchlow = false;
            if(en.isblockingLow()){
                en.blockLow();
            }else if (!(en.isSpecialing())){
                en.changeHealth(10);
                pk.updatePoints(10);
                if(en.canOneShot()){
                    en.setHealth(0);
                    en.knockDown();
                    mar.action = "ecount";
                    en.changeTimesDown();
                    pk.updatePoints(1000);
                }else{
                    if(en.canCounter()){
                        en.hitAfterDodge();
                        en.changeHits();
                    }else{
                        en.hitLow();
                    }
                } 
                if(en.getHealth()<=0){
                    en.knockDown();
                    pk.updatePoints(10);
                    mar.action = "ecount";
                    en.changeTimesDown();
                }   
            }              
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
            }else if(spriteRenderer.sprite == leftPunchStart){
                spriteRenderer.sprite = leftPunchMid;
            }else{
                spriteRenderer.sprite = leftPunchStart;
            }
        }
    }

    void punchRight(){
        spriteRenderer.flipX = true;
        if(spriteRenderer.sprite == leftPunchMid){
            if(back){
                spriteRenderer.sprite = leftPunchStart;
            }else{
                spriteRenderer.sprite = leftPunch;
                punchlow = true;
            }
        }else if(spriteRenderer.sprite == leftPunch){
            back = true;
            spriteRenderer.sprite = leftPunchMid;
            punchlow = false;
            if(en.isblockingLow()){
                en.blockLow();
            }else if (!(en.isSpecialing())){
                en.changeHealth(10);
                pk.updatePoints(10);
                if(en.canOneShot()){
                    en.setHealth(0);
                    en.knockDown();
                    mar.action = "ecount";
                    en.changeTimesDown();
                    pk.updatePoints(1000);
                }else{
                    if(en.canCounter()){
                        en.hitAfterDodge();
                        en.changeHits();
                    }else{
                        en.hitLow();
                    }
                } 
                if(en.getHealth()<=0){
                    en.knockDown();
                    mar.action = "ecount";
                    en.changeTimesDown();
                }   
            }
        }else{
            if(back){
                back = false;
                spriteRenderer.flipX = false;
                spriteRenderer.sprite = normal;
                action = "";
            }else if(spriteRenderer.sprite == leftPunchStart){
                spriteRenderer.sprite = leftPunchMid;
            }else{
                spriteRenderer.sprite = leftPunchStart;
            }
        }
    }
    public int count;
    public void hit(){
        spriteRenderer.flipX = false;
        rb.position = fp;
        movement.x = 0f;
        movement.y = 0f;
        action = "hit";
        en.setCounter(false);
        if(count<2){
            spriteRenderer.sprite = hit1;
            count++;
        }else if(count < 6){
            spriteRenderer.sprite = hit2;
            count++;
        }else if (count == 6){
            count = 0;
            spriteRenderer.sprite = normal;
            action = "";
        }
        // if(spriteRenderer.sprite == hit1){
        //     spriteRenderer.sprite = hit2;
        // }else{
        //     if(spriteRenderer.sprite = hit2){
        //         spriteRenderer.sprite = normal;
        //         action = "";
        //     }else{
        //         spriteRenderer.sprite = hit1;
        //     }
        // }
    }
    
    public void knockeddown(){
        action = "knockeddown";
        if (count == 0){
            knockdowned++;
            spriteRenderer.sprite = hit1;
            en.setAction("asfasdfadfafa");
            count++;
        }else if (count == 1){
           spriteRenderer.sprite = hit2; 
           count++;
        }else if (count == 2){
            mar.action = "lcount";
            if(Input.GetKey(KeyCode.Z)||Input.GetKey(KeyCode.X)){
                countzx++;
            }
            spriteRenderer.sprite = knockedDown;
            if(countzx >= 10){
                mar.action = "";
                mar.count = 0;
                health = 100.0F;
                action = "";
                en.setAction("");
                countzx = 0;
            }
            if(knockdowned >= 4){
                health = 0F;
                countzx = 0;
                action = "knockedout";
                mar.tkod();
                en.win();
            }
        }        
    }

    public void win(){
        rb.position = fp;
        action = "win";
        en.setAction("wait");
        //en.knockDown();
        //en.spriteRenderer.sprite = normal;
        //en.hits = 7;
        //en.stunned = false;
        mar.lmwin = true;
        if(spriteRenderer.sprite == win1){
            spriteRenderer.sprite = win2;
        }else{
            spriteRenderer.sprite = win1;
        }
    }
    
}
