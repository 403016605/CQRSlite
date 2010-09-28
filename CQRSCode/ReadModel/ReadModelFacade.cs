﻿using System;
using System.Collections.Generic;
using CQRSCode.Infrastructure;
using CQRSCode.ReadModel.Dtos;

namespace CQRSCode.ReadModel
{
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return BullShitDatabase.List;
        }

        public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
        {
            return BullShitDatabase.Details[id];
        }
    }
}