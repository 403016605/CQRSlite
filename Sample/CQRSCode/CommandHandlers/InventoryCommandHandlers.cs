﻿using CQRSCode.Commands;
using CQRSCode.Domain;
using CQRSlite;
using CQRSlite.Domain;

namespace CQRSCode.CommandHandlers
{
    public class InventoryCommandHandlers : IHandles<CreateInventoryItem>, IHandles<DeactivateInventoryItem>, IHandles<RemoveItemsFromInventory>,
        IHandles<CheckInItemsToInventory>, IHandles<RenameInventoryItem>
    {
        private readonly ISession _session;

        public InventoryCommandHandlers(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateInventoryItem message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _session.Add(item);
            _session.Commit();
        }

        public void Handle(DeactivateInventoryItem message)
        {
            var item = _session.Get<InventoryItem>(message.InventoryItemId, message.ExpectedVersion);
            item.Deactivate();
            _session.Commit();
        }

        public void Handle(RemoveItemsFromInventory message)
        {
            var item = _session.Get<InventoryItem>(message.InventoryItemId, message.ExpectedVersion);
            item.Remove(message.Count);
            _session.Commit();
        }

        public void Handle(CheckInItemsToInventory message)
        {
            var item = _session.Get<InventoryItem>(message.InventoryItemId, message.ExpectedVersion);
            item.CheckIn(message.Count);
            _session.Commit();
        }

        public void Handle(RenameInventoryItem message)
        {
            var item = _session.Get<InventoryItem>(message.InventoryItemId, message.ExpectedVersion);
            item.ChangeName(message.NewName);
            _session.Commit();
        }
    }
}
