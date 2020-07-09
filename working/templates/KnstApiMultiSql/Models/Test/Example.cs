using System;
using System.Collections.Generic;

namespace KnstApiMultiSql.Models.Test
{
    public partial class Example
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RowDatetime { get; set; }
        public bool IsDelete { get; set; }
    }
}