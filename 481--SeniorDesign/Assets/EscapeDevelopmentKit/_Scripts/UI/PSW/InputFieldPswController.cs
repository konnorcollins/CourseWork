// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// It instantiate input fields for enter password
    /// </summary>
    public class InputFieldPswController : MonoBehaviour
    {
        [SerializeField] private GameObject _inputFieldPrefab;

        private List<InputField> _inputFields;
        private int _currentItem;
        private int _totalCount;

        void Start()
        {
            if (_inputFieldPrefab == null) Debug.LogError("EscapeModules: Input Firld Prefab is null " + gameObject.name);
			_inputFieldPrefab.SetActive (false);
        }

        public void SetInputFieldPswController(int countKeys)
        {
            if (_inputFields != null)
            {
                if (_inputFields.Count != countKeys)
                    SetFields(countKeys);
            }
            else
            {
                SetFields(countKeys);
            }
        }

        private void SetFields(int countKeys)
        {
            _totalCount = countKeys;
            _inputFields = new List<InputField>();
            for (int i = 0; i < countKeys; i++)
            {
                GameObject go = Instantiate(_inputFieldPrefab);
                go.transform.SetParent(transform);
                go.SetActive(true);
                _inputFields.Add(go.GetComponent<InputField>());
            }
        }

        public void AddKey(char keyStr)
        {
            if (_currentItem >= _totalCount) return;
            _currentItem = Mathf.Clamp(_currentItem, 0, _totalCount - 1);
            _inputFields[_currentItem].text = keyStr.ToString();
            _currentItem++;
        }

        public void DeleteLastKey()
        {
            _currentItem--;
            _currentItem = Mathf.Clamp(_currentItem, 0, _totalCount);
            _inputFields[_currentItem].text = "";
        }

        public string GetPSW()
        {
            string psw = "";
            foreach (var _input in _inputFields)
            {
                psw += _input.text;
            }
            return psw;
        }

        private void OnDisable()
        {
            if (_inputFields == null) return;

            for (int i = 0; i < _inputFields.Count; ++i)
            {
                _inputFields[i].text = "";
            }
            _currentItem = 0;
        }

        private void OnDestroy()
        {
            if (_inputFields == null) return;

            for (int i = 0; i < _inputFields.Count; ++i)
            {
                Destroy(_inputFields[i].gameObject);
            }
            _inputFields.RemoveAll(item => item != null);
        }
    }
}
