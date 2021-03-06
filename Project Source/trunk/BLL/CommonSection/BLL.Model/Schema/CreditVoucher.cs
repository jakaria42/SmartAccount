﻿
using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class CreditVoucher : VoucherBase
    {
        public CreditVoucher()
        {
        }

        public CreditVoucher(IRepository<Record> recordRepository)
            : base(recordRepository, null)
        {
        }

        public override string VoucherTypeKey()
        {
            return Constants.CreditVoucherShortKey; 
        }

        public override void SetAmount(double amount)
        {
            Credit = amount;
        }

        //public override double Debit
        //{
        //    get { return 0; }
        //}

        //public override double Credit
        //{
        //    get { return Amount; }
        //}
    }
}


