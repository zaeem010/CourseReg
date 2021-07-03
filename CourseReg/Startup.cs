using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseReg.Startup))]
namespace CourseReg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
