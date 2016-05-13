using ZCKT.DBHelpers;

namespace ZCKT.Infrastructure
{
    public class DBHelperProvider
    {
        public DBHelper DBContext { get; private set; }

        public DBHelperProvider(string connectionStringName)
        {
            this.DBContext = new DBHelper(connectionStringName);
        }
    }
}
