// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// It make interactable button if scene can be active.
    /// </summary>
    public class SceneProperty : MonoBehaviour, IPointerClickHandler
    {
        public static event UnityAction<string, int> AddSceneInfo;
        public static event UnityAction<Action<int>> GetLastSceneId;

        [SerializeField] private int _id = 1;
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _isLocked;

        private Button _thisButton;
        private Image _thisImage;

        public void OnPointerClick(PointerEventData eventData)
        {
            Data.CurrentLevel = _id;
            Data.CurrentLevelName = _sceneName;
            SceneManager.LoadScene(_sceneName);
        }

        void Awake()
        {
            if (string.IsNullOrEmpty(_sceneName)) Debug.LogError("EscapeModules: Scene Name is empty " + gameObject.name);
            _thisButton = GetComponent<Button>();
            _thisImage = GetComponent<Image>();
       
        }

        void Start()
        {
            if (AddSceneInfo != null)
                AddSceneInfo(_sceneName, _id);

            if (GetLastSceneId != null)
                GetLastSceneId(SetSceneState);
        }

        void OnEnable()
        {
 
        }

        private void SetSceneState(int lastId)
        {
            if (lastId + 1 >= _id)
            {
                _isLocked = false;
            }
            _thisButton.interactable = !_isLocked;
            _thisImage.raycastTarget = !_isLocked;
        }

    }
}
