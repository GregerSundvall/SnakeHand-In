using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LinkedList<GameObject> snake = new LinkedList<GameObject>();
    public float moveDelay = 0.3f;
    public GameObject snakePartPrefab;
    private Vector3 startPos = new Vector3(25, 0, 25);
    public bool grow = false;
    public int steps = 0;

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    List<Vector3> directions = new List<Vector3>();
    
    public Vector3 direction;
    private int directionInput;
    private int lastDirectionInput;
    private bool changeDirection = false;
        
    // Start is called before the first frame update
    void Start()
    {
        snake.Add(Instantiate(snakePartPrefab, startPos, Quaternion.identity));
        //direction = west;
        directions.Add(north);
        directions.Add(east);
        directions.Add(south);
        directions.Add(west);
        direction = directions[0];
        lastDirectionInput = 0;
        
        StartCoroutine(MakeAMove(moveDelay));
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKeyDown("w"))
        {
            lastDirectionInput = directionInput;
            directionInput = 0;
            changeDirection = true;
        } else if (Input.GetKeyDown("a"))
        {
            lastDirectionInput = directionInput;
            directionInput = -1;
            changeDirection = true;
        } else if (Input.GetKeyDown("d"))
        {
            lastDirectionInput = directionInput;
            directionInput = 1;
            changeDirection = true;
        }
    }

    void SetDirection()
    {
        var correctedInput = directionInput;
        if (lastDirectionInput + directionInput < 0)
        {
            correctedInput += 4;
        } else if (lastDirectionInput + directionInput > 3)
        {
            correctedInput -= 4;
        }
        
        //lastDirectionInput = correctedInput;
        direction = directions[correctedInput];
        changeDirection = false;
    }
   
    
    void Move()
    {
        if (changeDirection)
        {
            SetDirection();
        }
        var head = snake.GetFromIndex(0);
        var position = head.transform.position;
        var tempPos = position;
        position += direction;
        head.transform.position = position;

        for (int i = 1; i < snake.Count; i++)
        {
            var snakePart = snake.GetFromIndex(i);
            (snakePart.transform.position, tempPos) = (tempPos, snakePart.transform.position);
        }

        if (grow)
        {
            var newTail = Instantiate(snakePartPrefab, tempPos, Quaternion.identity);
            snake.Add(newTail);
            grow = false;
        }

        steps++;
        if (steps % 4 == 0)
        {
            grow = true;
        }
    }
    
    
    IEnumerator MakeAMove(float delay)
    {
        yield return new WaitForSeconds(delay);
            
        Move();
        StartCoroutine(MakeAMove(moveDelay));
    }
    
    
    
}
    