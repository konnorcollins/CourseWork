// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// When we add item to inventory from ItemControoler component set here options what will have UI button
    /// </summary>
    public class ItemProperty
    {
        private readonly string _id;
        private readonly Sprite _image;
        private readonly string _idObject;
        private GameObject _gameObject;
        private bool _isCanShowFromInventoru;

        public string ID { get { return _id; } }
        public Sprite Image { get { return _image; } }
        public string ObjectId { get { return _idObject; } }
        public GameObject GetGameObject { get { return _gameObject; } }
        public bool IsCanShowFromInventoru { get { return _isCanShowFromInventoru; } }

        public ItemProperty(string id, Sprite image, string idObject, GameObject gameObject, bool isCanShowFromInventoru)
        {
            _id = id;
            _image = image;
            _idObject = idObject;
            _gameObject = gameObject;
            _isCanShowFromInventoru = isCanShowFromInventoru;
        }

        public ItemProperty()
        {
            _id = null;
            _image = null;
            _idObject = null;
            _gameObject = null;
            _isCanShowFromInventoru = false;
        }
    }
}
