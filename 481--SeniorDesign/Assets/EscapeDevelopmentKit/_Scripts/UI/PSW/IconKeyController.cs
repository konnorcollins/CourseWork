// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Change Icon and sent current icon number
    /// </summary>
    public class IconKeyController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private List<Sprite> _sprites;

        private Image _currentImage;
        private int _counter;
        private Action _callBackAction;

        void Awake()
        {
            if (_sprites.Count == 0) Debug.LogError("EscapeModules: Sprites can't be null " + gameObject.name);

            _currentImage = GetComponent<Image>();
            _counter = UnityEngine.Random.Range(0, _sprites.Count);
            _currentImage.sprite = _sprites[_counter];
        }

        public void AddListener(Action action)
        {
            _callBackAction = action;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _counter++;
            if (_counter >= _sprites.Count) _counter = 0;
            _currentImage.sprite = _sprites[_counter];
            if (_callBackAction != null) _callBackAction();
        }

        public Sprite GetSprite()
        {
            return _currentImage.sprite;
        }

        public int GetNumbOfsprite()
        {
            return _counter;
        }

    }
}
