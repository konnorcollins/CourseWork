// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EscapeModules
{
    /// <summary>
    /// This component return last ended scene ID
    /// </summary>
    public class LevelsController : MonoBehaviour
    {
        private int baseTH = 6;
        private int basePPI = 210;
        private int dragTH = 0;
        [SerializeField]
        private Dictionary<string, int> _scenasDictionary = new Dictionary<string, int>();

        void Awake()
        {
            _scenasDictionary = new Dictionary<string, int>();
            Data.Init();
        }

        void Start()
        {
            dragTH = baseTH * (int)Screen.dpi / basePPI;
            EventSystem es = GetComponent<EventSystem>();
            if (es) es.pixelDragThreshold = dragTH;
        }

        void OnEnable()
        {
    
            SceneProperty.AddSceneInfo += AddScene;
            SceneProperty.GetLastSceneId += action => action(GetLastScenaId());
        }

        void OnDisable()
        {
            SceneProperty.AddSceneInfo -= AddScene;
            SceneProperty.GetLastSceneId -= action => action(GetLastScenaId());
        }

        public void AddScene(string scenaName, int id)
        {
            int result;
            if (_scenasDictionary.TryGetValue(scenaName, out result))
            {
                Debug.LogError("Scena " + scenaName + " " + id + " is exist");
                return;
            }
            _scenasDictionary.Add(scenaName, id);
            var l = _scenasDictionary.OrderBy(key => key.Key);
            _scenasDictionary = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
        }

        public int GetLastScenaId()
        {
            int lastId;
            if (_scenasDictionary.TryGetValue(Data.LastEndedLevel, out lastId))
            {
                return lastId;
            }
            return -1;
        }
    }
}
