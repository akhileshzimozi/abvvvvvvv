namespace Blabyapp.API.Migrations
{
    using Blabyapp.API.DataModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blabyapp.API.DataModels.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Blabyapp.API.DataModels.ApplicationDbContext context)
        {
            #region Demo Data input
            //to add demo data or meta data
            //var look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "Leadership",
            //    Code = "Leadership"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            // {
            //     LookupId = Guid.NewGuid().ToString(),
            //     Family = "SkillSet",
            //     FamilyType = "Expertise",
            //    Translation = "Marketing",
            //    Code = "Marketing"
            // };
            //context.Lookups.AddOrUpdate(look);
            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "AI",
            //    Code = "AI"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "Startups",
            //    Code = "Startups"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "IoT",
            //    Code = "IoT"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "Blockchain",
            //    Code = "Blockchain"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "Education",
            //    Code = "Skills"
            //};
            //context.Lookups.AddOrUpdate(look);

            //look = new Lookup
            //{
            //    LookupId = Guid.NewGuid().ToString(),
            //    Family = "SkillSet",
            //    FamilyType = "Expertise",
            //    Translation = "Finance",
            //    Code = "Skills"
            //};
            //context.Lookups.AddOrUpdate(look);

            //  var isExistCount = context.Expertises.Where(m => m.ExperAreaID == someobj.ExperAreaID).ToList().Count;

            //  if(isExistCount<=0)
            //context.ExpertiseArea.AddOrUpdate(someobj);

            var primarykey = Guid.NewGuid().ToString(); 
            #endregion


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
