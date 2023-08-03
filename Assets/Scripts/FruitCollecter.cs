using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollecter : MonoBehaviour
{
    [SerializeField] private FruitManager _fruitManager;
    [SerializeField] private TMP_Text _fruitText;
    
    private int _fruitCount;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Look for fruit components on the thing we've collided with
        Fruit possibleFruit = collider.GetComponent<Fruit>();
        
        // If there is a fruit component, then we've found a fruit! Consume it!
        if (possibleFruit != null)
        {
            _fruitManager.UnlistFruit(possibleFruit.gameObject);
            _fruitCount++;
            _fruitText.text = "Fruit: " + _fruitCount;
            possibleFruit.DestroyFruit();
        }
    }
}
