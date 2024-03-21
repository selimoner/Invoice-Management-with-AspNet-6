using testProject.Models;

namespace testProject.Repositories.ItemRepository
{
    public interface IItemCollection
    {
        void InsertItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(string id);
        List<Item> GetAll();
        Item GetItemById(string id);
    }
}
