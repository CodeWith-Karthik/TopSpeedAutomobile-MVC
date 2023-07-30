using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSpeed.Application.ApplicationConstants
{
    public class ApplicationConstant
    {
    }

    public static class CommonMessage
    {
        public static string RecordCreated = "Record Created Successfully";
        public static string RecordUpdated = "Record Updated Successfully";
        public static string RecordDeleted = "Record Deleted Successfully";
    }

    public static class CustomRole
    {
        public static string MasterAdmin = "MASTERADMIN";
        public static string Admin = "ADMIN";
        public static string Customer = "CUSTOMER";
    }
}
