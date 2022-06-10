﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassjoe : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normald;
    public Sprite normald2;
    public Sprite normald3;
    public Sprite move;
    public Sprite move2;
    public Sprite move3;
    public Sprite move4;
    public Sprite move5;
    public Sprite move6;
    public Sprite prepunch;
    public Sprite cluepunch;
    public Sprite midpunch;
    public Sprite punch;
    public Sprite upperStart;
    public Sprite upperclue;
    public Sprite upperMid;
    public Sprite upperEnd;
    public Sprite blocklow;
    public Sprite blocklow2;
    public Sprite blockhigh;
    public Sprite blockhigh2;
    public Sprite blockhigh3;
    public Sprite hitlow;
    public Sprite hitlow2;
    public Sprite hithigh;
    public Sprite hithigh2;
    public Sprite hithighr;
    public Sprite hithighr2;
    public Sprite dodgeHit;
    public Sprite knockdown;
    public Sprite knockdown2;
    public Sprite knockdown3;
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
        blockinghigh = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lowhits == 3){
            blockinglow = true;
            blockinghigh = false;
            lowhits = 0;
        }
        if(highhits == 3){
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
            if(randint == 1){
                action = "special";
            }else if(randint<2){
                action = "upper";
            }else if(randint<6){
                action = "normalPunch";
            }
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
                if(blockinghigh){
                    if(spriteRenderer.sprite == normal){
                        spriteRenderer.sprite = normal2;
                    }else{
                        spriteRenderer.sprite = normal;
                    }
                }else{
                    if(spriteRenderer.sprite == normald){
                        spriteRenderer.sprite = normald2;
                    }else{
                        spriteRenderer.sprite = normald;
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
            spriteRenderer.sprite = prepunch;
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = cluepunch;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = prepunch;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = midpunch;
            count++;
        }else if (count == 4){
            spriteRenderer.sprite = normal;
            count++;
        }else if( count == 5){
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
        }else if (count < 12){
            specialing = false;
            punching = false;
            counter = true;
            count++;
            temp1 = blockinghigh;
            temp2 = blockinglow;
            blockinghigh = false;
            blockinglow = true;
        }else if(count == 12){
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
            spriteRenderer.sprite = upperStart;
            count++;
        }else if (count == 1){
            spriteRenderer.sprite = upperclue;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = upperMid;
            count++;
        }else if (count ==3 ){
            punching = true;
            spriteRenderer.sprite = upperEnd;
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
                blockinghigh = temp1;
                blockinglow = temp2;
        }
    }

    void special(){
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
    }

    public void blockLow(){
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

    public void blockHigh(){
        action = "blockHigh";
        specialing = false;
        if(count == 0){
            spriteRenderer.sprite = blockhigh;
            count++;
        }if(count == 1){
            spriteRenderer.sprite = blockhigh2;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = blockhigh3; 
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public void hitLow(){
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

    public void rightHit(){
        highhits++;
        action = "rightHit";
        if(count == 0){
            spriteRenderer.sprite = hithighr;
            count++;
        }if(count == 1){
            spriteRenderer.sprite = hithighr2;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public void leftHit(){
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
    public void hitAfterDodge(){
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

    public void knockDown(){
        stunned = false;
        hits = 7;
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockdown;
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

    public void getUp(){
        mar.action = "wait";
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
        

    public void win(){
        action = "win";
        mar.action = "wait";
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
}
