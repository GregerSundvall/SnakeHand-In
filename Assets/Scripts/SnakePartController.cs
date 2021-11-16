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
    private Vector3 normalSize = new Vector3(.8f, .8f, .8f);
    private Vector3 bigSize = new Vector3(1.3f, 1.3f, 1.3f);
    private Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 currentSize;
    //public bool isTail = false;
    public GameObject collidingApple;
    public bool isColliding = false;
    private bool gameOver;
    
    void Start()
    {
        currentSize = normalSize;
    }
    
    void Update()
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //transform.localScale = isColliding ? bigSize : normalSize;
        transform.position = Vector3.MoveTowards(transform.position, 
            position, 
            SnakeController.moveTowardsMaxDelta * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Apple(Clone)"))
        {
            isColliding = true;
            other.transform.localScale = minSize;
            collidingApple = other.gameObject;
        }

        if (other.gameObject.name.Equals("SnakePart(Clone)"))
        {
            GameController.gameOver = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isColliding = false;

    }
}
