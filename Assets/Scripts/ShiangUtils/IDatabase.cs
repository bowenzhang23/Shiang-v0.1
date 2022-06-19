
namespace Shiang
{
    public interface IDatabase
    {
        public void Create();

        public void Insert(object entry);
        
        public void Retrieve();

        public void Clear();

    }

    public interface IDBData 
    {
    
    }
}
