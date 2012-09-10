﻿using System;
using CQRSlite.Contracts.Bus.Messages;

namespace CQRSCode.ReadModel.Events
{
    public class InventoryItemRenamed : Event
    {
        public readonly string NewName;
 
        public InventoryItemRenamed(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}