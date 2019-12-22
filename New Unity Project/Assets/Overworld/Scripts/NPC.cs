using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class NPC : MonoBehaviour
    {
        [TextArea] public string Name = "NA";

        [TextArea] public List<string> TextPreEvent = new List<string>();

        [TextArea] public List<string> TextPostEvent = new List<string>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual bool Activate(Action finishedCallback = null)
        {
            m_FinishedActivateCallback = finishedCallback;
            UIManager.instance.SetText(Name, TextPreEvent, OnActivateFinished);
            return true;
        }

        protected virtual void OnActivateFinished()
        {
            if (TextPostEvent.Count > 0)
            {
                UIManager.instance.SetText(Name, TextPostEvent, m_FinishedActivateCallback);
            }
            else
            {
                m_FinishedActivateCallback?.Invoke();
            }
        }

        private Action m_FinishedActivateCallback = null;
    }
}