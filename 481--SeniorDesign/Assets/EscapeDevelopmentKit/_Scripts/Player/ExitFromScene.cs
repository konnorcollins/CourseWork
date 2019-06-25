// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// On click exit from current scene to next scene
    /// </summary>
    public class ExitFromScene : MonoBehaviour, IData
    {

        private string _id;
        [SerializeField] private string _nextScene;
        [SerializeField] private bool _isEraseData;
        [SerializeField] private bool _isEndOfLevel;
        [SerializeField] private Transform _escapePlayer;

        void Start()
        {
            _id = SceneManager.GetActiveScene().name;
            _escapePlayer = GameObject.Find("EscapePlayer").transform;
            if(_escapePlayer==null) Debug.LogError("EscapeModules: Escape Player cant be null " + gameObject.name);
        }

        public void OnClick()
        {
            SaveDataInLevel sdl = new SaveDataInLevel();
            SavePlayerPosition spPosition = new SavePlayerPosition(SceneManager.GetActiveScene().buildIndex);
 
            if (_isEraseData)
            {
                sdl.Delete();
                spPosition.Delete();
            }
            else
            {
                EventAction.OnSave();
                sdl.Save();
                spPosition.Save();
            }

            if (_isEndOfLevel)
            {
                Data.EndCurrentLevel();
                InventoryControlSystem.DestroyAll();
            }

            SceneManager.LoadScene(_nextScene);
        }

        public string GetId()
        {
            return _id;
        }

        public void UnLock()
        {
            StartCoroutine(MovePlayer());
        }

        public void ActionElement(Action<string, int> action) { }

        IEnumerator MovePlayer()
        {
            float timer = 0f;
            Vector3 startPosition = _escapePlayer.position;
            while (timer < 1f)
            {
                timer += Time.deltaTime;
                _escapePlayer.position = Vector3.Lerp(startPosition, transform.position, timer);
                yield return null;
            }
            OnClick();
        }
    }
}
