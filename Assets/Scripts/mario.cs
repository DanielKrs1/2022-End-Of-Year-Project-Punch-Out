using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mario : MonoBehaviour
{
    // Start is called before the first frame update
    public littlemac lm;
    public enemy en;
    public Sprite normal;
    public Sprite fight;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;
    public Sprite ten;
    public Sprite ko;
    public Sprite tko;
    public Sprite counting;
    public Sprite prefight;
    public Sprite prefight2;
    public Sprite prefight3;
    public Sprite defendWin;
    public Sprite Win;
    public SpriteRenderer spriteRenderer;
    public string action = "startFight";

    public bool fightStarted = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer.sprite == null){
            spriteRenderer.sprite = normal;
        }
        action = "startFight";
        bool found = false;
        lm = GameObject.Find("lm").GetComponent("littlemac") as littlemac;
        en = GameObject.Find("enemy").GetComponent("glassjoe") as enemy;
        if(en!=null){
            en = GameObject.Find("enemy").GetComponent("glassjoe") as glassjoe;
            found = true;
        }/*else{
            en = GameObject.Find("enemy").GetComponent("vonkaiser") as enemy;
        }
        if (en!=null&&!found){
            en = GameObject.Find("enemy").GetComponent("vonkaiser") as vonkaiser;
            found = true;
        }else{
            en = GameObject.Find("enemy").GetComponent("pistonhonda") as enemy;
        }
        if(en != null&&!found){
            en = GameObject.Find("enemy").GetComponent("pistonhonda") as pistonhonda;
        }*/

        
    }

    public int frame = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(frame==25){
            frame = 0;
        }
        if(frame%25 == 0){
            if(action.Equals("startFight")){
                lm.action = "wait";
                en.setAction("wait");
                startFight();
            }else if(action.Equals("ecount")){
                encountdown();
            }else if(action.Equals("lcount")){
                lmcountdown();
            }else if (action.Equals("ko")){
                kod();
            }else if (action.Equals("tko")){
                tkod();
            }else if (action.Equals("wait")){

            }else{
                spriteRenderer.sprite = normal;
            }
        }
        frame++;
    }

    public int count = 0;
    void startFight(){
        if (count == 0){
            spriteRenderer.sprite = prefight;
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = prefight2;
            count++;
        }else if (count == 2){
            spriteRenderer.sprite = prefight3;
            count++;
        }else if (count == 3){
            spriteRenderer.sprite = fight;
            count = 0;
            fightStarted = false;
            lm.action = "";
            en.setAction("");
            action = "";
        }
    }

    public void encountdown(){
        action = "ecount";
        if (count == 0){
            lm.action = "a;klfjafja;dfjka;fdkja;";
            spriteRenderer.sprite = counting;
            en.getUp();
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = one;
            count++;
        }else if(count == 2){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = two;
            count++;
        }else if(count == 4){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 5){
            spriteRenderer.sprite = three;
            count++;
        }else if(count == 6){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 7){
            spriteRenderer.sprite = four;
            count++;
        }else if(count == 8){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 9){
            spriteRenderer.sprite = five;
            count++;
        }else if(count == 10){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 11){
            spriteRenderer.sprite = six;
            count++;
        }else if(count == 12){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 13){
            spriteRenderer.sprite = seven;
            count++;
        }else if(count == 14){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 15){
            spriteRenderer.sprite = eight;
            count++;
        }else if(count == 16){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 17){
            spriteRenderer.sprite = nine;
            count++;
        }else if(count == 18){
            en.getUp();
            spriteRenderer.sprite = counting;
            count++;
        }else if (count == 19){
            spriteRenderer.sprite = ten;
            count = 0;
            lm.win();
            en.setAction("wait");
            action = "ko";
        }
    }

    public void lmcountdown(){
        action= "lcount";
        if (count == 0){
            en.setAction("afa;dfjalkdfl;afdj;adfj");
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 1){
            spriteRenderer.sprite = one;
            count++;
        }else if(count == 2){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 3){
            spriteRenderer.sprite = two;
            count++;
        }else if(count == 4){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 5){
            spriteRenderer.sprite = three;
            count++;
        }else if(count == 6){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 7){
            spriteRenderer.sprite = four;
            count++;
        }else if(count == 8){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 9){
            spriteRenderer.sprite = five;
            count++;
        }else if(count == 10){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 11){
            spriteRenderer.sprite = six;
            count++;
        }else if(count == 12){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 13){
            spriteRenderer.sprite = seven;
            count++;
        }else if(count == 14){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 15){
            spriteRenderer.sprite = eight;
            count++;
        }else if(count == 16){
            spriteRenderer.sprite = counting;
            count++;
        }else if(count == 17){
            spriteRenderer.sprite = nine;
            count++;
        }else if(count == 18){
            spriteRenderer.sprite = counting;
            count++;
        }else if (count == 19){
            spriteRenderer.sprite = ten;
            count = 0;
            lm.action = "wait";
            en.win();
            action = "ko";
        }
    }

    public void kod(){
        action = "ko";
        if(count < 3){
            spriteRenderer.sprite = ko;  
            en.setAction("wait");
        }        
    }

    public void tkod(){
        action = "tko";
        if(count < 3){
            spriteRenderer.sprite = tko;  
            en.setAction("wait");
        }
    }

    public void defenderWin(){
        spriteRenderer.sprite = defendWin;
        action = "wait";
    }

    public void lmWin(){
        spriteRenderer.sprite = Win;
        action = "wait";
    }

}
