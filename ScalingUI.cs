using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *          INFO
 *          
 *     This ScalingUI function was used in an adventure game where the NPC characters needed an indicator that would direct the player to interact with them.
 *     The result is an exlamation mark (!) icon above the NPC, its size stays constant in relation to the size of the screen - it appears larger in the distance, but shinks
 *     as the player moves closer to it. It simultaneously moves up and down in a bouncy motion and always faces the camera.
 *     At its activation it scales up from 0 to the desired size during a set amount of frames.
 *          
 *     Timestamped link for the code in action:
 *     https://youtu.be/7HXWGKQ49kY?t=54
*/

public class ScalingUI : MonoBehaviour
{
    public float FixedSize = 0.05f;
    public Camera Camera;
    public float startScale = 0;

    void Update()
    {
        var distance = (Camera.transform.position - transform.position).magnitude;
        var size = distance * FixedSize * Camera.fieldOfView;
        transform.localScale = Vector3.one * size /10;
        transform.forward = transform.position - Camera.transform.position;

        if (startScale < 1)
        {
            transform.localScale *= startScale;
            startScale += 0.05f;
        }
    }
}
