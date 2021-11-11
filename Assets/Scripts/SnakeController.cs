using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static LinkedList<GameObject> snake = new LinkedList<GameObject>();
    public static float moveDelay = 0.6f;
    public GameObject snakePartPrefab;
    private Vector3 startPos = GameController.startPos;
    public int movesDone = 0;  
    public static int applesPicked = 0;

    private Vector3 north = new Vector3(0, 0, 1);
    private Vector3 south = new Vector3(0, 0, -1);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    List<Vector3> directions = new List<Vector3>();
    
    public static Vector3 direction;
    private int directionInput = 0;
    private int previousDirectionInput = 0;
    private bool changeInDirection = false;
    private GameObject nextToLastPart;
    private bool addSnakePart = false;
    private Vector3 posFromPreviousPart = Vector3.zero; // Temp position, to be passed on as next part's target pos.


    private void Awake()
    {
        // Create first snake part and add to snake.
        var newPart = Instantiate(snakePartPrefab, startPos, Quaternion.identity);
        var script = newPart.GetComponent<SnakePartController>();
        script.position = startPos;
        script.previousPosition = startPos;
        snake.Add(newPart);
    }

    private void Start()
    {
        
        
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
        if (GameController.gameOver) { StopAllCoroutines(); }
    }
    
    
    private void GetInput()
    {
        // Sets directionInput to last pressed direction key.
        
        if (Input.GetKeyDown("w"))
        {
            directionInput = 0;
            changeInDirection = true;
        } else if (Input.GetKeyDown("a"))
        {
            directionInput = -1;
            changeInDirection = true;
        } else if (Input.GetKeyDown("d"))
        {
            directionInput = 1;
            changeInDirection = true;
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
        changeInDirection = false;
    }

    private void Move()
    {
        if (addSnakePart)
        {
            var newTail = Instantiate(snakePartPrefab, posFromPreviousPart, Quaternion.identity);
            var newTailScript = newTail.GetComponent<SnakePartController>();
            newTailScript.position = posFromPreviousPart;
            newTailScript.previousPosition = posFromPreviousPart;
            snake.Add(newTail);
            addSnakePart = false;
        }
        
        if (changeInDirection)
        {
            SetDirection();
        }

        for (var i = 0; i < snake.Count; i++)
        {
            var snakePart = snake.GetFromIndex(i);
            var script = snakePart.GetComponent<SnakePartController>();
            script.previousPosition = script.position;
            if (i == 0)
            {
                // If current part is the head, get new position from direction.
                script.previousPosition = script.position;
                posFromPreviousPart = script.position;
                script.position += direction;
            }
            else
            {
                // If current part is not the head, get new position from previous parts old position.
                script.previousPosition = script.position;
                (script.position, posFromPreviousPart) = (posFromPreviousPart, script.position);
                nextToLastPart = snakePart;
            }

            if (i == snake.Count - 1)
            {
                snakePart.GetComponent<SphereCollider>().isTrigger = true;
            }

            if (i == snake.Count - 1 && script.isColliding)
            {
                script.isColliding = false;
                Destroy(script.collidingApple);
                addSnakePart = true;
            }
        }
        movesDone++;
    }

    private IEnumerator MakeAMove(float delay)
    {
        while (true)
        {
            Move();
            if(moveDelay > 0.1f)
            {
                moveDelay *= 0.999f;
            }
            yield return new WaitForSeconds(delay);
        }
    }


}
