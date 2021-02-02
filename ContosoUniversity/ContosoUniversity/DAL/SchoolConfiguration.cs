using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ContosoUniversity.DAL
{
    //the entity framework automatically runs the code it finds in a class that inherits from the "DbConfiguration" class. By making a class that inherits
    //from "DbConfiguration", you can automatically run configuration tasks that you would normally do in the "Web.config" file.
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}