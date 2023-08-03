using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CherryManager : MonoBehaviour
{
    // A Prefab is a GameObject in the Assets folder
    //  Spawning "instances" of prefabs is the easiest way to add things to the scene at runtime
    [SerializeField] private GameObject _cherryPrefab;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private List<GameObject> _spawnedCherries;
    
    private float _timeSinceLastSpawn = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (_spawnedCherries.Count >= 3)
        {
            // If 3 cherries are currently spawned, do nothing
        }
        else
        {
            // Increment our spawn timer
            _timeSinceLastSpawn += Time.deltaTime;
            
            // If enough time has elapsed, spawn a cherry
            if (_timeSinceLastSpawn >= _spawnDelay)
            {
                // Pick a random spawn position
                float randomX = Random.Range(-5, 5);
                float randomY = Random.Range(0, 5);
                
                // Instantiate() adds a new instance of a GameObject to the scene
                // See: https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
                GameObject newCherry = Instantiate(_cherryPrefab, new Vector3(randomX, randomY, 0), transform.rotation);
                _spawnedCherries.Add(newCherry);
                
                // Reset the spawn timer
                _timeSinceLastSpawn = 0f;
            }
        }
    }

    public void DestroyCherry(GameObject cherry)
    {
        _spawnedCherries.Remove(cherry);
        Destroy(cherry);
    }
}
