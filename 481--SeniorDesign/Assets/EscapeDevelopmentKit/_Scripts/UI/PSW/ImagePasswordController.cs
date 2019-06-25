// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Control all setted icons and check password
    /// Open if was click at element with same openItemId
    /// </summary>
    public class ImagePasswordController : MonoBehaviour
    {
        [SerializeField] private string _openItemId;
        [Tooltip("Enter number of sprite position for password. It start from 0")]
        [SerializeField] private string _pasword;
        [SerializeField] private List<IconKeyController> _iconKeyControllers;
        [SerializeField] private GameObject _panel;

        [Space(10)]
        [Tooltip("It optional button, can be null")]
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _closeButton;
        [Tooltip("It optional button, can be null")]
        [SerializeField] private float _pauseWhenOpen = 2;

        private string _id;
        private int _numberOfIcons;
        private SaveDataInLevel _sdLevel;
        private StateEnum _state = StateEnum.Close;


        void Start()
        {
            if (string.IsNullOrEmpty(_pasword)) Debug.LogWarning("EscapeModules: Password is empty " + gameObject.name);
            if (_iconKeyControllers.Count == 0) Debug.LogError("EscapeModules: Icon Key Controllers can't be null " + gameObject.name);
            if (_panel == null) Debug.LogError("EscapeModules: Panel can't be null " + gameObject.name);

            _id = GetInstanceID().ToString();
            _id = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + _id;
            _openItemId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + _openItemId;
            _sdLevel = new SaveDataInLevel();
            _state = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());
            if (_state == StateEnum.Open) _state = StateEnum.Open;
        }

        void Awake()
        {
            _numberOfIcons = _iconKeyControllers.Count;
            if (_numberOfIcons != _pasword.Length) Debug.LogError("IconKeyControllers size and password length must be same");
            if (_okButton == null)
                foreach (var icon in _iconKeyControllers)
                {
                    icon.AddListener(ClickOkButton);
                }
        }

        void OnEnable()
        {
            if (_okButton != null)
                _okButton.onClick.AddListener(ClickOkButton);
            if (_closeButton != null)
                _closeButton.onClick.AddListener(ClosePanel);
            EventAction.ShowPswPanel += EventAction_ShowPswPanel;
        }

        private void EventAction_ShowPswPanel(string arg0)
        {
            if (arg0 != _openItemId) return;
            _panel.SetActive(true);
            PlayerController.Instance.IsLock = true;
        }

        void OnDisable()
        {
            if (_okButton != null)
                _okButton.onClick.RemoveAllListeners();
            if (_closeButton != null)
                _closeButton.onClick.RemoveAllListeners();
            EventAction.ShowPswPanel -= EventAction_ShowPswPanel;
        }

        private void ClickOkButton()
        {
            string psw = "";

            foreach (var icon in _iconKeyControllers)
            {
                psw += icon.GetNumbOfsprite().ToString();
            }

            if (psw != _pasword) return;

            PlayerController.Instance.Pause(OpenItem, _pauseWhenOpen);
            _state = StateEnum.Open;
        }
        
        private void OpenItem()
        {
            EventAction.SetEvent(_openItemId, ClosePanel);
        }

        public void ClosePanel()
        {
            PlayerController.Instance.IsLock = false;
            _panel.SetActive(false);
        }
    }
}
