// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    public class ButtonClick : MonoBehaviour
    {
        protected Button _button;

        protected void Awake()
        {
            _button = GetComponent<Button>();
        }

        protected void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        protected virtual void Click()
        {
            print("Base Click");
        }
    }
}

