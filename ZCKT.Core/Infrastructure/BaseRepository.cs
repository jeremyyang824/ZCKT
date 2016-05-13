using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZCKT.DBHelpers;
using ZCKT.Entities;

namespace ZCKT.Infrastructure
{
    public class BaseRepository
    {
        private readonly DBHelperProvider dbHelperProvider;

        public DBHelper DBContext
        {
            get { return this.dbHelperProvider.DBContext; }
        }

        public BaseRepository(DBHelperProvider dbHelperProvider)
        {
            this.dbHelperProvider = dbHelperProvider;
        }
    }
}
