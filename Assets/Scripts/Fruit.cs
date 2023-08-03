using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Animator _animator;
    public Collider2D _collider;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    public void DestroyFruit()
    {
        _animator.SetBool("IsDestroyed", true);
        _collider.enabled = false;
        Destroy(gameObject, .5f);
    }
}
