using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Dialogs;

public class CupheadController : MonoBehaviour
{
    public class Pair
    {
        public string door;
        public string alt_door;
        public string question;
        public string title;
        public string answer;
        public Pair(string door, string alt_door, string title, string question, string answer)
        {
            this.door = door;
            this.alt_door = alt_door;
            this.answer = answer;
            this.title = title;
            this.question = question;
        }
    }

    public Rigidbody2D player;
    public Animator anim;
    GameObject m_camera;
    CameraController cam;
    public GameObject dialogPanel;
    public GameObject floatingText;


    string tileset_name = "Dungeon_Tileset_";
    float speed = 4f;
    float x_offset = 0.9906979f, y_offset = 0.4241866f;
    public bool lockDoor = true;
    public bool Freeze = false;
    int index = -1;
    List<Pair> lib;
    string chestName = "";
    bool WIN = false;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_camera = GameObject.Find("Main Camera");
        cam = (CameraController)m_camera.GetComponent(typeof(CameraController));
        DialogUI.Instance.Hide();
        initAnswer();
    }

    private void initAnswer()
    {
        lib = new List<Pair>();
        lib.Add(new Pair("E3", "3E", "Key to Room 3", "Test question", ""));
        lib.Add(new Pair("E1", "1E", "Key to Room 1", "Test question", ""));
        lib.Add(new Pair("34", "43", "Key to Room 4", "Test question", ""));
        lib.Add(new Pair("12", "21", "Key to Room 2", "Test question", ""));
        lib.Add(new Pair("2M", "M2", "Key to Main Chest", "Test question", ""));
        lib.Add(new Pair("4M", "M4", "Key to Main Room", "Test question", ""));
    }

    private void FixedUpdate()
    {
        Quaternion flip = transform.rotation;
        Vector2 velo = new Vector2(0, 0);

        if (!Freeze)
        {
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

    private void openDoor(string name)
    {
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

    private void openChest(string name)
    {
        SpriteRenderer target = GameObject.Find(name).GetComponent<SpriteRenderer>();
        Object[] sprites = Resources.LoadAll("Items");
        if (target.sprite.name == "Items_1")
        {
            target.sprite = (Sprite)sprites[1];
        }
        else if (target.sprite.name == "Items_2")
        {
            target.sprite = (Sprite)sprites[4];
        }
    }

    private void showInfo(string info)
    {
        if (floatingText)
        {
            GameObject floatingPrefab = Instantiate(floatingText, transform.position, Quaternion.identity);
            floatingPrefab.GetComponentInChildren<TextMesh>().text = info;
            Destroy(floatingPrefab, 1f);
        }
    }

    private bool checkDoorStatus(string sprite_name,string door)
    {
        if (sprite_name == tileset_name + "12" || sprite_name == tileset_name + "10" || sprite_name == tileset_name + "11" || sprite_name == tileset_name + "13")
        {
            showInfo("Cannot go through here yet");
            return false;
        }
        return true;
    }

    public void update_input(string input)
    {
        DialogUI.Instance.Hide();
        Freeze = false;
        if (input == lib[index].answer)
        {
            openDoor(lib[index].door);
            openDoor(lib[index].alt_door);
            openChest(chestName);
            string keyword = lib[index].door;
            switch (keyword)
            {
                case "E3":
                    showInfo("Room 3 is unlocked");
                    break;
                case "E1":
                    showInfo("Room 1 is unlocked");
                    break;
                case "34":
                    showInfo("Room 4 is unlocked");
                    break;
                case "12":
                    showInfo("Room 2 is unlocked");
                    break;
                case "4M":
                    showInfo("Main Room is unlocked");
                    break;
                case "2M":
                    showInfo("Main Chest is unlocked");
                    WIN = true;
                    break;
                default:
                    break;
            }
        }
    }

    public void hitChest(string chest)
    {
        if (WIN && chest == "WINCHEST")
        {
            anim.SetBool("win", true);
            openChest(chest);
        }

        if (chest.Length < 5)
            return;
        index = -1;
        chestName = "";
        string chestStatus = GameObject.Find(chest).GetComponent<SpriteRenderer>().sprite.name;
        if (chestStatus == "Items_3" || chestStatus == "Items_0")
        {
            showInfo("I have already looted this chest");
            return;
        }
        string door = "";
        door += chest[3]; door += chest[4];
        for(int i =0;i<lib.Count;++i)
            if (lib[i].door == door || lib[i].alt_door == door)
            {
                index = i;
                chestName = chest;
                break;
            }
        if (index == -1)
        {
            showInfo("There must be something in here");
            return;
        }
        DialogUI.Instance.setTitle(lib[index].title).setQuestion(lib[index].question).Show();
        Freeze = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer valid;
        string c_object = collision.gameObject.name;
        valid = GameObject.Find(c_object).GetComponent<SpriteRenderer>();
        switch (c_object)
        {
            case "EM":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("MainRoom",true,-5);
                break;
            case "ME":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("EntranceRoom", true, 5);
                break;
            case "E1":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room1", false, -2);
                break;
            case "1E":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("EntranceRoom", false, 2);
                break;
            case "E3":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room3", false, 2);
                break;
            case "3E":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("EntranceRoom", false, -2);
                break;
            case "12":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room2", true, -5);
                break;
            case "21":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room1", true, 5);
                break;
            case "2M":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("MainRoom", false, 2);
                break;
            case "M2":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room2", false, -2);
                break;
            case "34":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room4", true, -5);
                break;
            case "43":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room3", true, 5);
                break;
            case "4M":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("MainRoom", false, -2);
                break;
            case "M4":
                if (checkDoorStatus(valid.sprite.name, c_object))
                    Teleport("Room4", false, 2);
                break;
            default:
                hitChest(c_object);
                break;
        }
    }
}
