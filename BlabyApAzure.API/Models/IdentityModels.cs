using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using DocumentDB.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using Blabyap.Common.Model;

namespace BlabyApAzure.API.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
       [Required]
        public string FullName { get; set; }
       [Required]
        public string DisplayName { get; set; }

       
        //[Required]
        //[DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

       //  [Required]
        public string ImageUrl { get; set; }

        //[Required]
        public bool HideMe { get; set; }

        //[Required] 
        public bool HideMyAge { get; set; }

        public CVData CVInfo { get; set; }

        public string ChatConnectionID { get; set; }

        public DiscoverInfo DiscoverFilter { get; set; }

        public int NumComments { get; set; }

        public int NumMatches{ get; set; }

        //  [Required]
        [Display(Name = "NumRightSwipes")]
        public int NumRightSwipes { get; set; }

        //  [Required]
        [Display(Name = "NumLeftSwipes")]
        public int NumLeftSwipes { get; set; }


        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeletedDate { get; set; }

        public bool Deleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }
        
    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}