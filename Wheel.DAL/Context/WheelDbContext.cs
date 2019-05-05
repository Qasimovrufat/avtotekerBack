using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Wheel.DAL.Models;

namespace Wheel.DAL.Context
{
    public class WheelDbContext:IdentityDbContext<UserApp>
    {
        public WheelDbContext():base("Wheel")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // Database.SetInitializer(new WheelDbContextIntialaizer());
            base.OnModelCreating(modelBuilder);
        }

        //public class WheelDbContextIntialaizer : DropCreateDatabaseIfModelChanges<WheelDbContext>
        //{
        //    protected override void Seed(WheelDbContext context)
        //    {
        //        base.Seed(context);
        //    }
        //}
    }
}