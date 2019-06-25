// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// When put item in place it call other element what should be used.
    /// </summary>
    public class PuttingPlaceController : MonoBehaviour, IData
    {
        [SerializeField] private string _puttingPlaceId;
        private StateEnum state = StateEnum.Close;
        private SaveDataInLevel sdl;
        private Action<string, int> _action;

        void Start()
        {
            if (string.IsNullOrEmpty(_puttingPlaceId))
                Debug.LogWarning("EscapeModules: Putting Place Id is empty " + gameObject.name);
            sdl = new SaveDataInLevel();
            _puttingPlaceId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + _puttingPlaceId;
            state = (StateEnum) Enum.Parse(typeof(StateEnum), sdl.GetState(_puttingPlaceId) ?? state.ToString());
            if (state == StateEnum.Open) ChangeState();
        }

        public void OnClick() { }

        public string GetId()
        {
            return _puttingPlaceId;
        }

        private void ChangeState()
        {
            Collider collider = GetComponent<Collider>() ?? gameObject.GetComponentInChildren<Collider>();
            if (collider != null) collider.enabled = false;
            state = StateEnum.Open;
            if (_action != null) _action(_puttingPlaceId, (int)state);
        }

        public void UnLock()
        {
            EventAction.SetEvent(_puttingPlaceId, null);
            ChangeState();
        }

        public void ActionElement(Action<string,int> action)
        {
            _action = action;
        }
    }
}


