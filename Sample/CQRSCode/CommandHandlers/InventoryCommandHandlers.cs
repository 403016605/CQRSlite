﻿using CQRSCode.Commands;
using CQRSCode.Domain;
using CQRSlite;
using CQRSlite.Domain;

namespace CQRSCode.CommandHandlers
{
    public class InventoryCommandHandlers : IHandles<CreateInventoryItem>, IHandles<DeactivateInventoryItem>, IHandles<RemoveItemsFromInventory>,
        IHandles<CheckInItemsToInventory>, IHandles<RenameInventoryItem>
    {
        private readonly IRepository<InventoryItem> _repository;
        private readonly ISession _session;

        public InventoryCommandHandlers(IRepository<InventoryItem> repository, ISession session)
        {
            _repository = repository;
            _session = session;
        }

        public void Handle(CreateInventoryItem message)
        {
            var item = new InventoryItem(message.InventoryItemId, message.Name);
            _repository.Add(item);
            _session.Commit();
        }

        public void Handle(DeactivateInventoryItem message)
        {
            var item = _repository.Get(message.InventoryItemId);
            item.Deactivate();
            _session.Commit();
        }

        public void Handle(RemoveItemsFromInventory message)
        {
            var item = _repository.Get(message.InventoryItemId);
            item.Remove(message.Count);
            _session.Commit();
        }

        public void Handle(CheckInItemsToInventory message)
        {
            var item = _repository.Get(message.InventoryItemId);
            item.CheckIn(message.Count);
            _session.Commit();
        }

        public void Handle(RenameInventoryItem message)
        {
            var item = _repository.Get(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _session.Commit();
        }
    }
}
