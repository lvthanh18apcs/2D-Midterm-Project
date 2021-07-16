using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Story
{
    public class StoryUI : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Text lines;
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] GameObject canvas;

        GameObject player;

        StoryUI story = new StoryUI();

        public static StoryUI Instance;

        List<string> plot;
        int count = 0;

        void Awake()
        {
            Instance = this;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Hide);
            player = GameObject.Find("Cuphead");
            player.GetComponent<CupheadController>().Freeze = true;

            plot = new List<string>();
            plot.Add("Hello.. I'm a exceptional\nsenior student in high school!\n" +
                "University entrance exams\n are coming..");
            plot.Add("I want to have absolute score!\nbecause I'm exceptional\n" +
                "I have the greatest plan ever");
            plot.Add("I'M GOING TO STEEEAAAAAAL\nTHE QUESTION SHEETS\nOHH.... shhhhhhhhh\n" +
                "you're going to help me");
            plot.Add("I have already stole the key\nto the house\nBut.. eh.. it's open already\n" +
                "LOOK");
            plot.Add("We are expected to\nlook for the question sheets\neven if we have to search\n" +
                "every corner of the house");
            plot.Add("I heard there are a\nlots of riddles\nwhich are your favourites ;)\n" +
                "Help me!");
        }

        public void Show()
        {
            canvas.SetActive(true);
        }

        public void nextPage()
        {
            ++count;
            if (count == plot.Count - 1)
            {
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = "X";
            }
            if (count == plot.Count)
                Hide();
            else
                story.lines.text = plot[count];
        }

        public void Hide()
        {
            player.GetComponent<CupheadController>().Freeze = false;
            canvas.SetActive(false);
        }
    }
}
