using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FixedCameraFollowSmooth();

    }

    //     Follow Two Transforms with a Fixed-Orientation Camera
    public void FixedCameraFollowSmooth()
    {
        if (transform.position.z < -15.5)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -15.4f), transform.rotation);
        }
        if (transform.position.z > -8.4)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -8.4f), transform.rotation);
        }
        Transform t1 = playerOne.transform;
        Transform t2 = playerTwo.transform;
        // How many units should we keep from the players
        float zoomFactor = 0.7f;
        float followTimeDelta = 0.2f;

        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;
        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
     //   midpoint.y = midpoint.y + 1.8f;
        
        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - transform.forward * distance * zoomFactor;
        cameraDestination.y += 1f;


        //Move the camera from original position to cameraDestination
        transform.position = Vector3.Slerp(transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - transform.position).magnitude <= 0.05f)
            transform.position = cameraDestination;


    }
}
