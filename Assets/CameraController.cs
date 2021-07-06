using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
    
    }

    public void changeCameraPosition(Vector2 pos)
    {
        transform.Translate(pos);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
