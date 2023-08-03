using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FruitCollecter : MonoBehaviour
{
    [SerializeField] private CherryManager _cherryManager;
    [SerializeField] private TMP_Text _cherriesText;
    
    private int _cherryCount;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Look for cherry components on the thing we've collided with
        Cherry possibleCherry = collider.GetComponent<Cherry>();
        
        // If there is a cherry component, then we've found a cherry! Consume it!
        if (possibleCherry != null)
        {
            _cherryManager.DestroyCherry(possibleCherry.gameObject);
            _cherryCount++;
            _cherriesText.text = "Cherries: " + _cherryCount;
        }
    }
}
