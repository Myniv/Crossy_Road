using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour
{
    [SerializeField, Range(0,1)] float speed = 0.8f;
    public UnityEvent OnTouchPlaceToDestroy;
    public UnityEvent OnStart;
    Vector3 initialPosition;
    float distanceLimit = float.MaxValue;

    public void SetUpDistanceLimit(float distance){
        this.distanceLimit = distance;

    }
    private void Start() {
        initialPosition = this.transform.position;
        OnStart.Invoke();

    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    
        //Jika mobil melebihi dari batasan sisi, mobil dihancurkan
        if(Vector3.Distance(initialPosition,this.transform.position)>this.distanceLimit){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("DestroyRocket")){
            Debug.Log("Touch");
            OnTouchPlaceToDestroy.Invoke();
        }
    }
}
