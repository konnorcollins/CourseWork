// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// This script control item which will set to Inventory
    /// </summary>
    [RequireComponent(typeof(UniqueId))]
    public class ItemController : MonoBehaviour, IData
    {
        [Space(10)]
        [SerializeField]
        private string _targetObjectId;
        [SerializeField] private Sprite _inventoryItemImage;
        [SerializeField] private InventoryEnum _inventory = InventoryEnum.Default;

        [Space(10)]
        [SerializeField]
        private bool _isShowObject;
        [SerializeField] private float _durationShowTime = 2f;
        [SerializeField] private float _timeMovingToPlayer = 1f;
        [SerializeField] private bool _canRotate;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private bool _canShowFromInventory;

        [Space(10)]
        [SerializeField] private bool _isRotateToCamera;
        [SerializeField] private Vector3 _rotateAngle;

        [Space(10)]
        [SerializeField]
        private AudioClip _clickClip;
        [SerializeField] private AudioClip _moveClip;
        [SerializeField] private AudioClip _rotateClip;


        private string _id;

        private SoundController _soundController;
        private ItemProperty _itemProperty;
        private SaveDataInLevel _sdLevel;
        private StateEnum _state = StateEnum.Close;

        private Transform _transform;
        private Quaternion _startRot;

        private bool _isActiveItem;
        private bool _isFromInventory;

        private float _offsetLength;
        private const int FullCircle = 360;

        public static event UnityAction<InventoryEnum, ItemProperty> AddItemToInventory;
        private bool _showed;


        void Awake()
        {
            if (_inventoryItemImage == null) Debug.LogError("EscapeModules: Item Image is null " + gameObject.name);
            _sdLevel = new SaveDataInLevel();

            _id = SceneManager.GetActiveScene().buildIndex + GetComponent<UniqueId>().uniqueId;

            _state = (StateEnum)Enum.Parse(typeof(StateEnum), _sdLevel.GetState(_id) ?? _state.ToString());
            if (_state == StateEnum.Open)
            {
                gameObject.SetActive(false);
                return;
            }
            _targetObjectId = SceneManager.GetActiveScene().buildIndex + _targetObjectId;

            if (_clickClip != null || _moveClip != null || _rotateClip != null)
                _soundController = gameObject.AddComponent<SoundController>();

            _itemProperty = new ItemProperty(_id, _inventoryItemImage, _targetObjectId, gameObject, _canShowFromInventory);
            _transform = transform;
            _startRot = _transform.rotation;
            if (_isShowObject)
            {
                if (_offset == Vector3.zero) Debug.LogError("EscapeModules: Offcet vector can't be ZERO " + gameObject.name);
                if (_durationShowTime == 0) Debug.LogWarning("<color=yellow>Will be batter when Duration Show Time will be not null " + gameObject.name + " </color>");
                if (_timeMovingToPlayer == 0) Debug.LogWarning("<color=yellow>Will be batter when Duration Time Move To Camera will be not null " + gameObject.name + " </color> ");
                _offsetLength = Vector3.Distance(Vector3.zero, _offset);
            }
        }

        void OnEnable()
        {
            EventAction.Drag += EventAction_Drag;
            EventAction.Hide += AddElement;
            EventAction.Save += EventAction_Save;
        }

        private void EventAction_Save()
        {
            _sdLevel.AddData(_id, _state.ToString());
        }

        void OnDisable()
        {
            EventAction.Drag -= EventAction_Drag;
            EventAction.Hide -= AddElement;
            EventAction.Save -= EventAction_Save;
           if(_sdLevel != null) EventAction_Save();
        }

        IEnumerator Rotate()
        {
            if (_soundController != null)
                _soundController.PlaySound(_rotateClip);
            Quaternion startRot = _transform.rotation;
            float t = 0.0f;
            while (t < _durationShowTime)
            {
                t += Time.deltaTime;
                _transform.Rotate(_offset * Time.deltaTime / 1);
                yield return null;
            }

            _transform.rotation = startRot;

            AddElement();
        }

        IEnumerator MoveToCamera()
        {
            Vector3 startPosition = _transform.position;
            float timer = 0;
            if (_soundController != null) _soundController.PlaySound(_moveClip);
            while (timer < 1f)
            {
                _transform.position = Vector3.Lerp(startPosition, PlayerController.Instance.TargePosition, timer);
                timer += Time.deltaTime / _timeMovingToPlayer;
                yield return new WaitForEndOfFrame();
            }
            _transform.position = PlayerController.Instance.TargePosition;
            StartCoroutine(Rotate());
        }

        IEnumerator RotateToAngle()
        {
            PlayerController.Instance.IsLock = true;
            Vector3 startRot = _transform.eulerAngles;
            float t = 0.0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                _transform.localEulerAngles = Vector3.Lerp(startRot, _rotateAngle, t);
                yield return null;
            }
            _transform.localEulerAngles = _rotateAngle;
            _startRot = _transform.rotation;
            PlayerController.Instance.IsLock = false;
        }

        private void AddElement()
        {
            if (!_isActiveItem) return;

            if (_isFromInventory)
            {
                _isFromInventory = false;
                return;
            }
            PlayerController.Instance.IsHidden = true;
            _transform.rotation = _startRot;
            _isActiveItem = false;
            if (!_showed)
                if (AddItemToInventory != null)
                    AddItemToInventory(_inventory, _itemProperty);
            _showed = false;
            PlayerController.Instance.CanDragCamera = true;
            gameObject.transform.position = PlayerController.Instance.TargeTransform.position;
            _state = StateEnum.Open;
            gameObject.SetActive(false);
        }

        private void EventAction_Drag(Vector3 dragDistance)
        {
            if (!_isActiveItem) return;
            if (!_canRotate) return;
            StopAllCoroutines();
            float speed = dragDistance.x * FullCircle / Screen.width;
            _transform.Rotate(_offset / _offsetLength * speed);
        }


        public string GetId()
        {
            return _id;
        }

        public void OnClick()
        {
            _isActiveItem = true;
            PlayerController.Instance.IsHidden = false;
            PlayerController.Instance.CanDragCamera = false;
            if (_soundController != null) _soundController.PlaySound(_clickClip);
            if (_isShowObject)
            {
                _isShowObject = !_isShowObject;
                if (_isRotateToCamera) StartCoroutine(RotateToAngle());
                StartCoroutine(MoveToCamera());
            }
            else
            {
                AddElement();
            }
        }

        public void ActiveItems()
        {
            _isActiveItem = true;
            _isFromInventory = true;
            _showed = true;
        }

        public void UnLock()
        {
            OnClick();
        }

        public void ActionElement(Action<string, int> action)
        {
            return;
        }
    }
}
