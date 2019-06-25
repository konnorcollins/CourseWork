// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace EscapeModules
{
    /// <summary>
    /// Camera rotate, move. Control click at elements.
    /// </summary>
    public class PlayerController : Singleton<PlayerController>
    {
        [SerializeField] private float _distanceOfItemToCamera = .5f;
        private int _dragTH = 20;

        public Transform TargeTransform { get { return transform; } }
        public Vector3 TargePosition { get { return transform.position + transform.forward * _distanceOfItemToCamera; } }

        public bool IsDrag { set; get; }
        public bool IsLock { set; get; }
        public bool CanDragCamera { get; set; }
        public bool IsHidden { get; set; }

        private Vector2 _mousePosition;
        private Vector3 _delta;

        private Transform _transform;
        private Transform _pivot;
        private GameObject _tmp;
        private Vector3 _startPositionVector3;
        private Vector3 _rotateVector3;

        private bool _fingerDrag;
        private bool _isRotate;
        private bool _isMove;

        private float _timeRotate;
        private float _timeMove;
        private float _distance;

        private const float _speed = 0.1f;
        private const int _MovingCoefficient = 10;

        private SavePlayerPosition _spPosition;

        void Awake()
        {
            CanDragCamera = true;
            IsHidden = true;
            EventSystem es = GetComponent<EventSystem>();
            if (es) _dragTH = es.pixelDragThreshold;
        }

        void Start()
        {
            _transform = transform;
            _spPosition = new SavePlayerPosition(SceneManager.GetActiveScene().buildIndex);
            _transform.position = _spPosition.GetPosition() == Vector3.zero
                ? _transform.position
                : _spPosition.GetPosition();

            _tmp = new GameObject { name = "CameraPivot" };
            _pivot = _tmp.transform;
            _pivot.rotation = _transform.rotation;
            _pivot.position = _transform.position;
        }

        void OnEnable()
        {
            EventAction.Drag += RotatePlayer;
            PositionController.MoveToPosition += PositionController_MoveToPosition;
            EventAction.Save += EventAction_Save;
        }

        private void EventAction_Save()
        {
            _spPosition.Save();
        }

        void OnDisable()
        {
            EventAction.Drag -= RotatePlayer;
            PositionController.MoveToPosition -= PositionController_MoveToPosition;
            EventAction.Save -= EventAction_Save;
        }

        void OnDestroy()
        {
            Destroy(_tmp);
        }

        private void PositionController_MoveToPosition(Transform pointToMove)
        {
            if (!CanDragCamera) return;
            _pivot.position = pointToMove.position;
            _spPosition.Add(_pivot.position);
            _startPositionVector3 = _transform.position;
            _distance = Vector3.Distance(_transform.position, pointToMove.position);
            _timeMove = 0;
            _isMove = true;
        }

        private void RotatePlayer(Vector3 rotateAxis)
        {
            if (!CanDragCamera) return;
            Vector3 angle = Vector3.zero;
            angle += new Vector3(-rotateAxis.y * _speed, rotateAxis.x * _speed, rotateAxis.z);
            _rotateVector3 = CheckAngle(angle);
            _timeRotate = 0;
            _isRotate = true;
        }

        private Vector3 CheckAngle(Vector3 angle)
        {
            if (angle.x > 55 && angle.x < 200) angle.x = 55;
            else if (angle.x < 320 && angle.x > 200) angle.x = 320;
            angle.z = 0;
            return angle;
        }

        void Update()
        {
            if (_isRotate)
            {
                _timeRotate += Time.deltaTime;
                _transform.Rotate(_rotateVector3 * Time.deltaTime / _timeRotate);
                _transform.eulerAngles = CheckAngle(_transform.eulerAngles);
                if (_timeRotate > 1f)
                {
                    _isRotate = false;
                }
            }

            if (_isMove)
            {
                _transform.position = Vector3.Lerp(_startPositionVector3, _pivot.position, _timeMove);
                _timeMove += Time.deltaTime * _MovingCoefficient / _distance;
                if (_timeMove > 1f)
                {
                    _transform.position = _pivot.position;
                    _isMove = false;
                }
            }

            if (IsLock) return;
            if (IsDrag) { EventAction.HideElement(); return; }
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                _fingerDrag = true;
                _mousePosition = Input.mousePosition;
                _delta = _mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _fingerDrag = false;

                if (Vector2.Distance(_mousePosition, Input.mousePosition) < _dragTH)
                {
                    if (!IsHidden)
                    {
                        EventAction.HideElement();
                        return;
                    }
                    ClickAtItem(Input.mousePosition);
                    _mousePosition = Vector2.zero;
                }
            }
#else
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    _fingerDrag = true;
                    _mousePosition = Input.GetTouch(i).position;
                    _delta = _mousePosition;
                }

                if (Input.GetTouch(i).phase == TouchPhase.Ended)
                {
                    _fingerDrag = false;
                    if (Vector2.Distance(_mousePosition, Input.GetTouch(i).position) < _dragTH)
                    {
                        if (!IsHidden)
                        {
                            EventAction.HideElement();
                            return;
                        }
                        ClickAtItem(Input.GetTouch(i).position);
                        _mousePosition = Vector2.zero;
                    }
                }
            }
        }
#endif
            if (_fingerDrag)
            {
                DragItem(Input.mousePosition - _delta);
                _delta = Input.mousePosition;
            }
        }

        public void Pause(Action callAction, float pause)
        {
            StartCoroutine(PauseRealize(callAction, pause));
        }

        IEnumerator PauseRealize(Action action, float pause)
        {
            float time = 0;
            while (time < pause)
            {
                time += Time.deltaTime;
                yield return null;
            }
            action();
        }

        private void ClickAtItem(Vector3 position)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _fingerDrag = false;
                return;
            }

            IsDrag = true;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                IData[] datas = hit.transform.GetComponents<IData>();
                if (datas.Length == 0)
                {
                    datas = hit.transform.GetComponentsInParent<IData>();
                }
                if (datas.Length != 0)
                {
                    foreach (var data in datas)
                    {
                        data.OnClick();
                    }
                }
            }
            IsDrag = false;
        }

        private void DragItem(Vector3 distance)
        {
            IsDrag = true;
            EventAction.SetDrag(distance);
            IsDrag = false;
        }
    }


    public class EventAction
    {
        public static event UnityAction<string, string, string> Action;
        public static event UnityAction<string, Action> ActionOpen;
        public static event UnityAction<Vector3> Drag;
        public static event UnityAction Hide;
        public static event UnityAction<string> EnteredPassword;
        public static event UnityAction<string> ShowPswPanel;
        public static event UnityAction Save;

        public static void SetEvent(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("Id cannot be null");
                return;
            }
            if (Action != null)
                Action(key, null, null);

        }

        public static void SetEvent(string key, Action value)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("Id cannot be null");
                return;
            }
            if (ActionOpen != null)
                ActionOpen(key, value);
        }

        public static void SetEvent(string key, string value, string option)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("Id cannot be null");
                return;
            }
            if (Action != null)
                Action(key, value, option);
        }


        public static void SetDrag(Vector3 option)
        {
            if (Drag != null)
                Drag(option);
        }

        public static void HideElement()
        {
            if (Hide != null) Hide();
        }

        public static void EnterPassword(string psw)
        {
            if (EnteredPassword != null)
                EnteredPassword(psw);
        }

        public static void ShowPasswordPanel(string itemId)
        {
            if (ShowPswPanel != null)
                ShowPswPanel(itemId);
        }

        public static void OnSave()
        {
            if (Save != null) Save();
        }
    }
}