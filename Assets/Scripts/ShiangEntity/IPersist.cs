
namespace Shiang
{
    public interface IPersist
    {
        Persister Persister { get; }

        public void Save();

        public void Load();
    }
}
