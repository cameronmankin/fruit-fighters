using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // This is a reference to the player character's Transform component, which describes position and orientation
    // See: https://docs.unity3d.com/Manual/class-Transform.html
    [SerializeField] private Transform _player;
    
    // LateUpdate is called after every Update is already called for the frame
    // PLEASE SEE THIS VERY USEFUL CHART: https://docs.unity3d.com/Manual/ExecutionOrder.html
    void LateUpdate()
    {
        // If this component is attached to a Camera component, setting transform.position sets the camera position
        transform.position = new Vector3(_player.position.x, _player.position.y, 0);
        
        // NOTE: We chose to set the camera's z position to be 0, ignoring the player's z position.
        //  For a 2d game, z doesn't matter (x and y are the 2 dimensions in question),
        //  as long as the camera's "clipping planes" are set to include the plane of action.
    }
}
