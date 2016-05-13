using System;
using System.Web;

namespace ZCKT.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Run();
        }
    }
}