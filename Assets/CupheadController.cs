using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupheadController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D player;
    public Animator anim;
    GameObject camera;

    float speed = 4f;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Quaternion flip = transform.rotation;
        Vector2 velo = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            velo.y = speed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            velo.x = -speed;
            flip.y = 180;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            velo.y = -speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            velo.x = speed;
            flip.y = 0;
        }
        player.velocity = velo;
        transform.rotation = flip;

        anim.SetInteger("velox", (int)velo.x);
        anim.SetInteger("veloy", (int)velo.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float x_offset = 0.9906979f, y_offset = 0.4241866f;
        if (collision.gameObject.name == "test")
        {
            CameraController cam = (CameraController)camera.GetComponent(typeof(CameraController));
            Vector2 pos = GameObject.Find("MainRoom").transform.position;
            pos.x -= x_offset; pos.y -= y_offset;
            cam.changeCameraPosition(pos);
            pos.x -= 5;
            transform.position = pos;
        }
    }
}
