using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistonhonda : MonoBehaviour
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
    public Sprite stunned;
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

    void normalPunch(){
        if(count == 0){
            spriteRenderer.sprite = normalr;
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = punchclue;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = punchclue2;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = normalup3;
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

    void hook(){
        if(count == 0){
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

    void upper(){
        if(count == 0){
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
        }else if (count ==3 ){
            punching = true;
            spriteRenderer.sprite = up;
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
        if(count%4 == 0){
            punching = false;
            counter = true;
            count++; 
            spriteRenderer.sprite = forspecial;
            count++;
        }else if (count%4 == 1){
            spriteRenderer.sprite = forspecial2;
            count++;
            punching = true;
            spriteRenderer.sprite = up;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }else if (count%4 == 2){
            punching = false;
            counter = true;
            count++; 
            spriteRenderer.sprite = forspecial3;
            count++;
        }else if (count%4 == 3){
            spriteRenderer.sprite = forspecial4;
            count++;
            punching = true;
            spriteRenderer.sprite = up;
            count++;
            if(!lm.blocking && !lm.dodging){
                lm.health -=10;
                if(lm.health<=0){
                    lm.knockeddown();
                }
                lm.hit();
            }
        }
        if(count == 20){
                counter = false;
                spriteRenderer.sprite = normal;
                count = 0;
                action = "";    
        }
    }

    public void blockLow(){
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

    public void blockHigh(){
        action = "blockHigh";
        if(count == 0){
            spriteRenderer.sprite = blockup;
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
            spriteRenderer.sprite = falldown;
            count++;
        }else{
            spriteRenderer.sprite = falldown2;
        }
    }

    public void getUp(){
        action = "getUp";
        if(count <=2){
            spriteRenderer.sprite = getup;
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
