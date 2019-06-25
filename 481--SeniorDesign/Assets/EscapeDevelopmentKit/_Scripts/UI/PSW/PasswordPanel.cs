// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Show password panel what was generated. Read password from input fields and check if it same psw
    /// </summary>
    public class PasswordPanel : MonoBehaviour
    {
        [SerializeField] private string _openItemId;
        [SerializeField] private string _password;
        [SerializeField] private InputFieldPswController _inputFieldPswController;
        [SerializeField] private GameObject _keyBoardParent;
        [SerializeField] private GameObject _panelGameObject;
        [SerializeField] private float _pauseWhenOpen = 2f;
        [SerializeField] private Button _closeButton;

        private int _numberOfKeys;

        private Button[] _buttons;

        void Awake()
        {
            if (_panelGameObject == null) Debug.LogError("EscapeModules: PanelGameObject can't be null!!! " + gameObject.name);
            if (string.IsNullOrEmpty(_password)) Debug.LogWarning("EscapeModules: Pasword is empty " + gameObject.name);
            if (string.IsNullOrEmpty(_openItemId)) Debug.LogWarning("EscapeModules: Id Object For Open is empty " + gameObject.name);

            _numberOfKeys = _password.Length;
            _inputFieldPswController.SetInputFieldPswController(_numberOfKeys);
            _buttons = _keyBoardParent.GetComponentsInChildren<Button>();
        }

        void Start()
        {
            _openItemId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + _openItemId;
        }

        void OnEnable()
        {
            EventAction.EnteredPassword += OpenItem;
            EventAction.ShowPswPanel += EventAction_ShowPswPanel;
            foreach (var button in _buttons)
            {
                PswKeyButtonController tmp = button.GetComponent<PswKeyButtonController>();
                button.onClick.AddListener(() => ClickButton(tmp.ButtonCode, tmp.Value));
            }
            if (_closeButton != null)
                _closeButton.onClick.AddListener(ClosePanel);
        }

        void OnDisable()
        {
            EventAction.EnteredPassword -= OpenItem;
            EventAction.ShowPswPanel -= EventAction_ShowPswPanel;
            foreach (var button in _buttons)
            {
                button.onClick.RemoveAllListeners();
            }
            if (_closeButton != null)
                _closeButton.onClick.RemoveAllListeners();
        }

        private void OpenItem(string password)
        {
            if (password != _password)
                return;
            PlayerController.Instance.Pause(PauseOpen, _pauseWhenOpen);
        }

        private void PauseOpen()
        {
            EventAction.SetEvent(_openItemId, ClosePanel);
        }

        private void EventAction_ShowPswPanel(string itemId)
        {
            if (itemId != _openItemId) return;
            _panelGameObject.SetActive(true);
            PlayerController.Instance.IsLock = true;
        }

        private void ClickButton(KeyButtonOptionEnum key, string value)
        {
            char res;
            switch (key)
            {
                case KeyButtonOptionEnum.Enter:
                    OpenItem(_inputFieldPswController.GetPSW());
                    break;
                case KeyButtonOptionEnum.Delete:
                    _inputFieldPswController.DeleteLastKey();
                    break;
                default:
                    if (char.TryParse(value, out res))
                        _inputFieldPswController.AddKey(res);
                    break;
            }
        }

        public void ClosePanel()
        {
            PlayerController.Instance.IsLock = false;
            _panelGameObject.SetActive(false);
        }
    }
}
