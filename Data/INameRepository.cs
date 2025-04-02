using Data.Models;

namespace Data
{
    public interface INameRepository
    {
        List<Name> GetNames(string name, int offset, int pageSize);
        void AddName(Name name);
    }
}