using Architecture.Foundation;
using Architecture.Foundation.DataAccessor;
using Architecture.Foundation.DataAccessor.SqlClient;
using SampleApplication.Data.DataCachingDemo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AFDataCaching
{
    /// <summary>
    /// Data caching implemented in get method
    /// Clear caching in insert/update/delete method
    /// </summary>
    /// 

    [AFDataStore("AF")]
    public class DataCachingDemoDAL : AFDataStoreAccessor, IDataCachingDemoDAL
    {
        public Result AddEmployee(DataCachingEntity employeeData)
        {
            var result = new Result() { IsValid = false };

            StoreProcedureCommand procedure = CreateProcedureCommand("dbo.InsertEmployee");
            procedure.AppendGuid("EmpID", employeeData.EmpID);
            procedure.AppendNVarChar("Name", employeeData.Name);
            procedure.AppendNVarChar("Address", employeeData.Address);
            procedure.AppendNVarChar("EMail", employeeData.EMail);
            procedure.AppendNVarChar("Phone", employeeData.Phone);

            int resultValue = ExecuteCommand(procedure);

            if (resultValue == 0)
            {
                result.IsValid = true;
                result.Message = new List<string> { "Employee added successfully" };
                //Clear the cache by type
                AFCache.Caching.ClearCacheByType<List<DataCachingEntity>>();
            }

            return result;
        }

        public Result UpdateEmployee(DataCachingEntity employeeData)
        {
            var result = new Result() { IsValid = false };

            StoreProcedureCommand procedure = CreateProcedureCommand("dbo.UpdateEmployee");
            procedure.AppendGuid("EmpID", employeeData.EmpID);
            procedure.AppendNVarChar("Address", employeeData.Address);
            procedure.AppendNVarChar("EMail", employeeData.EMail);
            procedure.AppendNVarChar("Phone", employeeData.Phone);

            int resultValue = ExecuteCommand(procedure);

            if (resultValue == 0)
            {
                result.IsValid = true;
                result.Message = new List<string> { "Employee updated successfully" };
                //Clear the cache by type
                AFCache.Caching.ClearCacheByType<List<DataCachingEntity>>();
            }

            return result;
        }

        public Result DeleteEmployee(Guid empID)
        {
            var result = new Result() { IsValid = false };

            StoreProcedureCommand procedure = CreateProcedureCommand("dbo.DeleteEmployee");
            procedure.AppendGuid("EmpID", empID);

            int resultValue = ExecuteCommand(procedure);

            if (resultValue == 0)
            {
                result.IsValid = true;
                result.Message = new List<string> { "Employee deleted successfully" };
                //Clear the cache by type
                AFCache.Caching.ClearCacheByType<List<DataCachingEntity>>();
            }

            return result;
        }

        public List<DataCachingEntity> GetEmployeeList()
        {
            List<DataCachingEntity> empList;

            //Get cache object
            //it return default of object type
            var parameterList = AFCache.Caching.CreateSortedList<String>();
            empList = AFCache.Caching.GetCache<List<DataCachingEntity>>(parameterList);

            //If cache is null then call database
            if (empList == default(List<DataCachingEntity>) || empList.Count == 0)
            {
                SqlDataReader reader = null;
                empList = new List<DataCachingEntity>();

                try
                {
                    StoreProcedureCommand procedure = CreateProcedureCommand("dbo.GetEmployee");
                    reader = ExecuteCommandAndReturnDataReader(procedure);

                    while (reader.Read())
                        empList.Add(new DataCachingEntity { EmpID = new Guid(reader["EmployeeID"].ToString()), Name = reader["Name"].ToString(), Address = reader["Address"].ToString(), EMail = reader["EMail"].ToString(), Phone = reader["Phone"].ToString() });

                    reader.Close();

                    AFCache.Caching.SaveCache<List<DataCachingEntity>>(empList, parameterList, 10);
                }
                catch (Exception ex)
                {
                    reader.Close();
                    throw ex;
                }
            }

            return empList;
        }
    }
}
