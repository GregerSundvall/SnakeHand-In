using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject snakeHead;
    private Transform snakeHeadTF;
    private Vector3 snakeHeadPreviousPos;
    private Vector3 snakeHeadPos;
    
    // Start is called before the first frame update
    void Start()
    {
        snakeHead = SnakeController.snake.GetFromIndex(0);
        snakeHeadTF = snakeHead.transform;
        snakeHeadPos = snakeHeadTF.position;
        snakeHeadPreviousPos = snakeHeadPos;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(new Vector3(16, 0, 16), Vector3.up, 1 * Time.deltaTime);
        if (snakeHeadPos != snakeHeadTF.position)
        {
            snakeHeadPreviousPos = snakeHeadPos;
            snakeHeadPos = snakeHeadTF.position;
        }
        var camPosX = snakeHeadPos.x + SnakeController.direction.x * -30;
        var camPosY = snakeHeadPos.y + 20;
        var camPosZ = snakeHeadPos.z + SnakeController.direction.z * -30;
        var camPos = new Vector3(camPosX, camPosY, camPosZ);
        transform.position = Vector3.Lerp(transform.position, camPos, 4f * Time.deltaTime);
        //transform.localPosition = snakeHeadPos + SnakeController.direction * -5;
        //var camTarget = transform.TransformPoint(new Vector3(0, 0, 0));
        transform.LookAt(Vector3.Lerp(snakeHeadPreviousPos, snakeHeadPos, 4f * Time.deltaTime));
        
        //transform.LookAt(transform.localPosition + new Vector3(0, -20, 30));
    }
}
