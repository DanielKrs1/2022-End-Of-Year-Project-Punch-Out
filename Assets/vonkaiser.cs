﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vonkaiser : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite normal;
    public Sprite normal2;
    public Sprite normal3;
    public Sprite normal4;
    public Sprite normalup;
    public Sprite normaulup2;
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
    public Sprite stunned;
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

    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "";
    private SpriteRenderer spriteRenderer;
    Vector2 fp;
    public int health = 210;

    public int lowhits = 0;
    public int highhits = 0;
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
        if(frame%15==0&&action.Length<2){
            var randint = Random.Range(0, 100);
            /*if(randint == 1){
                action = "special";
            }else */if(randint<2){
                action = "upper";
            }else if(randint<6){
                action = "normalPunch";
            }
        }

        if(frame%15==0){
            if(action.Equals("normalPunch")){
                normalPunch();
            }else if(action.Equals("upper")){
                upper();
            }else if(action.Equals("special")){
                special();
                rb.MovePosition(rb.position+movement);//*Time.deltaTime);  
            }else if(action.Equals("blockLow")){
                blockLow();
            }else if(action.Equals("blockHigh")){
                blockHigh();
            }else if(action.Equals("hitLow")){
                hitLow();
            }else if(action.Equals("rightHit")){
                rightHit();
            }else if(action.Equals("leftHit")){
                leftHit();
            }else if(action.Equals("hitAfterDodge")){
                hitAfterDodge();
            }else if(action.Equals("knockdown")){
                knockDown();
            }else if(action.Equals("getUp")){
                getUp();
            }else if(action.Equals("win")){
                win();
            }else{
                // if(blockinghigh){
                //     if(spriteRenderer.sprite == normal){
                //         spriteRenderer.sprite = normal2;
                //     }else{
                //         spriteRenderer.sprite = normal;
                //     }
                // }else{
                //     if(spriteRenderer.sprite == normald){
                //         spriteRenderer.sprite = normald2;
                //     }else{
                //         spriteRenderer.sprite = normald;
                //     }
                // }
                if(spriteRenderer.sprite == normal){
                    spriteRenderer.sprite = normal2;
                }else if(spriteRenderer.sprite == normal2){
                    spriteRenderer.sprite = normal3;
                }else if(spriteRenderer.sprite = normal3){
                    spriteRenderer.sprite = normal4;
                }else{
                    spriteRenderer.sprite = normal;
                }
            }  
             
        }
         
        frame++;  
    }

    public int count = 0;

    void normalPunch(){
        if(count == 0){
            spriteRenderer.sprite = normal;
            count++;
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
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count == 6){
            punching = false;
            counter = true;
            count++;
        }else if(count == 7){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
        }
    }

    void upper(){
        if(count == 0){
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
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count == 4){
            punching = false;
            counter = true;
            count++;      
        }else if(count == 5){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
        }
    }

    void special(){
        // specialing = true;
        // if(count == 0){
        //     movement.x = 0f;
        //     movement.y = 1*moveSpeed;
        //     spriteRenderer.sprite = normal3;
        //     count++;
        // }else if (count == 1){
        //     movement.y = 0f;
        //     spriteRenderer.sprite = normal;
        //     count++;
        // }else if (count ==2){
        //     spriteRenderer.sprite = normal3;
        //     count++;
        // }else if (count == 3){
        //     spriteRenderer.sprite = move6;
        //     count++;
        // }else if (count == 4){
        //     spriteRenderer.sprite = normal3;
        //     count++;
        // }else if (count == 5){
        //     spriteRenderer.sprite = prepunch;
        //     count++;
        // }else if(count == 6){
        //     spriteRenderer.sprite = midpunch;
        //     count++;
        // }else if (count == 7){
        //     spriteRenderer.sprite = prepunch;
        //     count++;
        // }else if (count ==8){
        //     spriteRenderer.sprite = normal3;
        //     count++;
        // }else if (count == 9){
        //     specialing = false;
        //     onehit = true;
        //     spriteRenderer.sprite = normald;
        //     count++;
        //     movement.x = 0f;
        //     movement.y = -1*moveSpeed;
        //     rb.position  = fp;
        //     spriteRenderer.sprite = normal3;
        //     count++;
        // }else{
        //     specialing = false;
        //     movement.y = 0f;
        //     onehit = false;
        //     count-=10;
        //     upper();
        //     if(count!=0){
        //         count+=10;
        //     }
        // }
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

    public void hitLow(){
        lowhits++;
        action = "hitLow";
        if(count == 1){
            spriteRenderer.sprite = hitlow;
            count++;
        }else if (count==2){
            spriteRenderer.sprite = hitlow2;
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

    int hits = 7;
    public void hitAfterDodge(){
        action = "hitAfterDodge";
        if(hits>0){
            spriteRenderer.sprite = stunned;
        }else{
            action = "";
            spriteRenderer.sprite = normal;
            hits = 7;
        }
    }

    public void knockDown(){
        action = "knockDown";
        if(count <=2){
            spriteRenderer.sprite = knockeddown;
            count++;
        }else{
            spriteRenderer.sprite = knockeddown2;
        }
    }

    public void getUp(){
        action = "getUp";
        if(count <=2){
            spriteRenderer.sprite = getup;
            count++;
        }else if (count <=5){
            spriteRenderer.sprite = getup2;
            count++;
        }else{
            spriteRenderer.sprite = normal;
            count = 0;
            action = "";
        }
    }

    public void win(){
        action = "win";
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
}