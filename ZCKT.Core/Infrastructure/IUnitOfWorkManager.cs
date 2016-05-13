using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCKT.Infrastructure
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Begin(UnitOfWorkOptions options);
    }
}
