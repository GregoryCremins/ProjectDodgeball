using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class UIManager : MonoBehaviour
    {
        public GameObject TextBoxRoot;
        public UnityEngine.UI.Text NameText;
        public UnityEngine.UI.Text MessageText;
        public TextTyper Typer;

        public static UIManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                TextBoxRoot.SetActive(false);
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HideTextBox()
        {
            TextBoxRoot.SetActive(false);
        }

        public void SetText(string name, List<string> messages, Action finishedCallback = null)
        {
            TextBoxRoot.SetActive(true);
            NameText.text = name;
            Typer.StartTyping(messages, finishedCallback);
        }

        public void SetText(string name, string msg)
        {
            SetText(name, new List<string>() {msg});
        }
    }
}