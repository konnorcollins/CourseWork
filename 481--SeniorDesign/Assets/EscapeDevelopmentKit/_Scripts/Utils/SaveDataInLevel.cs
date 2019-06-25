// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using UnityEngine;

namespace EscapeModules
{
    /// <summary>
    /// Save all data in scene.
    /// Each element what is in scene.
    /// </summary>
    public class SaveDataInLevel
    {
        public static Dictionary<string, string> data;
        private readonly int _currentLevel;

        public SaveDataInLevel()
        {
            _currentLevel = Data.CurrentLevel;
            if (data != null) return;
            data = new Dictionary<string, string>();
            if (PlayerPrefs.HasKey(_currentLevel.ToString()))
            {
                Deserialized(PlayerPrefs.GetString(_currentLevel.ToString()));
            }
        }

        public void AddData(string id, string state)
        {
            if (data == null) return;
            if (data.ContainsKey(id))
            {
                data[id] = state;
            }
            else
            {
                data.Add(id, state);
            }
        }

        public string GetState(string id)
        {
            string val = "";
            return data.TryGetValue(id, out val) ? val : null;
        }

        public void Save()
        {
            PlayerPrefs.SetString(_currentLevel.ToString(), Serialized());
        }

        public void Delete()
        {
            PlayerPrefs.DeleteKey(_currentLevel.ToString());
            data = null;
        }

        private void Deserialized(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            var ss = value.Split('\t');
            foreach (var ds in ss)
            {
                string[] str = ds.Split(':');
                if (str.Length == 2)
                    data.Add(str[0], str[1]);
            }
        }

        private string Serialized()
        {
            string s = "";
            int count = data.Count;
            foreach (var da in data)
            {
                --count;
                s += da.Key + ":" + da.Value;
                if (count != 0)
                    s += "\t";
            }
            return s;
        }

        public string GetString()
        {
            return Serialized();
        }
    }
}