using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Vector3 headPos = new Vector3(25, 0, 25);

    public List<GameObject> snakeParts = new List<GameObject>();

    public float moveDelay = 0.3f;

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);

    public Vector3 direction;
    

    // Start is called before the first frame update
    void Start()
    {
        
        direction = north;
        StartCoroutine(MakeAMove(moveDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator MakeAMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        snakePositions.Insert(0, snakePositions[0] + direction);
        snakePositions.RemoveAt(snakePositions.Count -1);
        head.GetComponent<Transform>().position = snakePositions[0];
        StartCoroutine(MakeAMove(moveDelay));
    }
}
