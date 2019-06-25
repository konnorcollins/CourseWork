// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// Enable and disable element and toun on/off animation
    /// </summary>
    [RequireComponent(typeof(UniqueId))]
    public class OnOffGameObjectController : MonoBehaviour, IData
    {

        [SerializeField] private GameObject _onOffGameObject;
        [SerializeField] private Animation _animation;

        [SerializeField] private bool _isOneTimeUsed;
        [SerializeField] private bool _isOnWhenClick;

        [Space(10)]
        [SerializeField]
        private AudioClip _clickClip;

        private string _id;

        private SoundController _soundController;
        private SaveDataInLevel _sdLevel;
        private StateEnum _state = StateEnum.Close;

        private Action<string, int> _action;

        void Start()
        {
            _id = SceneManager.GetActiveScene().buildIndex + GetComponent<UniqueId>().uniqueId;
            _sdLevel = new SaveDataInLevel();
            if (_clickClip != null)
                _soundController = gameObject.AddComponent<SoundController>();

            _state = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());
            if (_state == StateEnum.Open) ChangeState();
        }

        void OnEnable()
        {
            EventAction.Save += EventAction_Save;
        }

        private void EventAction_Save()
        {
            _sdLevel.AddData(_id, _state.ToString());
        }

        void OnDisable()
        {
            EventAction.Save -= EventAction_Save;
        }

        private void ChangeState()
        {
            if (_isOneTimeUsed)
                GetComponent<Collider>().enabled = false;

            if (_animation != null)
            {
                _animation.Play();
            }

            if (_onOffGameObject != null) _onOffGameObject.SetActive(!_onOffGameObject.activeSelf);

            if (_isOnWhenClick)
                if (_onOffGameObject != null) _onOffGameObject.SetActive(true);
            _state = _state == StateEnum.Open ? StateEnum.Close : StateEnum.Open;
            
            if (_action != null) _action(_id, (int)_state);
        }

        public string GetId()
        {
            return _id;
        }

        public void OnClick()
        {
            if (_soundController != null)
                _soundController.PlaySound(_clickClip);
            ChangeState();
        }

        public void UnLock()
        {
            OnClick();
        }

        public void ActionElement(Action<string, int> action)
        {
            _action = action;
        }
    }
}
