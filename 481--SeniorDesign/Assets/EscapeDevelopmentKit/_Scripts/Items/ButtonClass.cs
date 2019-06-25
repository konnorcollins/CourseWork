// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// It use for Inventory system
    /// </summary>
    [Serializable]
    public class ButtonClass
    {
        [SerializeField]
        private RectTransform _buttonRect;
        [SerializeField]
        private string _itemId;
        [SerializeField]
        private string _openId;
        [SerializeField]
        private Sprite _itemImage;
        private GameObject _gameObject;
        private bool _isCanShowFromInventoru;

        public RectTransform ButtonRect { get { return _buttonRect; } }
        public string ItemId { get { return _itemId; } }
        public Sprite ItemImage
        {
            get { return _itemImage; }
            set { _itemImage = value; }
        }
        public GameObject ItemGameObject { get { return _gameObject; } }
        public bool IsCanShowFromInventoru { get { return _isCanShowFromInventoru; } }

        public ButtonClass(RectTransform button, string itemId)
        {
            _buttonRect = button;
            _itemId = itemId;
            _itemImage = null;
            _openId = null;
        }

        public ButtonClass(RectTransform button, ItemProperty itemProperty)
        {
            _buttonRect = button;
            _itemId = itemProperty.ID;
            _itemImage = itemProperty.Image;
            _openId = itemProperty.ObjectId;
            _gameObject = itemProperty.GetGameObject;
            _isCanShowFromInventoru = itemProperty.IsCanShowFromInventoru;
        }

        public ButtonClass(ButtonClass bc)
        {
            _buttonRect = bc._buttonRect;
            _itemId = bc._itemId;
            _itemImage = bc._itemImage;
            _openId = bc._openId;
            _gameObject = bc.ItemGameObject;
            _isCanShowFromInventoru = bc.IsCanShowFromInventoru;
        }

        public bool IsCanOpen(string openId)
        {
            if (_openId == null) return false;
            if (_openId == openId) return true;
            return false;
        }
    }
}
