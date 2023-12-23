﻿using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;

namespace Domain.Transactions.Agreggates
{
    public class Transaction : BaseModel
    {
        public DateTime DtTransaction { get; set; }
        public Monetary Value { get; set; }
        public String Description { get; set; }
        public Merchant Merchant { get; set; }
    }
}
