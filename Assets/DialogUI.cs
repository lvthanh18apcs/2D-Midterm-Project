using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUI.Dialogs
{
    public class Dialog
    {
        public string title;
        public string question;
    }
    public class DialogUI : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Text title;
        [SerializeField] UnityEngine.UI.Text question;
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] GameObject canvas;
        [SerializeField] UnityEngine.UI.InputField input;

        GameObject player;
        GameObject cam;

        Dialog dialog = new Dialog();

        public static DialogUI Instance;

        void Awake()
        {
            Instance = this;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Hide);
            player = GameObject.Find("Cuphead");
            cam = GameObject.Find("Main Camera");
        }

        public DialogUI setTitle(string title)
        {
            dialog.title = title;
            return Instance;
        }

        public DialogUI setQuestion(string question)
        {
            dialog.question = question;
            return Instance;
        }

        public void Show()
        {
            title.text = dialog.title;
            question.text = dialog.question;
            dialog = new Dialog();
            canvas.SetActive(true);
        }

        public void Hide()
        {
            input.text = "";
            player.GetComponent<CupheadController>().Freeze = false;
            canvas.SetActive(false);
            dialog = new Dialog();
            
        }

        private void FixedUpdate()
        {
            if (transform.position != cam.transform.position)
                transform.position = cam.transform.position;
        }
    }
}
