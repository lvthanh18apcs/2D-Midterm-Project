using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupheadController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D player;
    public Animator anim;
    GameObject m_camera;
    CameraController cam;

    string tileset_name = "Dungeon_Tileset_";
    float speed = 4f;
    float x_offset = 0.9906979f, y_offset = 0.4241866f;
    public bool lockDoor = true;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_camera = GameObject.Find("Main Camera");
        cam = (CameraController)m_camera.GetComponent(typeof(CameraController));
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

    private void Teleport(string room, bool hor, int offset)
    {
        Vector2 pos = GameObject.Find(room).transform.position;
        pos.x -= x_offset; pos.y -= y_offset;
        cam.changeCameraPosition(pos);
        if (hor)
            pos.x += offset;
        else
            pos.y += offset;
        transform.position = pos;
    }

    public void openDoor(string name)
    {
        string opposite = "";
        opposite += name[1]; opposite += name[0];
        openDoor(opposite);
        SpriteRenderer target = GameObject.Find(name).GetComponent<SpriteRenderer>();
        Object[] sprites = Resources.LoadAll("Dungeon_Tileset");
        if (target.sprite.name == tileset_name + "10")
        {
            target.sprite = (Sprite)sprites[7];
        }
        else if (target.sprite.name == tileset_name + "11")
        {
            target.sprite = (Sprite)sprites[8];
        }
        else if (target.sprite.name == tileset_name + "12")
        {
            target.sprite = (Sprite)sprites[9];
        }
        else if (target.sprite.name == tileset_name + "13")
        {
            target.sprite = (Sprite)sprites[10];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer valid;
        string door = collision.gameObject.name;
        //Debug.Log(collision.gameObject.name);
        valid = GameObject.Find(door).GetComponent<SpriteRenderer>();
        switch (door)
        {
            case "EM":
                if (valid.sprite.name == tileset_name + "12" && lockDoor)
                    break;
                Teleport("MainRoom",true,-5);
                break;
            case "ME":
                if (valid.sprite.name == tileset_name + "11" && lockDoor)
                    break;
                Teleport("EntranceRoom", true, 5);
                break;
            case "E1":
                if (valid.sprite.name == tileset_name + "10" && lockDoor)
                    break;
                Teleport("Room1", false, -2);
                break;
            case "1E":
                if (valid.sprite.name == tileset_name + "13" && lockDoor)
                    break;
                Teleport("EntranceRoom", false, 2);
                break;
            case "E3":
                if (valid.sprite.name == tileset_name + "13" && lockDoor)
                    break;
                Teleport("Room3", false, 2);
                break;
            case "3E":
                if (valid.sprite.name == tileset_name + "10" && lockDoor)
                    break;
                Teleport("EntranceRoom", false, -2);
                break;
            case "12":
                if (valid.sprite.name == tileset_name + "12" && lockDoor)
                    break;
                Teleport("Room2", true, -5);
                break;
            case "21":
                if (valid.sprite.name == tileset_name + "11" && lockDoor)
                    break;
                Teleport("Room1", true, 5);
                break;
            case "2M":
                if (valid.sprite.name == tileset_name + "13" && lockDoor)
                    break;
                Teleport("MainRoom", false, 2);
                break;
            case "M2":
                if (valid.sprite.name == tileset_name + "10" && lockDoor)
                    break;
                Teleport("Room2", false, -2);
                break;
            case "34":
                if (valid.sprite.name == tileset_name + "12" && lockDoor)
                    break;
                Teleport("Room4", true, -5);
                break;
            case "43":
                if (valid.sprite.name == tileset_name + "11" && lockDoor)
                    break;
                Teleport("Room3", true, 5);
                break;
            case "4M":
                if (valid.sprite.name == tileset_name + "10" && lockDoor)
                    break;
                Teleport("MainRoom", false, -2);
                break;
            case "M4":
                if (valid.sprite.name == tileset_name + "13" && lockDoor)
                    break;
                Teleport("Room4", false, 2);
                break;
            default:
                break;
        }
    }
}
