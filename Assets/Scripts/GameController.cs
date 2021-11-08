using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public LinkedList<GameObject> snake = new LinkedList<GameObject>();
    public float moveDelay = 0.2f;
    public GameObject snakePartPrefab;
    public GameObject applePrefab;
    private Vector3 startPos = new Vector3(16  , 2, 16);
    public static bool grow = false;
    public int steps = 0;
    public static int applesPicked = 0;
    private List<int> growPositions = new List<int>();

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    List<Vector3> directions = new List<Vector3>();
    
    public Vector3 direction;
    private int directionInput = 0;
    private int previousDirectionInput = 0;
    private bool changeDirection = false;
        
    // Start is called before the first frame update
    void Start()
    {
        var newPart = Instantiate(snakePartPrefab, startPos, Quaternion.identity);
        var script = newPart.GetComponent<SnakePartController>();
        script.position = startPos;
        script.previousPosition = startPos;
        snake.Add(newPart);
        directions.Add(north);
        directions.Add(east);
        directions.Add(south);
        directions.Add(west);
        direction = directions[0];
        
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
            directionInput = 0;
            changeDirection = true;
        } else if (Input.GetKeyDown("a"))
        {
            directionInput = -1;
            changeDirection = true;
        } else if (Input.GetKeyDown("d"))
        {
            directionInput = 1;
            changeDirection = true;
        }
    }

    void SetDirection()
    {
        var newDirection = directionInput + previousDirectionInput;
        if (newDirection < 0)
        {
            newDirection += 4;
        } else if (newDirection > 3)
        {
            newDirection -= 4;
        }
        
        previousDirectionInput = newDirection;
        direction = directions[newDirection];
        changeDirection = false;
    }
   
    
    void Move()
    {
        if (changeDirection)
        {
            SetDirection();
        }
        var head = snake.GetFromIndex(0);
        var script = head.GetComponent<SnakePartController>();
        var posFromPreviousPart = script.position;
        script.previousPosition = script.position;
        script.position += direction;

        if (grow)
        {
            growPositions.Add(1);
        }
       
        head.transform.localScale = new Vector3(1f,1f,1f);
        
        
        // var position = head.transform.position;
        // var posFromPreviousPart = position;
        // position += direction;
        // head.transform.position = position;
        //var delayBetweenParts = 0.02f;

        for (int i = 1; i < snake.Count; i++)
        {
            
            var snakePart = snake.GetFromIndex(i);
            script = snakePart.GetComponent<SnakePartController>();
            script.previousPosition = script.position;
            (script.position, posFromPreviousPart) = (posFromPreviousPart, script.position);
            for (int j = 0; j < growPositions.Count; j++)
            {
                if (i == j +1)
                {
                    snakePart.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else
                {
                    snakePart.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }

            // for (int index = 0; index < growPositions.Count; i++)
            // {
            //     growPositions[index] += 1;
            // }
            
            //var tempPos = snakePart.transform.position;
            //StartCoroutine(MoveASnakePart(delayBetweenParts * i, snakePart, posFromPreviousPart));
            //posFromPreviousPart = tempPos;
            //(snakePart.transform.position, posFromPrevious) = (posFromPrevious, snakePart.transform.position);

        }

        if (grow)
        {
            var newTail = Instantiate(snakePartPrefab, posFromPreviousPart, Quaternion.identity);
            script = newTail.GetComponent<SnakePartController>();
            script.position = posFromPreviousPart;
            script.previousPosition = posFromPreviousPart;
            snake.Add(newTail);
            grow = false;
        }

        steps++;
        if (steps % 10 == 3 || steps == 3)
        {
            SpawnApple();
        }
    }
    
    // IEnumerator MoveASnakePart(float delay, GameObject part, Vector3 newPos)
    // {
    //     yield return new WaitForSeconds(delay);
    //
    //     part.transform.position = newPos;
    // }

    void SpawnApple()
    {
        Vector3 pos = new Vector3(0,0,0);
        var crash = true;
        while (crash)
        {
            crash = false;
            pos = new Vector3(Random.Range(1, 31), 2, Random.Range(1, 31));
            for (int i = 0; i < snake.Count; i++)
            {
                if (pos == snake.GetFromIndex(i).transform.position)
                {
                    crash = true;
                }
            }
        }
        Instantiate(applePrefab, pos, Quaternion.identity);
    }

   
    
    
    
    IEnumerator MakeAMove(float delay)
    {
        yield return new WaitForSeconds(delay);
            
        Move();

        if(moveDelay > 0.1f)
        {
            moveDelay -= 0.001f;
        }

        StartCoroutine(MakeAMove(moveDelay));
    }
    
    
    
}
    