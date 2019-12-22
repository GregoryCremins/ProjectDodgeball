using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class CameraManager : MonoBehaviour
    {
        public AnimationCurve FadeInCurve =
            new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

        public AnimationCurve FadeOutCurve =
            new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

        public static CameraManager instance;
        private float _alpha = 1;
        private Texture2D _texture;
        private bool _done = true;
        private float _time;

        public bool FadeIn = false;
        private Action m_CallbackWhenDone;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                GameObject.Destroy(this);
            }
        }

        public void Start()
        {
            Fade(true);
        }

        public void Reset()
        {
            m_CallbackWhenDone = null;
            _done = false;
            _alpha = 1;
            _time = 0;
        }

        public void Fade(bool fadeIn, Action callbackWhenDone = null)
        {
            Reset();
            m_CallbackWhenDone = callbackWhenDone;
            FadeIn = fadeIn;
        }

        public void OnGUI()
        {
            if (_done) return;
            if (_texture == null) _texture = new Texture2D(1, 1);

            _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
            _texture.Apply();

            _time += Time.deltaTime;
            _alpha = FadeIn ? FadeInCurve.Evaluate(_time) : FadeOutCurve.Evaluate(_time);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

            float doneVal = FadeIn ? 0f : 1f;
            if (_alpha == doneVal)
            {
                _done = true;
                m_CallbackWhenDone?.Invoke();
            }
        }
    }
}