using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBAL.Repository.IRepository;
using CMSBAL.Data;

namespace CMSBAL.Repository
{    
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DatabaseContext moDatabaseContext;
        public UnitOfWork(DatabaseContext foDatabaseContext)
        {
            moDatabaseContext = foDatabaseContext;
            DivisionRepository = new DivisionRepository(moDatabaseContext);
            ZoneRepository = new ZoneRepository(moDatabaseContext);
            DepartmentRepository = new DepartmentRepository(moDatabaseContext);
            DesignationRepository = new DesignationRepository(moDatabaseContext);
            UserRepository = new UserRepository(moDatabaseContext);
            DeskRepository = new DeskRepository(moDatabaseContext);
            StoreRepository = new StoreRepository(moDatabaseContext);
            RoomRepository = new RoomRepository(moDatabaseContext);
            AlmirahRepository = new AlmirahRepository(moDatabaseContext);
            ShelveRepository = new ShelveRepository(moDatabaseContext);
            FileRepository = new FileRepository(moDatabaseContext);
            IssueFileHistoryRepository = new IssueFileHistoreyRepository(moDatabaseContext);
            CaseRepository = new CaseRepository(moDatabaseContext);
            DashboardRepository = new DashboardRepository(moDatabaseContext);
        }

        public IDivisionRepository DivisionRepository {get;private set;}
        public IZoneRepository ZoneRepository{get;private set;}
        public IDepartmentRepository DepartmentRepository{get;private set;}
        public IDesignationRepository DesignationRepository{get;private set;}
        public IUserRepository UserRepository{get;private set;}
        public IDeskRepository DeskRepository{get;private set;}
        public IStoreRepository StoreRepository{get;private set;}
        public IRoomRepository RoomRepository{get;private set;}
        public IAlmirahRepository AlmirahRepository{get;private set;}
        public IShelveRepository ShelveRepository{get;private set;}
        public IFileRepository FileRepository{get;private set;}
        public IIssueFileHistoryRepository IssueFileHistoryRepository{get;private set;}
        public ICaseRepository CaseRepository {get;private set;}
        public IDashboardRepository DashboardRepository { get;private set;}

        public void Dispose()
        {
            moDatabaseContext.Dispose();
        }
        public void Save()
        {
            moDatabaseContext.SaveChanges();
        }
    }
}