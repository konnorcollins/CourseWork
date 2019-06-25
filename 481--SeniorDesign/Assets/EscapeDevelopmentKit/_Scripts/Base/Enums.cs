// Copyright (c) TODA. All rights reserved.  http://todagroup.com
// Licensed under the MIT license. See LICENSE file in the project root.

namespace EscapeModules
{

    /// <summary>
    /// All Enums are here
    /// </summary>
    
        
    public enum StateEnum
    {
        Locked = -1,
        Close,
        Open = 1,
        InProcess
    }


    public enum DirectionEnum
    {
        Horizontal,
        Vertical
    }

    public enum KeyButtonOptionEnum
    {
        Default,
        Delete,
        Enter
    }


    public enum ActionRotateOrMoveEnum
    {
        Move,
        Rotate
    }


    public enum InventoryEnum
    {
        Default,
        Inventory1,
        Inventory2,
        Inventory3
    }


    public enum ChangeableEnum
    {
        Click,
        Item
    }
}