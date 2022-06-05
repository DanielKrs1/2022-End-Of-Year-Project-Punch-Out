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

    public glassjoe enemy;
    public int health = 90;

    public Rigidbody2D rb;
    Vector2 movement;
    public int frame = 0;
    public string action = "";
    private SpriteRenderer spriteRenderer;
    Vector2 fp;

    public bool punchlow = false;
    public bool punchhigh = false;
    public bool dodging = false;
    public bool blocking = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.sprite == null){
            spriteRenderer.sprite = normal;
        }
        fp = rb.position;
        moveSpeed = 1f;
        enemy = GameObject.Find("tile00b").GetComponent("glassjoe") as glassjoe;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = 1f;
        //lastPos = transform.position;
        if(frame == 119){
            frame = 0;
        }
        if(frame%15==0&&action.Length<2){
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                action = "dodgeRight";
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                action = "dodgeLeft";       
            }else if (Input.GetKey(KeyCode.UpArrow))
            {
                if(Input.GetKey(KeyCode.Z)){
                    action = "upPunchLeft";
                }else if(Input.GetKey(KeyCode.X)){
                    action = "upPunchRight";
                }
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                action = "block";
            }else if(Input.GetKey(KeyCode.Z)){
                action = "punchLeft";
            }else if(Input.GetKey(KeyCode.X)){
                action = "punchRight";
            }
        }

        if(frame%15==0){
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
            }else{
                if(spriteRenderer.sprite == normal){
                    spriteRenderer.sprite = normal2;
                }else{
                    spriteRenderer.sprite = normal;
                }
                movement.x = 0f;
                movement.y = 0f;
            }  
            rb.MovePosition(rb.position+movement);//*Time.deltaTime);   
        }
         
        frame++;  
    }

    public bool back = false;

    void leftDodge(){
        if(spriteRenderer.sprite == dodgeLeft){
            if(back){
                dodging = false;
                spriteRenderer.sprite = dodgeLeftStart;
                movement.x = moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }else{
                spriteRenderer.sprite = dodgeLeftEnd;
                movement.x = -1*moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }
        }else if(spriteRenderer.sprite == dodgeLeftEnd){
            back = true;
            spriteRenderer.sprite = dodgeLeft;
            
            movement.x = moveSpeed/2;
            movement.y = 0f;
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp;
                movement.x = 0;
                movement.y = 0;
            }else if(spriteRenderer.sprite == dodgeLeftStart){
                dodging = true;
                spriteRenderer.sprite = dodgeLeft;
                movement.x = -1*moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }else{
                spriteRenderer.sprite = dodgeLeftStart;                
            }
        }
    }

    void rightDodge(){
        if(spriteRenderer.sprite == dodgeRight){
            if(back){
                dodging = false;
                spriteRenderer.sprite = dodgeRightStart;
                movement.x = -1*moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }else{
                spriteRenderer.sprite = dodgeRightEnd;
                movement.x = moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }
        }else if(spriteRenderer.sprite == dodgeRightEnd){
            back = true;
            spriteRenderer.sprite = dodgeRight;
            movement.x = -1*moveSpeed/2;
            movement.y = 0f;
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp;
                movement.x = 0;
                movement.y = 0;
            }else if(spriteRenderer.sprite == dodgeRightStart){
                dodging = true;
                spriteRenderer.sprite = dodgeRight;
                movement.x = moveSpeed/2;
                movement.y = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }else{
                spriteRenderer.sprite = dodgeRightStart;
            }
        }
    }

    void uppercutLeft(){
        if(spriteRenderer.sprite == leftUppercutMid){
            if(back){
                spriteRenderer.sprite = leftUppercutStart;
                movement.x = 0;
                movement.y = 0;
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
            if(enemy.blockinghigh){
                enemy.blockHigh();
            }else if (!(enemy.specialing)){
                enemy.health-=10;
                if(enemy.onehit){
                    enemy.health = 0;
                    enemy.knockDown();
                }else{
                    if(enemy.counter){
                        enemy.hitAfterDodge();
                    }else{
                        enemy.leftHit();
                    }
                } 
                if(enemy.health<=0){
                    enemy.knockDown();
                }     
            }
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp;
                movement.x = 0;
                movement.y = 0;
            }else if(spriteRenderer.sprite == leftUppercutStart){
                spriteRenderer.sprite = leftUppercutMid;
            }else{
                spriteRenderer.sprite = leftUppercutStart;
            }
        }
    }

    void uppercutRight(){
        if(spriteRenderer.sprite == rightUppercutMid){
            if(back){
                spriteRenderer.sprite = rightUppercutStart;
                movement.x = 0;
                movement.y = 0;
            }else{
                punchhigh = true;
                spriteRenderer.sprite = rightUppercutEnd;
                movement.y = moveSpeed;
                movement.x = 0f;
                //rb.MovePosition(rb.position + movement * Time.deltaTime);
            }
        }else if(spriteRenderer.sprite == rightUppercutEnd){
            back = true;
            spriteRenderer.sprite = rightUppercutMid;
            movement.y = -1*moveSpeed;
            movement.x = 0f;
            punchhigh = false;
            if(enemy.blockinghigh){
                enemy.blockHigh();
            }else if (!(enemy.specialing)){
                enemy.health-=10;
                if(enemy.onehit){
                    enemy.health = 0;
                    enemy.knockDown();
                }else{
                    if(enemy.counter){
                        enemy.hitAfterDodge();
                    }else{
                        enemy.rightHit();
                    }
                } 
                if(enemy.health<=0){
                    enemy.knockDown();
                }     
            }
            //rb.MovePosition(rb.position + movement * Time.deltaTime);
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
                rb.position = fp;
                movement.x = 0;
                movement.y = 0;
            }else if(spriteRenderer.sprite == rightUppercutStart){
                spriteRenderer.sprite = rightUppercutMid;
            }else{
                spriteRenderer.sprite = rightUppercutStart;
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
            if(enemy.blockinglow){
                enemy.blockLow();
            }else if (!(enemy.specialing)){
                enemy.health-=10;
                if(enemy.counter){
                    enemy.hitAfterDodge();
                }else{
                    enemy.hitLow();
                }
                if(enemy.health<=0){
                    enemy.knockDown();
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
        if(spriteRenderer.sprite == rightPunchMid){
            if(back){
                spriteRenderer.sprite = rightPunchStart;
            }else{
                spriteRenderer.sprite = rightPunch;
                punchlow = true;
            }
        }else if(spriteRenderer.sprite == rightPunch){
            back = true;
            spriteRenderer.sprite = rightPunchMid;
            punchlow = false;
            if(enemy.blockinglow){
                enemy.blockLow();
            }else if (!(enemy.specialing)){
                enemy.health-=10;
                if(enemy.counter){
                    enemy.hitAfterDodge();
                }else{
                    enemy.hitLow();
                }
                if(enemy.health<=0){
                    enemy.knockDown();
                }   
            }
        }else{
            if(back){
                back = false;
                spriteRenderer.sprite = normal;
                action = "";
            }else if(spriteRenderer.sprite == rightPunchStart){
                spriteRenderer.sprite = rightPunchMid;
            }else{
                spriteRenderer.sprite = rightPunchStart;
            }
        }
    }

    public void hit(){
        action = "hit";
        if(spriteRenderer.sprite == hit1){
            spriteRenderer.sprite = hit2;
        }else{
            if(spriteRenderer.sprite = hit2){
                spriteRenderer.sprite = normal;
                action = "";
            }else{
                spriteRenderer.sprite = hit1;
            }
        }
    }

    public void knockeddown(){
        if(spriteRenderer.sprite == hit1){
            spriteRenderer.sprite = hit2;
        }else{
            if(spriteRenderer.sprite = hit2){
                spriteRenderer.sprite = knockedDown;
            }else{
                spriteRenderer.sprite = hit1;
            }
        }
    }

}
