using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Range(0,1)] float speed = 0.8f;
    
    Vector3 initialPosition;
    float distanceLimit = float.MaxValue;

    public void SetUpDistanceLimit(float distance){
        this.distanceLimit = distance;

    }
    private void Start() {
        initialPosition = this.transform.position;

    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    
        //Jika mobil melebihi dari batasan sisi, mobil dihancurkan
        if(Vector3.Distance(initialPosition,this.transform.position)>this.distanceLimit){
            Destroy(this.gameObject);
        }
    }
}
