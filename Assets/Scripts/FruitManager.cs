using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FruitManager : MonoBehaviour
{
    // A Prefab is a GameObject in the Assets folder
    //  Spawning "instances" of prefabs is the easiest way to add things to the scene at runtime
    [SerializeField] private List<GameObject> _fruitPrefab;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private List<GameObject> _spawnedFruit;
    
    private float _timeSinceLastSpawn = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (_spawnedFruit.Count >= 3)
        {
            // If 3 cherries are currently spawned, do nothing
        }
        else
        {
            // Increment our spawn timer
            _timeSinceLastSpawn += Time.deltaTime;
            
            // If enough time has elapsed, spawn a fruit
            if (_timeSinceLastSpawn >= _spawnDelay)
            {
                // Pick a random spawn position and a ranom fruit type from our list of prefabs.
                float randomX = Random.Range(-5, 5);
                float randomY = Random.Range(0, 5);
                GameObject randomFruit = _fruitPrefab[Random.Range(0, _fruitPrefab.Count)];
                
                // Instantiate() adds a new instance of a GameObject to the scene
                // See: https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
                GameObject newFruit = Instantiate(randomFruit, new Vector3(randomX, randomY, 0), transform.rotation);
                _spawnedFruit.Add(newFruit);
                
                // Reset the spawn timer
                _timeSinceLastSpawn = 0f;
            }
        }
    }

    public void UnlistFruit(GameObject fruit)
    {
        _spawnedFruit.Remove(fruit);
    }
}
