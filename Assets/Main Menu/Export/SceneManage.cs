using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("Game With Different Asset");
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Quit(){
        Application.Quit();
    }
}
