﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class BankBook
    {
        public int ID { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public DateTime ChequeDate { get; set; }
        //public int RecordId { get; set; }

        [Required]
        public virtual Record Record { get; set; }

    }
}
