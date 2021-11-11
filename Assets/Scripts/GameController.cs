using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    
    public GameObject applePrefab;
    private LinkedList<GameObject> snake = SnakeController.snake;
    public static Vector3 startPos = new Vector3(16  , 2, 16);
    public float spawnDelay = 3f;
    public static bool gameOver = false;

    private void Start()
    {
        StartCoroutine(AppleSpawner(spawnDelay));
    }

    private void Update()
    {
        if (gameOver) { StopAllCoroutines(); }
    }

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

    private IEnumerator AppleSpawner(float spawnDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnApple();
        }
    }
    
    
    

    
}
    