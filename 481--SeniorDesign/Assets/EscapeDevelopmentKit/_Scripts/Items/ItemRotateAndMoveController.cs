// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// this script can move object and return for initial state.
    /// </summary>
    [RequireComponent(typeof(UniqueId))]
    public class ItemRotateAndMoveController : MoveRotateBase, IData
    {

        [SerializeField] private ActionRotateOrMoveEnum Mode = ActionRotateOrMoveEnum.Rotate;
        [SerializeField] private Vector3 Offset;
        [SerializeField] private float Time = 1f;
        [SerializeField] private Transform ItemTransform;

        [Space(10)]
        [Tooltip("When rotate it is angle, when move it is distance")]
        [SerializeField] private StateEnum _itemState = StateEnum.Close;
        [SerializeField] private bool _isReturnToOriginalPosition;
        [SerializeField] private bool _isOneUse;
        [Range(1,10)]
        [SerializeField] private int _stateNumber = 1;

        [Space(10)]
        [SerializeField] private AudioClip _clickClip;
        [SerializeField] private AudioClip _moveClip;

        private SoundController _soundController;
        private SaveDataInLevel _sdLevel;
        private StateEnum _prevStateEnum;

        private string _id;

        private bool _isOpen;
        private bool _used;
        private bool _isActive;

        private Action<string,int> _action;

        private void Start()
        {
            _id = SceneManager.GetActiveScene().buildIndex + GetComponent<UniqueId>().uniqueId;
            
            if (ItemTransform == null) Debug.LogError("EscapeModules: Item Transform can't be null " + gameObject.name);
            _sdLevel = new SaveDataInLevel();
            _itemState = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _itemState.ToString());
            _isOpen = _itemState == StateEnum.Open;
            _prevStateEnum = _itemState;

            if (_clickClip != null || _moveClip != null)
                _soundController = gameObject.AddComponent<SoundController>();
            InitData(ItemTransform, Mode, Offset, Time, _isOpen, _stateNumber);
        }

        void OnEnable()
        {
            EventAction.Save += EventAction_Save;
        }

        private void EventAction_Save()
        {
            _sdLevel.AddData(_id, _itemState.ToString());
        }

        void OnDisable()
        {
            EventAction.Save -= EventAction_Save;
        }

        private void ChangeState()
        {
            switch (_prevStateEnum)
            {
                case StateEnum.Open:
                    _itemState = StateEnum.Close;
                    break;
                case StateEnum.Close:
                    _itemState = StateEnum.Open;
                    break;
            }
            if (_action != null) _action(_id, CurrentState);
            _isActive = false;
        }

        private void Open()
        {
            if (_soundController != null)
                _soundController.PlaySound(_moveClip);
            _prevStateEnum = _itemState;
            _itemState = StateEnum.InProcess;
            if(CallAction!= null) CallAction(ChangeState); else Debug.LogWarning("NULLL");
        }

        private void HandleOpen()
        {
            _prevStateEnum = _itemState;
            switch (_itemState)
            {
                case StateEnum.Locked:
                case StateEnum.Close:
                    CallAction(CloseHangle);
                    break;
                case StateEnum.Open:
                    CallAction(OpenHandle);
                    break;
            }
            _itemState = StateEnum.InProcess;
        }

        private void CloseHangle()
        {
            CallAction(ChangeState);
        }

        private void OpenHandle()
        {
            CallAction(ChangeState);
        }

        public void OnClick()
        {
            if (_soundController != null)
                _soundController.PlaySound(_clickClip);
            if (_isActive) return;
            _isActive = true;
            if (_itemState == StateEnum.Locked) return;
            if (!_used && !_isReturnToOriginalPosition) Open();
            if (_isReturnToOriginalPosition) HandleOpen();
            if (!_isOneUse) return;
            _isOneUse = false;
            _used = true;
        }

        public string GetId()
        {
            return _id;
        }

        public void UnLock()
        {
            _itemState = StateEnum.Close;
        }

        public void ActionElement(Action<string,int> action)
        {
            _action = action;
        }
    }
}
