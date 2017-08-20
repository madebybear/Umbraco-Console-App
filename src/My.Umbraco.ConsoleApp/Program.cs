using System;
using System.IO;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace My.Umbraco.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // initialize the app
            var application = new ConsoleApplicationBase();
            application.Start(application, new EventArgs());

            // get references
            var context = ApplicationContext.Current;
            var serviceContext = context.Services;
            var contentService = serviceContext.ContentService;

            // get root content
            var rootContent = contentService.GetRootContent().FirstOrDefault();

            // list children
            if (rootContent != null)
            {
                Console.WriteLine($"ROOT : {rootContent.Name} : {rootContent.Id}");
                foreach (var child in rootContent.Children())
                {
                    Console.WriteLine($" - CHILD : {child.Name} : {child.Id}");
                }
            }

            // wait for user input
            Console.ReadLine();
        }
    }

    internal class ConsoleApplicationBase : UmbracoApplicationBase
    {
        protected override IBootManager GetBootManager()
        {
            var dataDirectory = new DirectoryInfo("..\\..\\..\\..\\src\\My.Umbraco.Website\\App_Data\\");

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory.FullName);

            return new ConsoleBootManager(this);
        }

        public void Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
        }
    }

    internal class ConsoleBootManager : CoreBootManager
    {
        public ConsoleBootManager(UmbracoApplicationBase umbracoApplication) : base(umbracoApplication)
        {
        }
    }
}
