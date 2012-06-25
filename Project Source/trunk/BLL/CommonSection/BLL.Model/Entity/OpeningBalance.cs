﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class OpeningBalance
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Balance { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string FinancialYear { get; set; }

        [Required]
        public virtual ProjectHead ProjectHead { get; set; }
    }
}
