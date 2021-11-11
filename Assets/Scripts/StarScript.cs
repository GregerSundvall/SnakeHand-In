using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{

    public GameObject starPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(starPrefab, GetStarPosition(), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetStarPosition()
    {
        var random = Random.Range(-1000f, 1000f);
        return new Vector3(random, random, random);
    }
}
