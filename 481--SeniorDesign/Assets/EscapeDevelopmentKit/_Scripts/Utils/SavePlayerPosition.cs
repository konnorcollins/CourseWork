// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// when player exit from scene to other, it save his position
    /// </summary>
    public class SavePlayerPosition
    {
        [System.Serializable]
        private class LocalizationData
        {
            public List<LocalizationItem> items;
        }

        [System.Serializable]
        private class LocalizationItem
        {
            public LocalizationItem(int k, string v)
            {
                key = k;
                value = v;
            }

            public int key;
            public string value;
        }

        private int _buildIndex;
        private int _currentLevel;
        private string _player;
        private static Dictionary<int, string> _data;

        public SavePlayerPosition(int buildIndexScena)
        {
            _player = "Player";
            _buildIndex = buildIndexScena;
            _currentLevel = Data.CurrentLevel;

            if (_data != null) return;
            _data = new Dictionary<int, string>();
            if (PlayerPrefs.HasKey(_currentLevel + _player))
            {
                Deserialized(PlayerPrefs.GetString(_currentLevel + _player));
            }
        }

        private void Deserialized(string value)
        {
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(value);
            for (int i = 0; i < loadedData.items.Count; i++)
            {
                _data.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }

        private string Serialized()
        {
            LocalizationData loadedData = new LocalizationData();
            loadedData.items = new List<LocalizationItem>();
            foreach (var data in _data)
            {
                loadedData.items.Add(new LocalizationItem(data.Key, data.Value));
            }
            return JsonUtility.ToJson(loadedData);
        }

        public void Add(Vector3 positionVector3)
        {
            if (_data == null) return;
            if (_data.ContainsKey(_buildIndex))
            {
                _data[_buildIndex] = positionVector3.x + ";" + positionVector3.y + ";" + positionVector3.z;
            }
            else
            {
                _data.Add(_buildIndex, positionVector3.x + ";" + positionVector3.y + ";" + positionVector3.z);
            }
        }

        public Vector3 GetPosition()
        {
            if (!_data.ContainsKey(_buildIndex)) return Vector3.zero;
            string[] vec = _data[_buildIndex].Split(';');
            return vec.Length != 3
                ? Vector3.zero
                : new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));
        }

        public void Save()
        {
            PlayerPrefs.SetString(_currentLevel + _player, Serialized());
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_currentLevel + _player);
            _data = null;
        }
    }
}