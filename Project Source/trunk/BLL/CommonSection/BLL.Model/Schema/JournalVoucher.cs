﻿using System;
using System.Linq;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class JournalVoucher : VoucherBase
    {
        private const string DebitText = "debit";
        private const string CreditText = "credit";

        // ContraType can be "Bank to cash" or "Cash to bank"
        private string _jvDebitOrCredit;

        public JournalVoucher(IRecordRepository recordRepository) : base(recordRepository)
        {
        }

        public string JVDebitOrCredit
        {
            get { return _jvDebitOrCredit; }
            set
            {
                if (new[] { DebitText, CreditText }.Contains(value.ToLower()))
                    _jvDebitOrCredit = value.ToLower();
                else
                    throw new Exception("Invalid Journal typeJVDebitOrCredit. JVDebitOrCredit can only be \"" + DebitText + "\" or \"" + CreditText + "\"");
            }
        }


        public override string VoucherTypeKey
        {
            get { return Constants.JournalVoucherShortKey; }
        }

        public override double Debit
        {
            get
            {
                if (string.IsNullOrWhiteSpace(JVDebitOrCredit)) throw JVDebitOrCreditNull();
                return JVDebitOrCredit.Equals(DebitText, StringComparison.OrdinalIgnoreCase) ? Amount : 0;
            }
        }

        public override double Credit
        {
            get
            {
                if (string.IsNullOrWhiteSpace(JVDebitOrCredit)) throw JVDebitOrCreditNull();
                return JVDebitOrCredit.Equals(CreditText, StringComparison.OrdinalIgnoreCase) ? Amount : 0;
            }
        }

        private Exception JVDebitOrCreditNull()
        {
            return new Exception("JVDebitOrCredit can not be null for Journal Voucher."); 
        }

        //public static JournalVoucher Clone(Record record)
        //{
        //    return new JournalVoucher(record.RecordRepository)
        //    {
        //        VoucherSerialNo = record.VoucherSerialNo,
        //        ProjectName = record.ProjectName,
        //        Date = record.Date,
        //        Narration = record.Narration,
        //        LinkedVoucherNo = record.LinkedVoucherNo,
        //        Tag = record.Tag,
        //        VoucherTypeKey = record.VoucherTypeKey,
        //        HeadName = record.HeadName,
        //        Debit = record.Debit,
        //        Credit = record.Credit
        //    };
        //}
    }
}