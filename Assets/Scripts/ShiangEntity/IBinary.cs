
namespace Shiang
{
    public interface IBinary
    {
        public void Open();

        public void Close();

        public bool IsOpen { get; set; }
    }
}
