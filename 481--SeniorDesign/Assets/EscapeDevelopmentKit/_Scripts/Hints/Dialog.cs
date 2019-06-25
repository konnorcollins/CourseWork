// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Show hints with button. It can be called by TapHint component
    /// </summary>
    public class Dialog : Singleton<Dialog>
    {
        [SerializeField] private Text _text;

        [SerializeField] private GameObject _messageObject;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _minTimeWaintingMessageDisplayed = 1f;

        private float _waitingMessageTime;
        private bool _waitingMessageActive;
        private bool _closeWaitingMessage;

        private int _messageIndex = 0;

        private List<string> _messages;

        void Awake()
        {
            _instance = this;
            _continueButton.gameObject.SetActive(false);
            SetActive(false);
        }

        void OnEnable()
        {
            if (_continueButton != null)
                _continueButton.onClick.AddListener(NextMessage);
        }

        void OnDisable()
        {
            if (_continueButton != null)
                _continueButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (_waitingMessageActive)
            {
                _waitingMessageTime += Time.deltaTime;
                if (_closeWaitingMessage && _waitingMessageTime > _minTimeWaintingMessageDisplayed)
                {
                    _waitingMessageActive = false;
                    SetActive(false);
                }
            }
        }

        public void ShowWaitingMessage(string message)
        {
            _text.text = message;
            SetActive(true);
            _waitingMessageTime = 0;
            _waitingMessageActive = true;
            _closeWaitingMessage = true;
        }

        public void ShowInformationMessage(List<string> message)
        {
            _messages = new List<string>(message);
            _text.text = _messages[_messageIndex];
            _continueButton.gameObject.SetActive(true);
            _waitingMessageActive = false;
            _closeWaitingMessage = false;
            PlayerController.Instance.IsLock = true;
            SetActive(true);
        }

        private void ShowInformationMessage()
        {
            SetActive(false);
            _text.text = _messages[_messageIndex];
            _continueButton.gameObject.SetActive(true);
            SetActive(true);
        }


        public void SetActive(bool visible)
        {
            _messageObject.SetActive(visible);
        }

        private void NextMessage()
        {
            ++_messageIndex;
            if (_messageIndex >= _messages.Count)
            {
                SetActive(false);
                _messageIndex = 0;
                _continueButton.gameObject.SetActive(false);
                PlayerController.Instance.IsLock = false;
                return;
            }
            ShowInformationMessage();
        }
    }
}