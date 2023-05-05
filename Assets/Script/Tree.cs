using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    // //Jika memakai List
    // static List<Vector3> positions;

    //Kenapa memakai HashSet? karena HashSet memungkinkan untuk tidak memiliki nilai yang sama,
    //karena memakai list bisa terdapat memakai nilai yang sama. 
    static HashSet<Vector3>positionSet = new HashSet<Vector3>();

    public static HashSet<Vector3> AllPosition { get => new HashSet<Vector3>(positionSet);}

    private void OnEnable() {
        // //Jika memakai List
        // if(positions.Contains(this.transform.position)==false){
        //     positions.Add(this.transform.position);
        // }
        positionSet.Add(this.transform.position);
    }

    private void OnDisable() {
        // //Jika memakai List
        // if(positions.Contains(this.transform.position)==true){
        //      positions.Remove(this.transform.position)
        // }
        
        positionSet.Remove(this.transform.position);
    }
}
