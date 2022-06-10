using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    private static int points;
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void updatePoints(int number){
        points+=number;
        
    }
}
