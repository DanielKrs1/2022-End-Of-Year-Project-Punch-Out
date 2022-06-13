using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointKeeper : MonoBehaviour
{
    public SpriteRenderer pointLoader;
    public Sprite firstDigit;
    public Sprite digit2;
    public Sprite digit3;
    public Sprite digit4;
    public Sprite digit5;
    public GameObject ssx;
    public Sprite[] images;
    public Image[] digit_image;
    // Start is called before the first frame update
    private int points;
    void Start()

    {
        digit_image[0].sprite = images[0];
        digit_image[1].sprite = images[0];
        digit_image[2].sprite = images[0];
        digit_image[3].sprite = images[0];
        digit_image[4].sprite = images[0];
        pointLoader = GetComponent<SpriteRenderer>();
        //firstDigit = Resources.Load<Sprite>("tile000");
        //digit2 = Resources.Load<Sprite>("tile001");
        
        //pointLoader.sprite = digit2;
        //changeSprite(2);
        
        //points = 365;
        //changeSprite(4);
        //new1 = 
    }

    // Update is called once per frame

    public void updatePoints(int number){
        points+=number;
        changeSprite(points);
    }

    public int getPoints(){
        return points;
    }

   private int[] SeperatedDigit(int score)
    {
        List<int> list = new List<int>();

        while (score > 0)
        {
            list.Add(score % 10);
            score = score / 10;
        }
        list.Reverse();
        return list.ToArray();

    }

    private void changeSprite(int number)
    {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = images[number];
        int[] updatingScoreArray = SeperatedDigit(points);
        if(updatingScoreArray.Length == 1)
        {
            digit_image[4].sprite = images[updatingScoreArray[0]];
        }
        else if (updatingScoreArray.Length == 2)
        {
            digit_image[3].sprite = images[updatingScoreArray[0]];

            digit_image[4].sprite = images[updatingScoreArray[1]];
        }
        else if (updatingScoreArray.Length == 3)

        {
            digit_image[2].sprite = images[updatingScoreArray[0]];
            digit_image[3].sprite = images[updatingScoreArray[1]];

            digit_image[4].sprite = images[updatingScoreArray[2]];
        }
        else if (updatingScoreArray.Length == 4)
        {
            digit_image[1].sprite = images[updatingScoreArray[0]];
            digit_image[2].sprite = images[updatingScoreArray[1]];
            digit_image[3].sprite = images[updatingScoreArray[2]];

            digit_image[4].sprite = images[updatingScoreArray[3]];
        }
        else if (updatingScoreArray.Length == 5)
        {
            digit_image[0].sprite = images[updatingScoreArray[0]];

            digit_image[1].sprite = images[updatingScoreArray[1]];

            digit_image[2].sprite = images[updatingScoreArray[2]];

            digit_image[3].sprite = images[updatingScoreArray[3]];
            digit_image[4].sprite = images[updatingScoreArray[4]];
        }
    }
}
