// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;


namespace EscapeModules
{
    /// <summary>
    /// Base class for DoorController and for ItemRotateAndMoveController.
    /// It make item rotate and move by Init Properties
    /// </summary>
    public class MoveRotateBase : MonoBehaviour
    {
        private ActionRotateOrMoveEnum _mode = ActionRotateOrMoveEnum.Rotate;
        private Vector3 _offset;

        protected delegate void CallItemAction(Action callBack);
        protected CallItemAction CallAction;

        private float _time;

        private Quaternion _closedRotation, _openRotation, _targetRotation;
        private Vector3 _closedPosition, _openPosition, _targetPosition;
        private Transform _transform;

        private float _timer;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private bool _state;
        private int _numberOfState;
        protected int CurrentState;

        protected void InitData(Transform transformObj, ActionRotateOrMoveEnum state, Vector3 offcet, float time, bool isOpen, int stateNumber)
        {
            _mode = state;
            _offset = offcet;
            _time = time;
            _transform = transformObj;
            _state = isOpen;
            _numberOfState = stateNumber;
            CurrentState = _state ? _numberOfState : 0;
            InitStartData();
        }

        private void InitStartData()
        {
            switch (_mode)
            {
                case ActionRotateOrMoveEnum.Move:
                    InitMoving();
                    break;
                case ActionRotateOrMoveEnum.Rotate:
                    InitRotator();
                    break;
            }
            _time = _time >= 0 ? _time : 0;
        }

        private void InitRotator()
        {
            _closedRotation = _transform.localRotation;
            _openRotation = _transform.localRotation;

            CallAction = new CallItemAction(Rotate);
            Vector3 vect = _openRotation.eulerAngles;
            vect += _offset;

            _openRotation.eulerAngles = vect;

            //CheckStatus
            _transform.localRotation = _state ? _openRotation : _closedRotation;
            _state = !_state;
        }

        private void InitMoving()
        {
            _closedPosition = _transform.localPosition;
            _openPosition = _transform.localPosition + _offset;
            CallAction = new CallItemAction(Move);

            //CheckStatus
            _transform.localPosition = _state ? _openPosition : _closedPosition;
            _state = !_state;
        }

        private void Rotate(Action callBack)
        {
            StartCoroutine(RotateCoroutine(callBack));
        }

        private void Move(Action callBack)
        {
            StartCoroutine(MoveCoroutine(callBack));
        }

        private IEnumerator MoveCoroutine(Action callBack)
        {
            _timer = 0;
            _startPosition = _transform.localPosition;
            ++CurrentState;
            if (CurrentState > _numberOfState)
            {
                CurrentState = 0;
                _state = false;
            }

            _targetPosition = _state ? (_transform.localPosition + _offset/_numberOfState) : _closedPosition;

            if (!_state)
            {
                _state = !_state;
            }

            while (_timer < 1f)
            {
                _transform.localPosition = Vector3.Lerp(_startPosition, _targetPosition, _timer);
                _timer += Time.deltaTime / _time;
                yield return null;
            }
            _transform.localPosition = _targetPosition;
            if (callBack != null) callBack();

        }


        private IEnumerator RotateCoroutine(Action callBack)
        {
            _startRotation = _transform.localRotation;

            Vector3 vect = _transform.localEulerAngles;
            vect += _offset/_numberOfState;
            _openRotation.eulerAngles = vect;
            ++CurrentState;
            if (CurrentState > _numberOfState)
            {
                CurrentState = 0;
                _state = false;
            }

            _targetRotation = _state ? _openRotation : _closedRotation;

            if (!_state)
            {
                _state = !_state;
            }

            float speed = Vector3.Distance(Vector3.zero, _offset);
            _timer = 0;
            while (_timer < 1f)
            {
                _transform.localRotation = Quaternion.RotateTowards(_startRotation, _targetRotation,
                    _timer * speed);
                _timer += Time.deltaTime / _time;
                yield return null;
            }
            _transform.localRotation = _targetRotation;

            if (callBack != null) callBack();
        }
    }
}