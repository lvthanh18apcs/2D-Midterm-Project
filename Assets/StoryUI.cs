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

        public static StoryUI Instance;

        void Awake()
        {
            Instance = this;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(nextPage);
            player = GameObject.Find("Cuphead");
            player.GetComponent<CupheadController>().Freeze = true;
            plot = new List<string>();
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

        public StoryUI setPlot(List<string> msg)
        {
            this.plot.Clear();
            i = -1;
            count = 0;
            plot = new List<string>(msg);
            if (count == plot.Count - 1)
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = "X";
            else
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = ">";
            return Instance;
        }

        public void nextPage()
        {
            if (i < plot[count].Length-1)
                return;
            ++count; to_be_seen = ""; i = -1;
            if (count == plot.Count - 1)
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = "X";
            if (count == plot.Count)
            {
                Hide();
                if (plot.Count > 1)
                    SoundManager.playSound("intro");
            }
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
