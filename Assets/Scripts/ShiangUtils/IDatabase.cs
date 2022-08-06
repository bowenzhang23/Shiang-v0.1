
namespace Shiang
{
    public interface IDBData
    {

    }

    public interface IDatabase
    {
        public void Create();

        public void Insert(object entry);
        
        public void Retrieve();

        public void Clear();

        public object Data { get; }

    }
}
