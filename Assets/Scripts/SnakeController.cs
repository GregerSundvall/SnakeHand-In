using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static LinkedList<GameObject> snake = new LinkedList<GameObject>();
    private float moveDelay = 0.6f;
    public GameObject snakePartPrefab;
    private Vector3 startPos = GameController.startPos;
    //public static bool grow = false;
    public int movesDone = 0;  
    public static int applesPicked = 0;

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    List<Vector3> directions = new List<Vector3>();
    
    public Vector3 direction;
    private int directionInput = 0;
    private int previousDirectionInput = 0;
    private bool changeDirection = false;
    private GameObject nextToLastPart;
    
    
    private void Start()
    {
        // Create first snake part and add to snake.
        var newPart = Instantiate(snakePartPrefab, startPos, Quaternion.identity);
        var script = newPart.GetComponent<SnakePartController>();
        script.position = startPos;
        script.previousPosition = startPos;
        snake.Add(newPart);
        
        // Add directions to list.
        directions.Add(north);
        directions.Add(east);
        directions.Add(south);
        directions.Add(west);
        direction = directions[0];
        
        // Start movement timer.
        StartCoroutine(MakeAMove(moveDelay));
    }

   
    private void Update()
    {
        GetInput();
    }
    
    
    private void GetInput()
    {
        // Sets directionInput to last pressed direction key.
        
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

    private void SetDirection()
    {
        // Sets move direction according to current direction and last pressed direction key.
        
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

    private void Move()
    {
        if (changeDirection)
        {
            SetDirection();
        }

        // var head = snake.GetFromIndex(0);
        //var script = new SnakePartController();
        var posFromPreviousPart = Vector3.zero; // Temp position, to be passed on as next part's target pos.
        // script.previousPosition = script.position;
        // script.position += direction;

        for (var i = 0; i < snake.Count; i++)
        {
            var snakePart = snake.GetFromIndex(i);
            var script = snakePart.GetComponent<SnakePartController>();
            script.previousPosition = script.position;
            if (i == 0)
            {
                posFromPreviousPart = script.position;
                script.position += direction;
            }
            else
            {
                (script.position, posFromPreviousPart) = (posFromPreviousPart, script.position);
                nextToLastPart = snakePart;
            }


            if (i == snake.Count - 1 && script.isColliding)
            {
                script.isColliding = false;
                Destroy(script.collidingApple);
                var newTail = Instantiate(snakePartPrefab, posFromPreviousPart, Quaternion.identity);
                script = newTail.GetComponent<SnakePartController>();
                script.position = posFromPreviousPart;
                script.previousPosition = posFromPreviousPart;
                snake.Add(newTail);
            }
        }

        movesDone++;
    }

    private IEnumerator MakeAMove(float delay)
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
