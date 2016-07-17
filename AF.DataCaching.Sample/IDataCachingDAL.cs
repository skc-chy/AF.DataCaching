using AFDataCaching;
using System;
using System.Collections.Generic;

namespace SampleApplication.Data.DataCachingDemo
{

    public interface IDataCachingDemoDAL
    {
        Result AddEmployee(DataCachingEntity employeeData);

        Result UpdateEmployee(DataCachingEntity employeeData);

        Result DeleteEmployee(Guid empID);

        List<DataCachingEntity> GetEmployeeList();
    }
}
