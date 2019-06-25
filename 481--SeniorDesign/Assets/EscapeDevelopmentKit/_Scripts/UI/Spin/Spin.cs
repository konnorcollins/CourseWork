// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EscapeModules
{
    /// <summary>
    /// Spin rotate and choose item.
    /// </summary>
    public class Spin : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform[] Rects;
        [SerializeField] private DirectionEnum _direction = DirectionEnum.Vertical;
        [SerializeField] private float AutoRotateThreshold = 10f;

        private Action _callBackAction;

        private float _size;

        int index = 0;

        private bool _isHorizontal;

        void Start()
        {
            if (Rects.Length == 0) Debug.LogError("EscapeModules: Rects is empty " + gameObject.name);
            _isHorizontal = _direction == DirectionEnum.Horizontal;

            _size = _isHorizontal ? Rects[0].sizeDelta.x : Rects[0].sizeDelta.y;
            for (int i = 0; i < Rects.Length; i++)
            {
                Rects[i].anchoredPosition -= new Vector2(_isHorizontal ? -_size * 2f + i * _size : 0, _isHorizontal ? 0 : -_size * 2f + i * _size);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            StopAllCoroutines();
            RotateObj(_isHorizontal ? eventData.delta.x : eventData.delta.y);

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(_isHorizontal ? eventData.delta.x : eventData.delta.y) > AutoRotateThreshold)
            {
                StartCoroutine(AutoRotate(_isHorizontal ? eventData.delta.x : eventData.delta.y));
            }
            else
            {
                CheckEnd(0);
            }
        }

        IEnumerator AutoRotate(float Delta)
        {
            float sign = Mathf.Sign(Delta);

            float speed = Mathf.Clamp(Mathf.Abs(Delta), 2, 20f);
            while (speed > 5)
            {
                RotateObj(sign * speed);
                speed -= Time.deltaTime * 5f;
                yield return null;
            }
            CheckEnd(sign);
        }

        void RotateObj(float delta)
        {
            for (int i = 0; i < Rects.Length; i++)
            {
                Rects[i].anchoredPosition += new Vector2(_isHorizontal ? delta : 0, _isHorizontal ? 0 : delta);
            }
            CheckPosition();
        }

        void CheckEnd(float AutoRotateDirect)
        {
            index = FindWinNumber();

            if (AutoRotateDirect == 0)
            {
                if ((_isHorizontal ? Rects[index].anchoredPosition.x % _size : Rects[index].anchoredPosition.y % _size) < 0)
                {
                    StartCoroutine(AutoFinish(1));
                }
                else
                {
                    StartCoroutine(AutoFinish(-1));
                }
            }
            else
            {
                StartCoroutine(AutoFinish(AutoRotateDirect));
            }
        }

        void CheckPosition()
        {
            for (int i = 0; i < Rects.Length; i++)
            {
                float rectPos = _isHorizontal ? Rects[i].anchoredPosition.x : Rects[i].anchoredPosition.y;
                if (rectPos >
                    _size * 2)
                {
                    int previos = i - 1;
                    if (previos < 0)
                        previos = Rects.Length - 1;
                    Rects[i].anchoredPosition = Rects[previos].anchoredPosition - new Vector2(_isHorizontal ? _size : 0, _isHorizontal ? 0 : _size);
                }
                else if (rectPos < -_size * (Rects.Length - 2))
                {
                    int next = i + 1;
                    if (next == Rects.Length) next = 0;
                    Rects[i].anchoredPosition = Rects[next].anchoredPosition + new Vector2(_isHorizontal ? _size : 0, _isHorizontal ? 0 : _size);
                }
            }
        }

        IEnumerator AutoFinish(float direct)
        {
            while (Mathf.Abs(_isHorizontal ? Rects[index].anchoredPosition.x % _size : Rects[index].anchoredPosition.y % _size) > 2)
            {
                for (int i = 0; i < Rects.Length; i++)
                {
                    Rects[i].anchoredPosition += new Vector2(_isHorizontal ? direct * Time.deltaTime * 200f : 0, _isHorizontal ? 0 : direct * Time.deltaTime * 200f);
                }
                CheckPosition();
                yield return null;
            }
            if (_callBackAction != null) _callBackAction();
        }

        int FindWinNumber()
        {
            int result = 0;
            for (int i = 0; i < Rects.Length; i++)
            {
                float recPos = _isHorizontal ? Rects[i].anchoredPosition.x : Rects[i].anchoredPosition.y;
                if (recPos > -_size / 2f && recPos < _size / 2f)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public string GetValue()
        {
            return Rects[FindWinNumber()].GetComponent<SpinItem>().GetValue();
        }

        public void AddListener(Action action)
        {
            _callBackAction = action;
        }
    }
}
