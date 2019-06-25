// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

using System;

namespace EscapeModules
{
    /// <summary>
    /// Interface for all objects
    /// </summary>
    public interface IData
    {
        string GetId();
        void OnClick();
        void UnLock();
        void ActionElement(Action<string, int> action);
    }
}