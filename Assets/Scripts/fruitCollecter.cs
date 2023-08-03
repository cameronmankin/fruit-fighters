using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class fruitCollecter : MonoBehaviour
{
    private int cherryCount;

    [SerializeField] private TMP_Text cherriesText;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cherry"))
        {
            Destroy(collider.gameObject);
            cherryCount++;
            cherriesText.text = "Cherries: " + cherryCount;
        }
    }
}
