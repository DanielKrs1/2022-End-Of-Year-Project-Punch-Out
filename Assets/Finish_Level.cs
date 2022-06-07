using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish_Level : MonoBehaviour
{
    // Start is called before the first frame update
   private void Finished(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
       
    }
}
