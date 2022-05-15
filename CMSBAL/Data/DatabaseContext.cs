using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMSBAL.Zone.Models;
using CMSBAL.Division.Models;
using CMSUtility.Models;
using CMSBAL.Room.Models;
using CMSBAL.Almirah.Models;
using CMSBAL.Shelve.Models;
using CMSBAL.User.Models;
using CMSBAL.FIle.Models;
using CMSBAL.IssueFIleHistory.Models;
using CMSBAL.Case.Models;
using CMSBAL.Trace.Models;

namespace CMSBAL.Data
{
    public class DatabaseContext:IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>Options):base(Options)
        {

        }
        public DbSet<Zone.Models.Zone> Zones { get; set; }
        public DbSet<ZoneListResult> ZoneListResult { get; set; }
        public DbSet<Division.Models.Division> Division { get; set; }
        public DbSet<DivisionListResult> DivisionListResult { get; set; }
        public DbSet<Department.Models.Department> Department { get; set; }
        public DbSet<Department.Models.DepartmentListResult> DepartmentListResult { get; set; }
        public DbSet<Designation.Models.Designation> Designation { get; set; }
        public DbSet<Designation.Models.DesignationListResult> DesignationListResult { get; set; }
        public DbSet<User.Models.UserEmailResult> UserEmailResult { get; set; }
        public DbSet<Desk.Models.Desk> Desk { get; set; }
        public DbSet<Desk.Models.DeskListResult> DeskListResult { get; set; }
        public DbSet<Store.Models.Store> Store { get; set; }
        public DbSet<Store.Models.StoreListResult> StoreListResult { get; set; }
        public DbSet<Room.Models.Room> Room { get; set; }
        public DbSet<RoomListResult> RoomListResult { get; set; }
        public DbSet<Almirah.Models.Almirah> Almirah { get; set; }
        public DbSet<AlmirahListResult> AlmirahListResult { get; set; }
        public DbSet<Shelve.Models.Shelve> Shelve { get; set; }
        public DbSet<ShelveListResult> ShelveListResult { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserListResult> UserListResult { get; set; }
        public DbSet<FileListResult> FileListResult{ get; set; }
        public DbSet<FileDetail> FileDetail { get; set; }
        public DbSet<IssueFile> IssueFile { get; set; }
        public DbSet<IssueFileListResult> IssueFileListResult { get; set; }
        public DbSet<GetAssignFileDetailResult> GetAssignFileDetailResult { get; set; }
        public DbSet<Select2> Select2 { get; set; }
        public DbSet<CaseListResult> CaseListResult { get; set; }
        public DbSet<CaseDetailResult> CaseDetailResult { get; set; }
        public DbSet<UserDropDownDetailResult> UserDropDownDetailResult { get; set; }
        public DbSet<StoreFileDetailDropDownResult> StoreFileDetailDropDownResult { get; set; }
        public DbSet<TraceFileResults> TraceFileResults { get; set; }

        protected override void OnModelCreating(ModelBuilder foModelbuilder)
        {
            foModelbuilder.Entity<ZoneListResult>().HasNoKey();
            foModelbuilder.Entity<Zone.Models.Zone>().HasNoKey();
            foModelbuilder.Entity<Division.Models.Division>().HasNoKey();
            foModelbuilder.Entity<DivisionListResult>().HasNoKey();
            foModelbuilder.Entity<Department.Models.DepartmentListResult> ().HasNoKey();
            foModelbuilder.Entity<Department.Models.Department> ().HasNoKey();
            foModelbuilder.Entity<Designation.Models.Designation>().HasNoKey();
            foModelbuilder.Entity<Designation.Models.DesignationListResult>().HasNoKey();
            foModelbuilder.Entity<Select2>().HasNoKey();
            foModelbuilder.Entity<User.Models.UserEmailResult>().HasNoKey();
            foModelbuilder.Entity<Desk.Models.Desk>().HasNoKey();
            foModelbuilder.Entity<Desk.Models.DeskListResult>().HasNoKey();
            foModelbuilder.Entity<Store.Models.Store>().HasNoKey();
            foModelbuilder.Entity<Store.Models.StoreListResult>().HasNoKey();
            foModelbuilder.Entity<Room.Models.Room>().HasNoKey();
            foModelbuilder.Entity<RoomListResult>().HasNoKey();
            foModelbuilder.Entity<Almirah.Models.Almirah>().HasNoKey();
            foModelbuilder.Entity<AlmirahListResult>().HasNoKey();
            foModelbuilder.Entity<Shelve.Models.Shelve>().HasNoKey();
            foModelbuilder.Entity<ShelveListResult>().HasNoKey();
            foModelbuilder.Entity<UserProfile>().HasNoKey();
            foModelbuilder.Entity<UserListResult>().HasNoKey();
            foModelbuilder.Entity<FileListResult>().HasNoKey();
            foModelbuilder.Entity<FileDetail>().HasNoKey();
            foModelbuilder.Entity<IssueFileListResult>().HasNoKey();
            foModelbuilder.Entity<IssueFile>().HasNoKey();
            foModelbuilder.Entity<GetAssignFileDetailResult>().HasNoKey();
            foModelbuilder.Entity<CaseListResult>().HasNoKey();
            foModelbuilder.Entity<CaseDetailResult>().HasNoKey();
            foModelbuilder.Entity<UserDropDownDetailResult>().HasNoKey();
            foModelbuilder.Entity<StoreFileDetailDropDownResult>().HasNoKey();
            foModelbuilder.Entity<TraceFileResults>().HasNoKey();
            base.OnModelCreating(foModelbuilder);
        }
    }
    


}
