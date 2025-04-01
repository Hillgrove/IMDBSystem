using Data.Models;

namespace Data
{
    public interface ITitleRepository
    {
        void AddTitle(Title title);
        List<Genre> GetGenres();
        List<Title> GetTitles(string title);
        List<IMDBType> GetTypes();
        void UpdateTitle(Title original, Title updated);
        void DeleteTitle(Title title);
    }
}