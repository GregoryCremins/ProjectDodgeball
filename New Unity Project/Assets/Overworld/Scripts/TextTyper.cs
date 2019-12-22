using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Overworld
{

    public class TextTyper : MonoBehaviour
    {

        public float letterPause = 0.2f;
        public AudioClip typeSound1;
        public AudioClip typeSound2;

        private List<string> m_MessagePanes = new List<string>();
        private int m_CurrentMessageIndex = 0;
        Text textComp;
        private bool m_DoneTyping = false;
        private bool m_FastForwardTyping = false;
        private Action m_FinishedCallback = null;

        public void StartTyping(List<string> messages, Action finishedCallback = null)
        {
            m_FinishedCallback = finishedCallback;
            m_MessagePanes = messages;
            m_CurrentMessageIndex = 0;
            m_FastForwardTyping = false;
            m_DoneTyping = false;
            textComp = GetComponent<Text>();
            textComp.text = "";
            StartCoroutine(TypeText());
        }

        void Update()
        {
            if (!Input.GetButtonDown("Submit"))
            {
                return;
            }

            if (m_DoneTyping)
            {
                if (m_CurrentMessageIndex + 1 == m_MessagePanes.Count)
                {
                    UIManager.instance.HideTextBox();
                    m_FinishedCallback?.Invoke();
                }
                else
                {
                    m_CurrentMessageIndex++;
                    m_DoneTyping = false;
                    m_FastForwardTyping = false;
                    textComp.text = "";
                    StartCoroutine(TypeText());
                }
            }
            else
            {
                m_FastForwardTyping = true;
                m_DoneTyping = true;
            }
        }

        IEnumerator TypeText()
        {
            foreach (char letter in m_MessagePanes[m_CurrentMessageIndex].ToCharArray())
            {
                if (m_DoneTyping)
                {
                    break;
                }

                textComp.text += letter;
                if (typeSound1 && typeSound2)
                {
                    SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
                    yield return 0;
                }

                yield return new WaitForSeconds(letterPause);
            }

            if (m_FastForwardTyping)
            {
                textComp.text = m_MessagePanes[m_CurrentMessageIndex];
            }

            m_DoneTyping = true;
        }
    }
}