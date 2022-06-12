using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Timer : MonoBehaviour

{

    public float timeIn = 0;
    public Sprite[] images;
    public Image[] digit_image;
    public int minutespassed;
    public int tenspassed;
    public int secondspassed;
    // Start is called before the first frame update
    void Start()
    {
        digit_image[0].sprite = images[0];
        digit_image[1].sprite = images[0];
        digit_image[2].sprite = images[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIn < 300)
        {

        
        timeIn +=  Time.deltaTime;
         
        }
        else
        {
            timeIn = 300;
        }
        DisplayTime(timeIn);
       
    }

    public void DisplayTime(float time)
    {
        if (time > 300)
        {
            time = 300;
            digit_image[0].sprite = images[5];
            digit_image[1].sprite = images[0];
            digit_image[2].sprite = images[0];

        }

        int minutes = Mathf.FloorToInt(time / 60);

        int secs = Mathf.FloorToInt(time % 60);

        digit_image[0].sprite = images[minutes];
        minutespassed = minutes;
        secondspassed = secs%10;
        tenspassed = secs/10;

        List<int> list = new List<int>();

        while (secs > 0)
        {
            list.Add(secs % 10);
            secs = secs / 10;
        }
        list.Reverse();
//        print(list);
        int[] list1 = list.ToArray();
        if (list1.Length == 1)
        {
            digit_image[2].sprite = images[list1[0]];
        }
        else if(list1.Length == 0)
        {

        }
        else
        {
            digit_image[1].sprite = images[list1[0]];
            digit_image[2].sprite = images[list1[1]];
        }

    }

    public int getTime(){
        return minutespassed*100+tenspassed*10+secondspassed*1;
    }
}
