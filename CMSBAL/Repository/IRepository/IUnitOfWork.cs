using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBAL.Repository.IRepository;

namespace CMSBAL.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IDivisionRepository DivisionRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        IUserRepository UserRepository { get; }
        IDeskRepository DeskRepository { get; }
        IStoreRepository StoreRepository { get; }
        IRoomRepository RoomRepository { get; }
        IAlmirahRepository AlmirahRepository { get; }
        IShelveRepository ShelveRepository { get; }
        IFileRepository FileRepository { get; }
        IIssueFileHistoryRepository IssueFileHistoryRepository { get; }
        ICaseRepository CaseRepository { get; }
        IDashboardRepository DashboardRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Save();
    }
}