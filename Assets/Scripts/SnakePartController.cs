using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartController : MonoBehaviour
{
    public Vector3 position;
    public Vector3 previousPosition;
    


    

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = position;
        //transform.position = Vector3.Lerp(previousPosition, position, 0.1f);
        // var currentVelocity = Vector3.zero;
        // transform.position = Vector3.SmoothDamp(previousPosition, position, ref currentVelocity, 0.2f);
       
    }
}
