﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Model.Repositories;


namespace SQL2K8
{
    public class LedgerRepository : ILedgerRepository
    {
        SmartAccountEntities db = new SmartAccountEntities();

        public IList<Ledger> GetLedger(int projectId)
        {
            int[] projectHeadIds = db.ProjectHeads.Where(ph => ph.ProjectID == projectId).Select(ph => ph.ID).ToArray();
            double balance = 0;
            return db.Records.Where(r => projectHeadIds.Contains(r.ProjectHeadID) && r.Tag == "Advance" && r.LedgerType == "LedgerBook").ToList()
                .Select(r => GetLedger(r, ref balance)).ToList();
        }

        public IList<Ledger> GetLedger(int projectId, int headId)
        {
            int projectHeadId =
                db.ProjectHeads.Where(pc => pc.ProjectID == projectId && pc.HeadID == headId).SingleOrDefault().ID;
            double balance = 0;
            return db.Records.Where(r => r.ProjectHeadID == projectHeadId).ToList()
                .Select(r => GetLedger(r, ref balance)).ToList();
        }

        private BankRecord GetBankRecord(int recordId)
        {
            return db.BankRecords.Where(br => br.RecordID == recordId).SingleOrDefault();
        }

        private Ledger GetLedger(Record record, ref double previousBalance)
        {
            BankRecord bankRecord = GetBankRecord(record.ID);
            double newBalance = previousBalance + record.Debit - record.Credit;
            Ledger ledger = new Ledger
                                   {
                                       Credit = record.Credit,
                                       Debit = record.Debit,
                                       Date = record.Date,
                                       VoucherNo = record.VoucherType + "-" + record.VoucherSerialNo,
                                       Particular = bankRecord != null ? "Bank" : "Cash", // TODO: This is wrong for showing all advance.
                                       ChequeNo = bankRecord != null ? bankRecord.ChequeNo : "",
                                       Remarks = record.Narration,
                                       Balance = newBalance
                                   };
            previousBalance = newBalance;
            return ledger;
        }

        public IList<Ledger> GetLedger(int projectId, int headId, TimeSpan dateRange)
        {
            throw new NotImplementedException();
        }

        public IList<Ledger> GetLedger(int projectId, int headId, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
