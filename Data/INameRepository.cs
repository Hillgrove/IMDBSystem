using Data.Models;

namespace Data
{
    public interface INameRepository
    {
        List<Name> GetPersons(string name);
    }
}