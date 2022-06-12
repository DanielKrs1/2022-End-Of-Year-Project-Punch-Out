using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stamina : MonoBehaviour
{
    public Sprite[] images;
    public Image[] digit_image;
    public int stamina_score;

    // Start is called before the first frame update
    void Start()
    {
        digit_image[0].sprite = images[0];
        digit_image[1].sprite = images[0];
        //set(99);
        //change(30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get()
    {
        return stamina_score;
    }
    public void set(int x)
    {
        stamina_score = x;
        display();

    }
    public void change(int x)
    {
        stamina_score -= x;
        display();
    }
    public void display()
    {
        int temp = stamina_score;
        List<int> list = new List<int>();
        while (temp > 0)
        {
            list.Add(temp % 10);
            temp = temp / 10;
        }
        list.Reverse();

        int[] list1 = list.ToArray();
        if (list1.Length == 1)
        {
            digit_image[0].sprite = images[0];
            digit_image[1].sprite = images[list1[0]];

        }
        else
        {
            digit_image[0].sprite = images[list1[0]];
            digit_image[1].sprite = images[list1[1]];
        }
    }
}
