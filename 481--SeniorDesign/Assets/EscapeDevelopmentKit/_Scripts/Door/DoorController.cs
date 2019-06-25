// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EscapeModules
{
    /// <summary>
    /// It make control for door. Open/CLose door and can locked door. Play all sound for door.
    /// </summary>
    public class DoorController : MoveRotateBase, IData
    {
        [SerializeField] private string Id;
        [SerializeField] private ActionRotateOrMoveEnum Mode = ActionRotateOrMoveEnum.Rotate;
        [SerializeField] private Vector3 Offset;
        [SerializeField] private float Time = 1f;
        [SerializeField] private Transform _doorTransform;

        [Space(10)]
        [SerializeField] private StateEnum _doorState = StateEnum.Close;
        [Tooltip("If you want automatically open after action ")]
        [SerializeField] private bool _isAutoOpening;
        [SerializeField] private GameObject _onVisibleObject;
        [SerializeField] private GameObject _offVisibleObject;

        [Space(10)]
        [SerializeField] private AudioClip _openDoorClip;
        [SerializeField] private AudioClip _closeDoorClip;
        [SerializeField] private AudioClip _LookedDoorClip;
        [SerializeField] private AudioClip _UnlockDoorClip;

        private static event UnityAction<string> OpenSeconDoor;
        private static event UnityAction<string> UnlockSeconDoor;

        private StateEnum _prevStateEnum;
        public StateEnum StateValue { get { return _prevStateEnum; } }

        private SoundController _soundController;
        private SaveDataInLevel _sdil;

        private bool _canOpen;
        private bool State;
        private bool _isActionLock;

        private bool _isSettedId;
        public bool IsSettedId { get { return _isSettedId; } }

        private Action<string, int> _action;

        void Start()
        {
            if (string.IsNullOrEmpty(Id)) Debug.LogWarning("EscapeModules: Id is empty " + gameObject.name);
            Id = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + Id;
            _sdil = new SaveDataInLevel();
            _prevStateEnum = _doorState;
            _doorState = (StateEnum)Enum.Parse(typeof(StateEnum), _sdil.GetState(Id) ?? _doorState.ToString());
            _isSettedId = true;
            State = _doorState == StateEnum.Open;
            if (_prevStateEnum != _doorState) UnLockItem();

            if (!Data.IsOffSound)
                if (_openDoorClip != null || _closeDoorClip != null || _LookedDoorClip != null || _UnlockDoorClip != null)
                    _soundController = gameObject.AddComponent<SoundController>();
            if (_doorTransform == null) Debug.LogError("EscapeModules: Door Transform is null!!! " + gameObject.name);

            InitData(_doorTransform, Mode, Offset, Time, State, (int)StateEnum.Open);

        }

        void OnEnable()
        {
            EventAction.Action += EventAction_Action;
            EventAction.ActionOpen += ActionOpen;
            EventAction.Save += EventAction_Save;
            OpenSeconDoor += DoorController_openSeconDoor;
            UnlockSeconDoor += DoorController_UnlockSeconDoor;
        }

        private void EventAction_Action(string arg0, string arg1, string arg2)
        {
            if (arg0 == Id && arg1 == "open")
                Open();
        }

        private void DoorController_openSeconDoor(string arg0)
        {
            if (arg0 != Id) return;
            Open();
        }

        private void DoorController_UnlockSeconDoor(string arg0)
        {
            if (arg0 != Id) return;
            UnLockItem();
            if (_isAutoOpening)
                Open();
            
            //If on door is TapHint Component
            IData[] datas = GetComponents<IData>();
            if (datas.Length == 0) return;
            foreach (var data in datas)
            {
                if (data.GetId() == "TapHint") data.UnLock();
            }
        }

        private void EventAction_Save()
        {
            _sdil.AddData(Id, _doorState.ToString());
        }

        void OnDisable()
        {
            EventAction.Action += EventAction_Action;
            EventAction.ActionOpen -= ActionOpen;
            EventAction.Save -= EventAction_Save;
            OpenSeconDoor -= DoorController_openSeconDoor;
            if (_sdil != null) EventAction_Save();
        }


        private void ChangeState()
        {
            switch (_prevStateEnum)
            {
                case StateEnum.Open:
                    _doorState = StateEnum.Close;
                    break;
                case StateEnum.Close:
                    _doorState = StateEnum.Open;
                    break;
            }
            if (_action != null) _action(Id, (int)_doorState);
            _isActionLock = false;
        }

        public string GetId()
        {
            return Id;
        }

        private void ActionOpen(string key, Action action)
        {
            if (key != Id) return;
            if (_isActionLock) return;
            if (_doorState == StateEnum.Locked)
            {
                if (action != null) action();
                UnLockItem();
                if (_isAutoOpening)
                    Open();
            }
            else
            {
                Open();
            }
        }

        private void Open()
        {
            if (_doorState == StateEnum.Open) if (_soundController != null)
                    _soundController.PlaySound(_closeDoorClip);

            if (_doorState == StateEnum.Close) if (_soundController != null)
                    _soundController.PlaySound(_openDoorClip);

            _prevStateEnum = _doorState;
            _doorState = StateEnum.InProcess;

            CallAction(ChangeState);
        }

        private void UnLockItem()
        {
            if (_soundController != null) _soundController.PlaySound(_UnlockDoorClip);
            if (_onVisibleObject != null) _onVisibleObject.SetActive(true);
            if (_offVisibleObject != null) _offVisibleObject.SetActive(false);

            _doorState = StateEnum.Close;
        }

        public void OnClick()
        {
            if (_isActionLock) return;
            _isActionLock = true;
            if (_doorState == StateEnum.Locked)
            {
                if (_soundController != null)
                    _soundController.PlaySound(_LookedDoorClip);
                EventAction.ShowPasswordPanel(Id);
                _isActionLock = false;
            }
            else
            {
                if (OpenSeconDoor != null) OpenSeconDoor(Id);
            }
        }

        public void UnLock()
        {
            if (UnlockSeconDoor != null) UnlockSeconDoor(Id);
        }

        public void ActionElement(Action<string , int> action)
        {
            _action = action;
        }
    }
}