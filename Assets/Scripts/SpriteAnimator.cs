using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _frameRate;
    [SerializeField] private List<AnimDescription> _animations;
    
    // [Serializable] marks this struct for use with Unity's [SerializeField] in the list above
    [Serializable]
    public struct AnimDescription
    {
        public string Name;
        public List<Sprite> Sprites;
    }
    
    private AnimDescription _currentAnim;
    private int _currentFrame;

    void Start()
    {
        StartCoroutine(AnimationTimer());
    }
    
    public void PlayAnimation(string animName)
    {
        // Take no action if the animation hasn't changed
        if (animName != _currentAnim.Name)
        {
            // Find the animation with a matching name
            foreach (var anim in _animations)
                if (anim.Name == animName)
                {
                    // Set the new animation and reset progress to 0
                    _currentAnim = anim;
                    _currentFrame = 0;
                    break;
                }
        }
    }
    
    private IEnumerator AnimationTimer()
    {
        while (true)
        {
            // Update the sprite every 1 / frameRate seconds
            yield return new WaitForSeconds(1f / _frameRate);
            
            // Check currentAnim validity
            if (_currentAnim.Sprites != null && _currentAnim.Sprites.Count > 0)
            {
                // Show the current frame's sprite
                _renderer.sprite = _currentAnim.Sprites[_currentFrame];
                
                // Increment and loop the current frame
                _currentFrame++;
                _currentFrame %= _currentAnim.Sprites.Count;
            }
        }
    }
}
