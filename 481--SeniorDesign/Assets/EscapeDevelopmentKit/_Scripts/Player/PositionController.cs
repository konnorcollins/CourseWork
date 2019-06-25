// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;
using UnityEngine.Events;

namespace EscapeModules
{
    /// <summary>
    /// Send position to player when click on the object with collider
    /// </summary>
    public class PositionController : MonoBehaviour, IData
    {
        public static event UnityAction<Transform> MoveToPosition;

        [SerializeField] private Color _gizmosColor;

        private Transform _transform;
        private Action<string> _action;

        void Start()
        {
            _transform = transform;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        public string GetId()
        {
            return null;
        }

        public void OnClick()
        {
            if (MoveToPosition != null) MoveToPosition(_transform);
        }

        public void UnLock()
        {
            if (MoveToPosition != null) MoveToPosition(_transform);
        }

        public void ActionElement(Action<string, int> action)
        {
            if (MoveToPosition != null) MoveToPosition(_transform);
        }
    }
}