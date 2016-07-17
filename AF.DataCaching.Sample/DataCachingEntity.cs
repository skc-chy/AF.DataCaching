using System;

namespace AFDataCaching
{
    public sealed class DataCachingEntity
    {
        public Guid EmpID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }
    }
}