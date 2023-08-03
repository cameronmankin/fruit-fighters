using System.Collections;
using UnityEngine;

// Fruit primarily exists to be a component identifiable by FruitCollector.

public class Fruit : MonoBehaviour
{
    public Animator _animator;
    public Collider2D _collider;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    // This method is called by the FruitCollector on contact.
    public void DestroyFruit()
    {
        // We begin the destruction animation.
        _animator.SetBool("IsDestroyed", true);

        // We toggles the fruit's collider off, so we don't accidentally collect it twice.
        _collider.enabled = false;

        // Finally, we destroy the fruit, after a .5 second delay - enough time for the animation to play.
        Destroy(gameObject, .5f);
    }
}
