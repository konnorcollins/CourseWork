// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// On off two objectsby click or by item
    /// </summary>
    public class ChangeObjectController : MonoBehaviour, IData
    {
        [SerializeField] private string _id;
        [SerializeField] private GameObject _ItemFirst;
        [SerializeField] private GameObject _ItemSecond;
        [SerializeField] private ChangeableEnum _changeBy;
        [SerializeField] private bool _isManyChanges;

        [Space(10)]
        [SerializeField]
        private AudioClip _changeClip;

        private bool _isActive;
        private bool _isClick;

        private SoundController _soundController;
        private StateEnum _state = StateEnum.Close;
        private SaveDataInLevel _sdLevel;

        private Action<string, int> _action;

        void Start()
        {
            _id = SceneManager.GetActiveScene().buildIndex + _id;
            _sdLevel = new SaveDataInLevel();
            _state = (StateEnum)System.Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());
            _isActive = false;
            if (_state == StateEnum.Open)
                Change();
            if (!Data.IsOffSound)
                if (_changeClip != null)
                    _soundController = gameObject.AddComponent<SoundController>();
            _isClick = _changeBy == ChangeableEnum.Click;
            if (_ItemSecond != null)
                _ItemSecond.SetActive(false);
        }

        void OnEnable()
        {
            EventAction.ActionOpen += EventActionOnActionOpen;
            EventAction.Save += EventAction_Save;
        }

        private void EventActionOnActionOpen(string arg0, Action action)
        {
            if (arg0 != _id) return;
            Change();
        }

        private void EventAction_Save()
        {
            _sdLevel.AddData(_id, _state.ToString());
        }

        void OnDisable()
        {
            EventAction.ActionOpen -= EventActionOnActionOpen;
            EventAction.Save -= EventAction_Save;
        }

        private void Change()
        {
            if (_soundController != null)
                _soundController.PlaySound(_changeClip);

            _state = StateEnum.Open;

            if (_ItemFirst != null)
                _ItemFirst.SetActive(_isActive);

            if (_ItemSecond != null)
                _ItemSecond.SetActive(!_isActive);

            if (_isManyChanges)
                _isActive = !_isActive;

            if (_action != null)
            {
                _action(_id, (int)_state);
            }

        }

        public void OnClick()
        {
            if (!_isClick) return;
            Change();
        }

        public string GetId()
        {
            return _id;
        }

        public void UnLock()
        {
            Change();
        }

        public void ActionElement(Action<string, int> action)
        {
            _action = action;
        }
    }
}
