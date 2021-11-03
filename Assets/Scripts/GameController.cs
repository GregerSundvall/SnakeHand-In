using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LinkedList<GameObject> snake;
    public float moveDelay = 0.3f;
    public GameObject snakePartPrefab;
    private Vector3 startPos = new Vector3(25, 0, 25);

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);

    public Vector3 direction;
        
    // Start is called before the first frame update
    void Start()
    {
        snake.Add(Instantiate(snakePartPrefab, startPos, Quaternion.identity));
        direction = north;
        StartCoroutine(MakeAMove(moveDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        
        for (int i = 0; i < snake.Count; i++)
        {
            
        }
        var temp = snake.Head();
        var newPos = temp + direction;
        snake.head.content = newPos;
    }
    
    
    IEnumerator MakeAMove(float delay)
    {
        yield return new WaitForSeconds(delay);
            
        
        StartCoroutine(MakeAMove(moveDelay));
    }
    
    
    
}
    