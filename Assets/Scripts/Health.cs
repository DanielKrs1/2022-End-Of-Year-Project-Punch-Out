using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    SpriteRenderer current;
    
    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new Vector2(0.9991398f, 1.382134f);


        //scale(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scale(float x)
    {
        transform.localScale = new Vector2(x, 1.382134f);
        //transform.position = new Vector2(transform.position.x - x, transform.position.y);
    }
}
