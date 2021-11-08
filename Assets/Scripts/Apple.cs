using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "SnakePart(Clone)")
        {
            Destroy(this.gameObject);
            GameController.applesPicked++;
            GameController.grow = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
