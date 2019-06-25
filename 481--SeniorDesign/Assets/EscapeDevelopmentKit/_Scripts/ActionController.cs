// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Create multielement used action.
    /// each element in list shoulb have setted state, then next game object will unlocked
    /// </summary>
    public class ActionController : MonoBehaviour
    {
        [Serializable]
        private class ActionItemProperty
        {
            [SerializeField] private GameObject _gameObject;
            [Range(0,10)]
            [SerializeField] private int _stateNumber = 1;

            public IData GetIData
            {
                get { return _gameObject != null ? _gameObject.GetComponent<IData>() : null; }
            }

            public int NumberOfState { get { return _stateNumber; } }
        }
        [Serializable]
        private class ActionClass
        {
            public ActionClass(IData idata, int numb)
            {
                _idata = idata;
                isUsed = false;
                stateNumber = numb;
            }

            private IData _idata;
            public IData GetIdata { get { return _idata; } }

            public int stateNumber { set; get; }
            public bool isUsed { set; get; }
        }

        [SerializeField] private List<ActionItemProperty> _ConditionElementsGameObjects;
        [SerializeField] private GameObject _nextGameObjectChangeState;
        [SerializeField] private bool _isOneTime = true;

        private IData _nextGameObjectChangeStateIdata;
        private List<ActionClass> _activedElements;
        private bool _wasPlayed;

        private void Start()
        {
            if (_ConditionElementsGameObjects.Count == 0) Debug.LogWarning("EscapeModules: Actions Ids can't be null " + gameObject.name);
            if (_nextGameObjectChangeState == null) Debug.LogWarning("EscapeModules: Next Game Object Change State cant be empty " + gameObject.name);
            _wasPlayed = false;
            _nextGameObjectChangeStateIdata = _nextGameObjectChangeState.GetComponent<IData>();
            _activedElements = new List<ActionClass>();
            for (int i = 0; i < _ConditionElementsGameObjects.Count; i++)
            {
                if (_ConditionElementsGameObjects[i].GetIData != null)
                {
                    _activedElements.Add(new ActionClass(_ConditionElementsGameObjects[i].GetIData,
                        _ConditionElementsGameObjects[i].NumberOfState));
                    _activedElements[i].GetIdata.ActionElement(ChooseSelectedElement);
                }
                else
                {
                    Debug.LogError("EscapeModules: GameObject at element " + i + " is missed " + gameObject.name);
                }
            }
        }

        private void ChooseSelectedElement(string id, int stateNumber)
        {
            for (int i = 0; i < _activedElements.Count; i++)
            {
                if (_activedElements[i].GetIdata.GetId() == id)
                {
                    _activedElements[i].isUsed = _activedElements[i].stateNumber == stateNumber;
                }
            }
            CheckCondition();
        }

        private void CheckCondition()
        {
            for (int i = 0; i < _activedElements.Count; i++)
            {
                if (!_activedElements[i].isUsed)
                    return;
            }
            if(!_wasPlayed)
                _nextGameObjectChangeStateIdata.UnLock();
            if (_isOneTime)
                _wasPlayed = true;
        }
    }
}