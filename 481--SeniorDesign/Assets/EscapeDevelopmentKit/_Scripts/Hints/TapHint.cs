using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Show hint by tap on object. 
    /// 
    /// </summary>
    public class TapHint : MonoBehaviour, IData
    {
        [SerializeField] private bool _isMultiMessages;
        [SerializeField] private bool _playOnStart;
        [TextArea] [SerializeField] private List<string> _text;
        [Tooltip("If -1 then you have unlimit show cycles")]
        [SerializeField]
        private int _numberOfShowCycles = -1;

        private bool _isUnlock;
        private int _messageIndex = 0;

        private void Start()
        {
            if (_playOnStart)
                OnClick();
        }

        public void ActionElement(Action<string, int> action)
        {
            return;//don't used
        }

        public string GetId()
        {
            return "TapHint";
        }

        public void OnClick()
        {
            if (_isUnlock) return;
            if (_numberOfShowCycles == 0) return;
            if (!_isMultiMessages)
                Dialog.Instance.ShowWaitingMessage(_text[_messageIndex]);
            else
                Dialog.Instance.ShowInformationMessage(_text);

            ++_messageIndex;
            if (_messageIndex >= _text.Count)
            {
                _messageIndex = 0;
                --_numberOfShowCycles;
            }
        }

        public void UnLock()
        {
            _isUnlock = true;
        }
    }
}