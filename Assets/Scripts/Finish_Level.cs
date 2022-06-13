using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish_Level : MonoBehaviour
{
    // Start is called before the first frame update
   public void Win(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }

    public void Lost(){
        if(SceneManager.GetActiveScene().buildIndex>0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
