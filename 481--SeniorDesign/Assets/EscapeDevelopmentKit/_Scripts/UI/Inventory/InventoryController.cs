// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Build inventory by height and width.  Add elements and chek it if used.
    /// This is dontDestroy object
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryEnum _name = InventoryEnum.Default;
        [SerializeField] private GameObject _inventoryItem;
        [SerializeField] private RectTransform Content;
        [SerializeField] private float _spacing;
        [SerializeField] private DirectionEnum _position;

        public DirectionEnum InvPosition
        {
            get { return _position; }
        }

        [SerializeField] private TextAnchor _gridAlignment;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _openCloseTime;
        [Space(10)] [SerializeField] private AudioClip _clickClip;

        private int _visibleItems;

        public int VisibleItems
        {
            get { return _visibleItems; }
        }

        private bool _dragIsEnabled;

        public bool GetDragEnabled
        {
            get { return _dragIsEnabled; }
        }

        private GridLayoutGroup _grid;
        private RectTransform _mainRectTransform;
        private InventoryScrollControll _scrollControll;
        private ScrollRect _scrollRect;

        private List<ButtonClass> _buttons = new List<ButtonClass>();
        private ButtonClass _draggingObject;

        private int _dragObjectSiblingIndex;

        private int _actualItemsCount;

        public int ActualItemsCount
        {
            get { return _actualItemsCount; }
        }

        private int _currentElementNumber;
        private int _removeIndex = -1;

        private Vector2 _openPosition;
        private Vector2 _targetPosition;
        private Vector2 _startPosition;

        private bool _menuIsOpen;
        private bool _opening;
        private float _timer;

        public bool MenuIsOpening
        {
            get { return _opening; }
        }

        private SoundController _soundController;

        private bool _isHorizontal;

        public bool IsHorizontal
        {
            get { return _isHorizontal; }
        }

        private bool _isHorizintalHidden;

        public bool IsHorizontalHidden
        {
            get { return _isHorizintalHidden; }
        }

        private RectTransform _rectTransform;
        private Vector2 _addSize;

        void Awake()
        {

            _isHorizontal = _position == DirectionEnum.Horizontal;
            _isHorizintalHidden = !_isHorizontal;
            _rectTransform = transform as RectTransform;
            InventoryControlSystem.DontDestroy(gameObject.transform.parent, _name);
        }

        void Start()
        {
            if (!Data.IsOffSound)
                if (_clickClip != null)
                    _soundController = gameObject.AddComponent<SoundController>();
            if (_inventoryItem == null) Debug.LogError("EscapeModules: ButtonPref is empty " + gameObject.name);
            _inventoryItem.SetActive(false);
            if (Content == null) Debug.LogError("EscapeModules: Content is empty " + gameObject.name);

            _scrollRect = Content.transform.parent.GetComponent<ScrollRect>();
            if (_scrollRect == null)
                Debug.LogError("EscapeModules: ScrollRect must be at " + Content.transform.parent.name);

            if (_openButton == null)
                Debug.LogError("EscapeModules: Open Close Button can't be null " + gameObject.name);
            else
            {
                RectTransform rectTransform = _openButton.transform as RectTransform;
                if (rectTransform != null)
                    rectTransform.SetAsLastSibling();
                _openButton.gameObject.SetActive(false);
            }
            if (_closeButton == null)
                Debug.LogError("EscapeModules: Open Close Button can't be null " + gameObject.name);
            else
            {
                RectTransform rectTransform = _closeButton.transform as RectTransform;
                if (rectTransform != null)
                    rectTransform.SetAsLastSibling();
                _closeButton.gameObject.SetActive(true);
            }
            InitMenuParams();
        }

        void OnEnable()
        {
            ItemController.AddItemToInventory += AddItem;
            _openButton.onClick.AddListener(OpenClose);
            _closeButton.onClick.AddListener(OpenClose);
        }

        void OnDisable()
        {
            ItemController.AddItemToInventory -= AddItem;
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }


        void InitMenuParams()
        {
            _mainRectTransform = transform.parent as RectTransform;

            if (_mainRectTransform != null) _mainRectTransform.SetAsLastSibling();
            _grid = Content.GetComponent<GridLayoutGroup>();
            if (_grid == null)
                Debug.LogError("EscapeModules: Need GridLayoutGroup component in content " + gameObject.name);
            _scrollControll = _scrollRect.GetComponent<InventoryScrollControll>();
            if (_scrollControll == null)
                Debug.LogError("EscapeModules: Need ItemMenuScrollControll component in " + gameObject.name);
            _scrollControll.Init();

            Vector2 size = new Vector2((_inventoryItem.transform as RectTransform).rect.width,
                (_inventoryItem.transform as RectTransform).rect.height);

            _grid.cellSize = size;
            _grid.childAlignment = _gridAlignment;

            _grid.constraint = !_isHorizontal
                ? GridLayoutGroup.Constraint.FixedColumnCount
                : GridLayoutGroup.Constraint.FixedRowCount;
            _grid.constraintCount = 1;

            _grid.startCorner = !_isHorizontal
                ? GridLayoutGroup.Corner.UpperRight
                : GridLayoutGroup.Corner.UpperLeft;

            _grid.spacing = new Vector2(_isHorizontal ? _spacing : 0, _isHorizontal ? 0 : _spacing);

            float _elementHeight = _isHorizontal
                ? _grid.cellSize.x + _grid.spacing.x * 2
                : _grid.cellSize.y + _grid.spacing.y * 2;

            _addSize = new Vector2(!_isHorizontal ? 0 : _elementHeight, !_isHorizontal ? _elementHeight : 0);

            _openCloseTime = _openCloseTime <= 0 ? 0 : _openCloseTime;
            _menuIsOpen = true;
            _openPosition = _rectTransform.anchoredPosition;


            _scrollRect.vertical = false;
            _scrollRect.horizontal = false;
            RectTransform scrollRectTransform = _scrollRect.transform as RectTransform;
            _visibleItems = (int)(_isHorizontal
                ? scrollRectTransform.rect.width / (_grid.cellSize.x + _grid.spacing.x)
                : scrollRectTransform.rect.height / (_grid.cellSize.y + _grid.spacing.y));

            for (int i = 0; i < _visibleItems; i++)
            {
                AddItem("null");
            }
        }

        public float GetElementHeight
        {
            get { return IsHorizontal ? _rectTransform.rect.height : _rectTransform.rect.width; }
        }


        public void AddItem(string itemId)
        {
            GameObject go = Instantiate(_inventoryItem, Vector2.zero, Quaternion.identity) as GameObject;
            go.name = itemId;
            go.SetActive(true);
            go.transform.SetParent(Content);
            go.transform.localScale = Vector3.one;

            _buttons.Add(new ButtonClass(go.transform as RectTransform, itemId));

            _actualItemsCount++;
            ChechIfMoveEnable();
            int showenelementcount = _actualItemsCount - _visibleItems;
            showenelementcount = showenelementcount >= 0 ? showenelementcount : 0;

            _scrollControll.SetFinalisingPosition(showenelementcount);

            if (_currentElementNumber > 0)
                --_currentElementNumber;
        }


        public void AddItem(InventoryEnum inventory, ItemProperty itemProperty)
        {
            if (_name != inventory) return;
            if (_soundController != null) _soundController.PlaySound(_clickClip);
            if (!_menuIsOpen)
                OpenClose();
            GameObject go = Instantiate(_inventoryItem, Vector2.zero, Quaternion.identity) as GameObject;
            go.name = itemProperty.ID;
            go.SetActive(true);
            go.GetComponent<Image>().sprite = itemProperty.Image;
            go.transform.SetParent(Content);

            go.transform.localScale = Vector3.one;

            if (_currentElementNumber < _visibleItems)
            {
                Destroy(_buttons[_currentElementNumber].ButtonRect.gameObject);
                go.transform.SetSiblingIndex(_currentElementNumber);
                _buttons[_currentElementNumber] = new ButtonClass(go.transform as RectTransform, itemProperty);
                _currentElementNumber++;
            }
            else
            {
                go.transform.SetAsLastSibling();
                _buttons.Add(new ButtonClass(go.GetComponent<RectTransform>(), itemProperty));
                _actualItemsCount++;

                ChechIfMoveEnable();

                int showenelementcount = _actualItemsCount - _visibleItems;
                showenelementcount = showenelementcount >= 0 ? showenelementcount : 0;
                if (_visibleItems < _buttons.Count) ResizeContent();
                _scrollControll.SetFinalisingPosition(showenelementcount);

            }
        }

        void ChechIfMoveEnable()
        {
            _dragIsEnabled = _buttons.Count > _visibleItems;
            switch (_position)
            {
                case DirectionEnum.Horizontal:
                    _scrollRect.horizontal = _dragIsEnabled;
                    break;
                case DirectionEnum.Vertical:
                    _scrollRect.vertical = _dragIsEnabled;
                    break;
            }
        }

        public void TakeFromMenu(string itemName)
        {
            int index = -1;
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (itemName == _buttons[i].ButtonRect.name)
                {
                    index = i;
                    _removeIndex = i;
                    break;
                }
            }

            if (index != -1)
            {
                _draggingObject = new ButtonClass(_buttons[index]);

                GameObject go = Instantiate(_inventoryItem, Vector2.zero, Quaternion.identity) as GameObject;
                go.name = _draggingObject.ItemId;
                go.SetActive(true);
                _buttons[index] = new ButtonClass(go.transform as RectTransform, _draggingObject.ItemId);
                go.transform.SetParent(Content);
                go.transform.localScale = Vector3.one;

                _dragObjectSiblingIndex = _draggingObject.ButtonRect.GetSiblingIndex();
                _draggingObject.ButtonRect.transform.SetParent(_mainRectTransform);
                _buttons[index].ButtonRect.SetSiblingIndex(_dragObjectSiblingIndex);

                _grid.enabled = false;
                _grid.enabled = true;
                switch (_position)
                {
                    case DirectionEnum.Horizontal:
                        _scrollRect.horizontal = false;
                        break;
                    case DirectionEnum.Vertical:
                        _scrollRect.vertical = false;
                        break;
                }
            }
        }

        public bool IsNotNullElement(string itemName)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (itemName == _buttons[i].ButtonRect.name)
                {
                    if (_buttons[i].ItemId == "null") return false;
                }
            }
            return true;
        }

        public void Drag(Vector2 position)
        {
            if (_draggingObject == null || _draggingObject.ButtonRect == null) return;
            _draggingObject.ButtonRect.position = position;
        }


        public void ReleaseFromDragging()
        {
            if (CheckIfItemUsed()) return;
            if (_removeIndex != -1)
            {
                Destroy(_buttons[_removeIndex].ButtonRect.gameObject);
                _buttons[_removeIndex] = _draggingObject;
                _buttons[_removeIndex].ButtonRect.transform.SetParent(Content);
                _buttons[_removeIndex].ButtonRect.SetSiblingIndex(_dragObjectSiblingIndex);
                _removeIndex = -1;
            }
            _draggingObject = null;
            switch (_position)
            {
                case DirectionEnum.Horizontal:
                    _scrollRect.horizontal = _dragIsEnabled;
                    break;
                case DirectionEnum.Vertical:
                    _scrollRect.vertical = _dragIsEnabled;
                    break;
            }
            _grid.enabled = false;
            _grid.enabled = true;
        }

        bool CheckIfItemUsed()
        {
            if (_draggingObject == null || _draggingObject.ButtonRect == null) return false;
            Ray ray =
                Camera.main.ScreenPointToRay(new Vector3(_draggingObject.ButtonRect.position.x,
                    _draggingObject.ButtonRect.position.y, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10)) //NOTE Raycast 1000 mitters
            {
                if (hit.collider != null)
                {
                    IData data = hit.collider.gameObject.GetComponent<IData>() ??
                                 hit.collider.gameObject.GetComponentInParent<IData>();

                    if (data != null)
                    {
                        if (!_draggingObject.IsCanOpen(data.GetId()))
                            return false;
                        data.UnLock();
                        RemoveFromMenu();
                        return true;
                    }
                }
            }
            return false;
        }

        void RemoveFromMenu()
        {
            _actualItemsCount--;
            Destroy(_buttons[_removeIndex].ButtonRect.gameObject);
            _buttons.RemoveAt(_removeIndex);
            _removeIndex = -1;
            Destroy(_draggingObject.ButtonRect.gameObject);
            _draggingObject = null;
            ChechIfMoveEnable();
            if (_buttons.Count < _visibleItems)
            {
                AddItem("null");
            }
            SubtractionContent();
        }

        public void ShowGameObject(string itemName)
        {
            int index = -1;
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (itemName == _buttons[i].ButtonRect.name)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                _draggingObject = new ButtonClass(_buttons[index]);
                _dragObjectSiblingIndex = _draggingObject.ButtonRect.GetSiblingIndex();

                if (!_draggingObject.IsCanShowFromInventoru)
                {
                    ReleaseFromDragging();
                    PlayerController.Instance.CanDragCamera = true;
                    return;
                }
                GameObject go = _draggingObject.ItemGameObject;
                go.transform.position = PlayerController.Instance.TargePosition;
                go.SetActive(true);
                go.GetComponent<ItemController>().ActiveItems();
                PlayerController.Instance.IsHidden = false;
            }
        }


        public void PlayClickSound()
        {
            if (_soundController != null)
                _soundController.PlaySound(_clickClip);
        }

        public void OpenClose()
        {
            if (_opening) return;

            _timer = 0;
            _menuIsOpen = !_menuIsOpen;
            int count = _buttons.Count;

            if (_isHorizontal)
                count = _isHorizintalHidden ? count : 1;
            else
                count = !_isHorizintalHidden ? count : 1;

            count = count < _visibleItems ? count : _visibleItems;

            if (_menuIsOpen)
            {

                _targetPosition = _openPosition;
                _startPosition = new Vector2(
                    _isHorizintalHidden ? _openPosition.x + (_spacing + GetElementHeight) * count : _openPosition.x,
                    _isHorizintalHidden ? _openPosition.y : _openPosition.y + (_spacing + GetElementHeight) * count);
            }
            else
            {
                _targetPosition = new Vector2(
                    _isHorizintalHidden ? _openPosition.x + (_spacing + GetElementHeight) * count : _openPosition.x,
                    _isHorizintalHidden ? _openPosition.y : _openPosition.y + (_spacing + GetElementHeight) * count);
                _startPosition = _openPosition;
            }
            StartCoroutine(OpenButton());
        }


        IEnumerator OpenButton()
        {
            PlayClickSound();
            _timer = 0;
            _opening = true;
            while (_timer < 1f)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, _targetPosition, _timer);
                _timer += Time.deltaTime / _openCloseTime;
                yield return null;
            }
            _openButton.gameObject.SetActive(!_openButton.gameObject.activeSelf);
            _closeButton.gameObject.SetActive(!_closeButton.gameObject.activeSelf);
            _opening = false;
            _rectTransform.anchoredPosition = _targetPosition;
        }

        private void ResizeContent()
        {
            Content.sizeDelta += _addSize;
        }

        private void SubtractionContent()
        {
            if (_buttons.Count >= _visibleItems)
                Content.sizeDelta -= _addSize;

            if (_buttons.Count == _visibleItems)
            {
                Content.anchoredPosition = Vector2.zero;
            }

        }

        public bool IsScrollAble
        {
            get { return _visibleItems == _buttons.Count; }
        }
    }


    public class InventoryControlSystem : MonoBehaviour
    {
        private static List<Transform> savedObjects = new List<Transform>();
        private static List<InventoryEnum> _enabledEnumList = new List<InventoryEnum>();

        public static void DontDestroy(Transform obj, InventoryEnum type)
        {
            for (int i = 0; i < _enabledEnumList.Count; i++)
            {
                if (_enabledEnumList[i] == type)
                {
                    Debug.LogWarning("EscapeModules: You have two same inventory " + type);
                    DestroyImmediate(obj);
                    return;
                }
            }
            savedObjects.Add(obj);
            DontDestroyOnLoad(obj);
        }

        public static void DestroyObject(Transform obj)
        {
            savedObjects.Remove(obj);
            Destroy(obj);
        }

        public static List<Transform> GetSavedObjects()
        {
            return new List<Transform>(savedObjects);
        }

        public static void DestroyAll()
        {
            for (int i = 0; i < savedObjects.Count; i++)
            {
                Destroy(savedObjects[i].gameObject);
            }
            _enabledEnumList.Clear();
            savedObjects.Clear();
        }
    }
}