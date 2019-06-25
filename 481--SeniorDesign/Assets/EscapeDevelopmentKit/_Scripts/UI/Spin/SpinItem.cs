// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Set Value for spin item
    /// </summary>
    public class SpinItem : MonoBehaviour
    {
        [SerializeField] private string _value = String.Empty;

        private Text _text;
        
        private void Start()
        {
            _text = GetComponentInChildren<Text>();
            if (_text != null) _text.text = _value;
        }

        public string GetValue()
        {
            return _value;
        }

    }
}
