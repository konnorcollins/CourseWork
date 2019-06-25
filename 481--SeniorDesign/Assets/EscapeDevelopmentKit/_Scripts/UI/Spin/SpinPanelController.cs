// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Open when element with openItemId was clicked. It check password if true then open(unlock) item
    /// </summary>
    public class SpinPanelController : MonoBehaviour
    {
        [SerializeField] private string _openItemId;
        [SerializeField] private string _password;
        [SerializeField] private List<Spin> _spins;
        [SerializeField] private GameObject _parentGO;

        [Space(10)]
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _pauseWhenOpen = 2;

        private string _id;
        private SaveDataInLevel _sdLevel;
        private StateEnum _state = StateEnum.Close;
        

        void Start()
        {
            if (_password.Length != _spins.Count) Debug.LogError("Password and Spins length should be same");
            if (string.IsNullOrEmpty(_openItemId)) Debug.LogWarning("EscapeModules: Open Item Id is empty " + gameObject.name);
            if (string.IsNullOrEmpty(_password)) Debug.LogWarning("EscapeModules: Password is empty " + gameObject.name);
            if (_spins.Count == 0) Debug.LogError("EscapeModules: Spins can't be null " + gameObject.name);
            if (_parentGO == null) Debug.LogError("EscapeModules: Parent GO can't be null " + gameObject.name);

            _id = GetInstanceID().ToString();
            _openItemId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + _openItemId;
            _sdLevel = new SaveDataInLevel();
            _state = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());
            if (_state == StateEnum.Open) _state = StateEnum.Open;
        }

        void OnEnable()
        {
            EventAction.ShowPswPanel += EventAction_ShowPswPanel;
            if (_okButton != null)
                _okButton.onClick.AddListener(GetPassword);
            if (_closeButton != null)
                _closeButton.onClick.AddListener(ClosePanel);
            if (_okButton == null)
                foreach (var spin in _spins)
                {
                    spin.AddListener(GetPassword);
                }
        }

        void OnDisable()
        {
            EventAction.ShowPswPanel -= EventAction_ShowPswPanel;
            if (_okButton != null)
                _okButton.onClick.RemoveAllListeners();
            if (_closeButton != null)
                _closeButton.onClick.RemoveAllListeners();
        }

        void GetPassword()
        {
            string psw = String.Empty;
            foreach (var spin in _spins)
            {
                psw += spin.GetValue();
            }
            if (_password == psw)
            {
                _state = StateEnum.Open;
                PlayerController.Instance.Pause(OpenItem, _pauseWhenOpen);
            }
        }

        private void OpenItem()
        {
            EventAction.SetEvent(_openItemId, ClosePanel);
        }

        public void ClosePanel()
        {
            PlayerController.Instance.IsLock = false;
            _parentGO.SetActive(false);
        }


        private void EventAction_ShowPswPanel(string itemId)
        {
            if (itemId != _openItemId) return;
            _parentGO.SetActive(true);
            PlayerController.Instance.IsLock = true;
        }
    }
}
