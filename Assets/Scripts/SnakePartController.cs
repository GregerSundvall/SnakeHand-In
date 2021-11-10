using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SnakePartController : MonoBehaviour
{
    public Vector3 position;
    public Vector3 previousPosition;
    //public bool appleHit = false;
    public Vector3 normalSize = new Vector3(1f, 1f, 1f);
    public Vector3 bigSize = new Vector3(1.1f, 1.1f, 1.1f);
    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 currentSize;
    //public bool isTail = false;
    public GameObject collidingApple;
    public bool isColliding = false;

    
    void Start()
    {
        currentSize = normalSize;
    }
    
    void Update()
    {
        transform.localScale = isColliding ? bigSize : normalSize;
        transform.position = Vector3.MoveTowards(transform.position, position, 3 * Time.deltaTime);
        //transform.position = position;
        //transform.position = Vector3.Lerp(previousPosition, position, 1f);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.name == "Apple(Clone)")
    //     {
    //         other.transform.localScale = minSize;
    //         collidingApple = other.gameObject;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Apple(Clone)")
        {
            isColliding = true;
            other.transform.localScale = minSize;
            collidingApple = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isColliding = false;

    }

    

    
}
