namespace Building
{
    public interface IUpgradable
    {
        void Upgrade();
        int Price { get; set; }
    }
}