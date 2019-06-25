// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EscapeModules
{
    /// <summary>
    /// Make scroll and take element from inventory
    /// </summary>
    public class InventoryScrollControll : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        [SerializeField] private RectTransform _content;
        private GridLayoutGroup _grid;

        private float _elementHeight;
        private float _spacingHeight;
        private float _totalDragDelta;
        private float _currentDelta;

        private bool _dragingItem;
        private bool _isHorizontal;

        private int _currentFirstVisibleElement;

        private Vector2 _finalisingPos;

        private GameObject _dragOverObject;
        private GameObject _clickedGameObject;

        private InventoryController _inventoryController;

        private const float _minDragDistance = 10f;

        public void Init()
        {
            if (_content == null) Debug.LogError("EscapeModules: ItemMenuScrollControll is absent on child content object " + gameObject.name);

            _grid = _content.GetComponent<GridLayoutGroup>();
            if (_grid == null) Debug.LogError("EscapeModules: ItemMenuScrollControll is absent on child content object " + gameObject.name);

            _inventoryController = GetComponentInParent<InventoryController>();
            if (_inventoryController == null) Debug.LogError("EscapeModules:Component ItemMenuController is absent on parent object " + gameObject.name);

            _isHorizontal = _inventoryController.IsHorizontal;
            _elementHeight = _isHorizontal ? _grid.cellSize.x : _grid.cellSize.y;
            _spacingHeight = _isHorizontal ? _grid.spacing.x : _grid.spacing.y;
        }



        #region Drag Handlers region
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragOverObject = eventData.pointerEnter;
        }

        public void OnDrag(PointerEventData eventData)
        {
            PlayerController.Instance.CanDragCamera = false;
            float deltaX = eventData.delta.x;
            float deltaY = eventData.delta.y;

            if (deltaX < 0) deltaX = -deltaX; //abs
            if (deltaY < 0) deltaY = -deltaY;
            bool canScroll = false;
            if (_inventoryController.IsScrollAble)
            {
                _totalDragDelta += deltaY > deltaX ? deltaY : deltaX;
                canScroll = true;
                _currentDelta = 0;
            }
            else
            {
                _totalDragDelta += _isHorizontal ? deltaY : deltaX;
                canScroll = _isHorizontal ? deltaX < deltaY : deltaX > deltaY;
                _currentDelta = _isHorizontal ? eventData.delta.x : eventData.delta.y;
            }

            if (!_dragingItem)
            {
                if (_totalDragDelta > _minDragDistance && canScroll) //release element from menu
                {
                    if (_inventoryController.MenuIsOpening) return;
                    if (_dragOverObject == null) return;
                    if (!_inventoryController.IsNotNullElement(_dragOverObject.name)) return;
                    _inventoryController.TakeFromMenu(_dragOverObject.name);
                    _dragingItem = true;
                    FinaliseDrag(0);
                    PlayerController.Instance.IsDrag = true;
                    PlayerController.Instance.IsLock = true;
                }
            }
            else
            {
                _inventoryController.Drag(eventData.position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_inventoryController.GetDragEnabled) FinaliseDrag(_currentDelta);
            _totalDragDelta = 0;
            _currentDelta = 0;
            if (_dragingItem)
            {
                _dragingItem = false;
                _inventoryController.ReleaseFromDragging();
            }
            PlayerController.Instance.IsDrag = false;
            PlayerController.Instance.IsLock = false;
            PlayerController.Instance.CanDragCamera = true;
        }

        #endregion

        void FinaliseDrag(float delta)
        {
            float menu = (_isHorizontal ? _content.anchoredPosition.x : _content.anchoredPosition.y) / (_elementHeight + _spacingHeight);
            if (delta == 0)
                _currentFirstVisibleElement = Mathf.RoundToInt(menu);
            else
                _currentFirstVisibleElement = delta < 0 ? Mathf.FloorToInt(menu) : Mathf.CeilToInt(menu);

            if (_currentFirstVisibleElement >
                (_inventoryController.ActualItemsCount - _inventoryController.VisibleItems)) _currentFirstVisibleElement = (_inventoryController.ActualItemsCount - _inventoryController.VisibleItems);
            if (_currentFirstVisibleElement < 0) _currentFirstVisibleElement = 0;
        }

        public void SetFinalisingPosition(int curvisibleElement)
        {
            _finalisingPos = new Vector2(_isHorizontal ? curvisibleElement * (_elementHeight + _spacingHeight) : 0, _isHorizontal ? 0 : curvisibleElement * (_elementHeight + _spacingHeight));
            _content.anchoredPosition = _finalisingPos;
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (_dragingItem) return;
            if (_clickedGameObject != eventData.pointerEnter) return;
            _inventoryController.PlayClickSound();
            _inventoryController.ShowGameObject(eventData.pointerEnter.name);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_dragingItem) return;
            _clickedGameObject = eventData.pointerEnter;
            PlayerController.Instance.CanDragCamera = false;
            EventAction.HideElement();
        }
    }
}