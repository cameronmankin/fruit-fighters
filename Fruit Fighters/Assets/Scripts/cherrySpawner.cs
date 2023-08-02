using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherrySpawner : MonoBehaviour
{
    [SerializeField] private GameObject cherry;
    [SerializeField] private float spawnRate = 1f;
    private float elapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        int cherries = GameObject.FindGameObjectsWithTag("Cherry").Length;

        if (cherries < 3)
            elapsed += Time.deltaTime;
 
        if (cherries < 3 && elapsed >= spawnRate)
            {    
            Instantiate(cherry, new Vector3(Random.Range(-5, 5), Random.Range(0, 5), 0), transform.rotation);
            elapsed = 0f;
            }
    }
}
