using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using BlabyApAzure.API.Models;
using System.Configuration;
using DocumentDB.AspNet.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace BlabyApAzure.API
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var client = new SendGridClient("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");

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Rattana.sharma@zimozi.co", "BlabyAp Team"),
                Subject = message.Subject,
                PlainTextContent = message.Body,
                HtmlContent =message.Body
            };
            msg.AddTo(new EmailAddress(message.Destination));
            var response = await client.SendEmailAsync(msg);
        }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var endPoint = ConfigurationManager.AppSettings["DocumentDbEndPoint"];
            var authKey =  ConfigurationManager.AppSettings["DocumentDbAuthKey"];
            var dbName =  ConfigurationManager.AppSettings["DocumentDbName"];
            var collectionName = ConfigurationManager.AppSettings["DocumentDbCollectionName"];
             var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new Uri(endPoint), authKey, dbName, collectionName));
          //  var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
