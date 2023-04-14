using AFDataCaching;
using SampleApplication.Data.DataCachingDemo;

namespace AF.DataAccessor.Sample
{
    public sealed class OperationManager
    {
        public void AddEmployee()
        {
            Console.Clear();
            IDataCachingDemoDAL dataCachingDemoDAL = new DataCachingDemoDAL();
            DataCachingEntity dataCachingEntity = new DataCachingEntity();
            dataCachingEntity.EmpID = Guid.NewGuid();

            Console.WriteLine("Enter Name:");
            dataCachingEntity.Name = Console.ReadLine();

            Console.WriteLine("Enter Address:");
            dataCachingEntity.Address = Console.ReadLine();

            Console.WriteLine("Enter EMail:");
            dataCachingEntity.EMail = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            dataCachingEntity.Phone = Console.ReadLine();

            var result = dataCachingDemoDAL.AddEmployee(dataCachingEntity);

            if (result.IsValid)
            {
                Console.WriteLine(result.Message[0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void UpdateEmployee()
        {
            Console.Clear();
            IDataCachingDemoDAL dataCachingDemoDAL = new DataCachingDemoDAL();
            DataCachingEntity dataCachingEntity = new DataCachingEntity();

            Console.WriteLine("Enter employee ID:");
            var empID = Console.ReadLine();
            dataCachingEntity.EmpID = empID == null ? Guid.Empty : Guid.Parse(empID);

            Console.WriteLine("Enter Address:");
            dataCachingEntity.Address = Console.ReadLine();

            Console.WriteLine("Enter EMail:");
            dataCachingEntity.EMail = Console.ReadLine();

            Console.WriteLine("Enter Phone:");
            dataCachingEntity.Phone = Console.ReadLine();

            var result = dataCachingDemoDAL.UpdateEmployee(dataCachingEntity);

            if (result.IsValid)
            {
                Console.WriteLine(result.Message[0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void DeleteEmployee()
        {

            Console.Clear();
            IDataCachingDemoDAL dataCachingDemoDAL = new DataCachingDemoDAL();
            DataCachingEntity dataCachingEntity = new DataCachingEntity();

            Console.WriteLine("Enter employee ID:");
            var empID = Console.ReadLine();
            dataCachingEntity.EmpID = empID == null ? Guid.Empty : Guid.Parse(empID);

            var result = dataCachingDemoDAL.DeleteEmployee(dataCachingEntity.EmpID);

            if (result.IsValid)
            {
                Console.WriteLine(result.Message[0]);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }

        public void ListEmployee()
        {
            Console.Clear();
            IDataCachingDemoDAL dataCachingDemoDAL = new DataCachingDemoDAL();
            DataCachingEntity dataCachingEntity = new DataCachingEntity();

            var empList = dataCachingDemoDAL.GetEmployeeList();

            if (empList.Count == 0)
                Console.WriteLine("No records found");

            foreach (var emp in empList)
            {
                Console.WriteLine("Employee ID: " + emp.EmpID);
                Console.WriteLine("Employee Name: " + emp.Name);
                Console.WriteLine("Employee Address:" + emp.Address);
                Console.WriteLine("Employee Email: " + emp.EMail);
                Console.WriteLine("Employee Phone: " + emp.Phone);

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
