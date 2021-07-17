using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float smoothSpeed = 0.025f;

    void Start()
    {
        target = new Vector3(0, 0, -10);
    }

    public void changeCameraPosition(Vector2 pos)
    {
        target.x = pos.x; target.y = pos.y; target.z = -10;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target;
        desiredPosition.z = -10;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
