using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //variable for the camera target
    public Transform target;

    //speed for the camera to follow the player
    public float smoothing = 5f;

    //variable for the offset
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //offset vector for the camera
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Position for the camera to move to
        Vector3 targetCamPos = target.position + offset;

        //Lerp the position of the camera to the offset time over the smoothing time period
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);



    }
}
