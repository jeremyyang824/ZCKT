using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCKT.Infrastructure
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private readonly DBHelperProvider dbHelperProvider;

        public UnitOfWorkManager(DBHelperProvider dbHelperProvider)
        {
            this.dbHelperProvider = dbHelperProvider;
        }

        public IUnitOfWork Begin(UnitOfWorkOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            IUnitOfWork uow = new DefaultUnitOfWork(options, this.dbHelperProvider.DBContext);
            return uow;
        }

        public IUnitOfWork Begin()
        {
            return this.Begin(UnitOfWorkOptions.Default);
        }
    }
}
