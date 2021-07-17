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
        taken_pos = new List<bool>();
        rooms = new List<string>();
        for (int i = 0; i < 9; ++i)
            taken_pos.Add(false);
        pos = new List<Vector3>();
        pos.Add(new Vector3(19 + x_offset, 12 + y_offset));
        pos.Add(new Vector3(38 + x_offset, 12 + y_offset));
        pos.Add(new Vector3(57 + x_offset, 12 + y_offset));
        pos.Add(new Vector3(19 + x_offset, 0 + y_offset));
        pos.Add(new Vector3(38 + x_offset, 0 + y_offset));
        pos.Add(new Vector3(57 + x_offset, 0 + y_offset));
        pos.Add(new Vector3(19 + x_offset, -12 + y_offset));
        pos.Add(new Vector3(38 + x_offset, -12 + y_offset));
        pos.Add(new Vector3(57 + x_offset, -12 + y_offset));

        //key chest
        lib.Add(new Pair("E3", "3E", "Chemistry", "What animal is made up of calcium, nickel and neon?", "CaNiNe"));
        lib.Add(new Pair("E1", "1E", "Mathematics", "I am an odd number. Take away one letter and I become even. What number am I?", "seven"));
        lib.Add(new Pair("34", "43", "Physics", "Why is a physics book always unhappy?", "lots of problems"));
        lib.Add(new Pair("12", "21", "Mathematics", "If 72 x 96 = 6927, 58 x 87 = 7885, then 79 x 86 = ?", "6897"));
        lib.Add(new Pair("2M", "M2", "Tricky riddle", "Who has the fish?", "Albert Einstein"));
        lib.Add(new Pair("4M", "M4", "Mathematics", "Look at this series: 53, 53, 40, 40, 27, 27, ... What number should come next?", "14"));

        //hint chest
        lib.Add(new Pair("II", "II", "Mathematics", "A farmer has 17 sheep and all but 9 die. How many are left?", "9"));
        lib.Add(new Pair("NN", "NN", "Science", "What is full of holes but still holds water?", "sponge"));
        lib.Add(new Pair("SS", "SS", "Mathematics", "If two's company and three's a crowd, what are four and five?", "9"));
        lib.Add(new Pair("YY", "YY", "Geography", "I am a rock bigger than Venus but smaller than Uranus. What am I?", "Earth"));

        //dummy chest
        lib.Add(new Pair("Da", "aD", "Physics", "What planet has the shortest year?", "Mercury"));
        lib.Add(new Pair("Db", "bD", "Science", "What kind of tree can fit into your hand?", "palm tree"));
        lib.Add(new Pair("Dc", "cD", "Physics", "What is the loneliest of all physics concepts?", "singularity"));
        lib.Add(new Pair("Dd", "dD", "Geography", "How many seas in the world touch at least three continents?", "1"));
        lib.Add(new Pair("De", "eD", "Chemistry", "What is the most uninteresting of all the periodic elements?", "Boron"));
        lib.Add(new Pair("Df", "fD", "Chemistry", "What can eat a lot of iron without getting sick?", "rust"));
        lib.Add(new Pair("Dg", "gD", "Mathematics", "How many sides does a circle have?", "2"));
        lib.Add(new Pair("Dh", "hD", "Biology", "What animal lives longest in zoos?", "turtle"));
        lib.Add(new Pair("Di", "iD", "Chemistry", "What kind of chemical element hates to be a follower?", "Lead"));
        lib.Add(new Pair("Dj", "jD", "Science", "What has 3 feet but cannot walk?", "yardstick"));
        lib.Add(new Pair("Dk", "kD", "Mathematics", "I am a three-digit number. My tens digit is six more than my ones digit. My hundreds digit is eight less than my tens digit. What number am I?", "193"));
        lib.Add(new Pair("Dl", "lD", "Geography", "Whats the name of the continent Which contains Nigeria, Libya, Rwanda, Cameroon, Mali and Namibia ?", "Africa"));
        lib.Add(new Pair("Dm", "mD", "Tricky", "What 5-letter word becomes shorter when you add two letters to it?", "short"));
        lib.Add(new Pair("Dn", "nD", "Tricky", "What kind of room has no windows or doors?", "mushroom"));
        lib.Add(new Pair("Do", "oD", "Science", "It's been around for millions of years, but its no more than a month old. What is it?", "moon"));
        lib.Add(new Pair("Dp", "pD", "Mathematics", "What kinds of trees are mirror images?", "Symmetries"));
        lib.Add(new Pair("Dq", "qD", "Tricky", "What goes around the world and stays in a corner?", "stamp"));
        lib.Add(new Pair("Dr", "rD", "Sport", "Name the only sport in which the ball is always in possession of the team on defense, and the offensive team can score without touching the ball?", "baseball"));
        lib.Add(new Pair("Ds", "sD", "Mathematics", "What do you call an angle which is adorable?", "acute angle"));
        lib.Add(new Pair("Dt", "tD", "Tricky", "No sooner spoken than broken. What is it?", "silence"));
        /*
         lib.Add(new Pair("","","Mathematics IV","You have 50 biscuits. How many times can you subtract 5 from 50 biscuits?","once"));
         */

        rooms.Add("EntranceRoom"); rooms.Add("MainRoom");
        rooms.Add("Room1"); rooms.Add("Room2");
        rooms.Add("Room3"); rooms.Add("Room4");

        hint_msg.Add("Hello..\nI'm a exceptional senior student in high school! And.. You know..");
        hint_msg.Add("University entrance exams are coming..");
        //hint_msg.Add("I want to have absolute score!\nBecause.. I'm exceptional..\nSo.. I came up with the greatest plan ever..");
        //plot.Add("I'M GOING TO STEEEAAAAAAL\n...THE QUESTION SHEETS...\nOHH.... shhhhhhhhh and you're going to help me ;)");
        //plot.Add("I have already stole the key to the house\nBut.. eh.. it's open already\nLOOK..");
        //plot.Add("We are expected to look for the question sheets even if we have to search every corner of the house.");
        //plot.Add("I heard there are a lots of riddles\n..which are your favourites.. ;)\nLet's go!");
        Freeze = true;
        StoryUI.Instance.setPlot(hint_msg).Show();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isEsc)
            {
                Freeze = true;
                isEsc = true;
                DialogUI.Instance.setTitle("Do you want to quit?").setQuestion("Type <yes> to quit").Show();
            }
            else
            {
                isEsc = false;
                DialogUI.Instance.Hide();
            }
        }
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
                case "II":
                    showHint("The Swede keeps dogs.");
                    break;
                case "NN":
                    showHint("The Pall Mall smoker keeps birds.");
                    break;
                case "SS":
                    showHint("The German smokes Prince.");
                    break;
                case "YY":
                    showHint("The Norwegian lives next to the blue house.");
                    break;
                case "Da":
                case "Db":
                case "Dc":
                case "Dd":
                case "De":
                case "Df":
                case "Dg":
                case "Dh":
                case "Di":
                case "Dj":
                case "Dk":
                case "Dl":
                case "Dm":
                case "Dn":
                case "Do":
                case "Dp":
                case "Dq":
                case "Dr":
                case "Ds":
                case "Dt":
                    showInfo("Nothing here...");
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
