﻿using System.ComponentModel.DataAnnotations;

namespace AlertToCareApi.Models
{
    public class Icu
    {
        [Key]
        public int IcuNo { get; set; }
        public int FloorNo { get; set; }
        public int LayoutId { get; set; }
    }
}
