using Data.Models;

namespace Data
{
    public interface INameRepository
    {
        List<Name> GetNames(string name);
        void AddName(Name name);
    }
}