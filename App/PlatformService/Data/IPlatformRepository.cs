using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        IEnumerable<Platform> GetAll();
        Platform GetById(int id);
        void Create(Platform platform);
        bool SaveChanges();
    }
}
