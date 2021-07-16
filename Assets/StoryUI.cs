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

        List<string> plot;
        int count = 0;
        string to_be_seen = "";
        int i = -1;

        void Awake()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(nextPage);
            player = GameObject.Find("Cuphead");
            player.GetComponent<CupheadController>().Freeze = true;

            plot = new List<string>();
            plot.Add("Hello..\nI'm a exceptional senior student in high school! And.. You know\nUniversity entrance exams are coming..");
            plot.Add("I want to have absolute score!\nBecause.. I'm exceptional..\nSo.. I came up with the greatest plan ever..");
            plot.Add("I'M GOING TO STEEEAAAAAAL\n...THE QUESTION SHEETS...\nOHH.... shhhhhhhhh and you're going to help me ;)");
            plot.Add("I have already stole the key to the house\nBut.. eh.. it's open already\nLOOK..");
            plot.Add("We are expected to look for the question sheets even if we have to search every corner of the house.");
            plot.Add("I heard there are a lots of riddles\n..which are your favourites.. ;)\nHelp me!");
        }

        public void Show()
        {
            canvas.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (count < plot.Count && i < plot[count].Length - 1)
            {
                player.GetComponent<CupheadController>().Freeze = true;
                ++i;
                to_be_seen += plot[count][i];
                lines.text = to_be_seen;
            }
        }

        public void nextPage()
        {
            if (i < plot[count].Length-1)
                return;
            ++count; to_be_seen = ""; i = -1;
            if (count == plot.Count - 1)
            {
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = "X";
            }
            if (count == plot.Count)
                Hide();
            else
                lines.text = plot[count];
        }

        public void Hide()
        {
            player.GetComponent<CupheadController>().Freeze = false;
            canvas.SetActive(false);
        }
    }
}
