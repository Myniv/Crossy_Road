using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField, Range(0, 50)] float speed = 20;
    bool pause=false;


    private void Update()
    {
        if(pause==true){
            EaglePause(true);
        } else{
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void EaglePauseSpeed(float speedEagle){
        pause=true;
        transform.Translate(Vector3.forward * speedEagle * Time.deltaTime);
    }

    public void EaglePause(bool pauseEagle){
        pause=pauseEagle;
        EaglePauseSpeed(0);
    }

}
