// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// It for buttons at PasswordPannel 
    /// </summary>
    public class PswKeyButtonController : MonoBehaviour
    {
        [SerializeField] private KeyButtonOptionEnum _buttonCode;
        public KeyButtonOptionEnum ButtonCode { get { return _buttonCode; } }

        [SerializeField] private string _value;
        public string Value { get { return _value; } }

        private GameObject _gameObject;

        void Start()
        {
            if (_value.Length > 1) Debug.LogError("EscapeModules: Input one symbol " + gameObject.name);
            if (string.IsNullOrEmpty(_value)) Debug.LogError("EscapeModules: Input one symbol " + gameObject.name);
            Text textUi = GetComponentInChildren<Text>();
            if (textUi != null) textUi.text = _value;
            gameObject.name = _value;
        }
    }
}
