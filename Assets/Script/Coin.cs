using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField, Range (0,10)] float rotationSpeed = 1;

    public int Value { get => value;}


    //Animasi untuk mengambil Coin
    public void Collected()
    {
        GetComponent<Collider>().enabled=false;
        rotationSpeed*=2;
        this.transform.DOJump(this.transform.position,
                              1,
                              1,
                              0.3f).onComplete=SelfDestruct;
    }

    //Ketika coin telah diambil maka coin tersebut dihapus
    public void SelfDestruct(){
        Destroy(this.gameObject);
    }

    private void Update() {
        transform.Rotate(0,360*rotationSpeed*Time.deltaTime,0);
    }
}
