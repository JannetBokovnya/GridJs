using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentInformationJS.Startup))]
namespace StudentInformationJS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
