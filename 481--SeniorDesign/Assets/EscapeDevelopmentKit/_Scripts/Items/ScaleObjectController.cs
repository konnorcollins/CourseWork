// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Scale objectby click
    /// </summary>
    [RequireComponent(typeof(UniqueId))]
    public class ScaleObjectController : MonoBehaviour, IData
    {

        [SerializeField] private Vector3 _scaleOffset;
        [SerializeField] private float _time;
        [SerializeField] private bool _isClose;

        [Space(10)]
        [SerializeField] private AudioClip _openCloseAudioClip;

        private string _id;
        private SoundController _soundController;
        private Transform _transform;
        private Vector3 _openScale;
        private Vector3 _closeScale;
        private Vector3 _endScale;

        private bool _isActive = false;
        private SaveDataInLevel _sdLevel;
        private StateEnum _state = StateEnum.Close;

        private Action<string, int> _action;

        void Start()
        {
            _id = GetComponent<UniqueId>().uniqueId;
            
            _sdLevel = new SaveDataInLevel();
            _state = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());

            _transform = transform;
            if (!Data.IsOffSound)
                if (_openCloseAudioClip != null)
                    _soundController = gameObject.AddComponent<SoundController>();

            _openScale = _transform.localScale;
            _closeScale = _transform.localScale + _scaleOffset;
            _transform.localScale = !_isClose ? _openScale : _closeScale;
            if (_state == StateEnum.Open) _transform.localScale = _isClose ? _openScale : _closeScale;
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

        IEnumerator OpenClose()
        {
            float timer = 0;
            Vector3 currentScale = _transform.localScale;
            if (_soundController != null)
                _soundController.PlaySound(_openCloseAudioClip);
            while (timer < 1f)
            {
                timer += Time.deltaTime / _time;
                _transform.localScale = Vector3.Lerp(currentScale, _endScale, timer);
                yield return null;
            }
            _isClose = !_isClose;
            _transform.localScale = _endScale;
            _state = _state == StateEnum.Open ? StateEnum.Close : StateEnum.Open;
            if (_action != null) _action(_id, (int)_state);

            _isActive = false;
        }

        public void OnClick()
        {
            if (_isActive) return;
            _isActive = true;
            _endScale = _isClose ? _openScale : _closeScale;
            StartCoroutine(OpenClose());
        }

        public string GetId()
        {
            return _id;
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