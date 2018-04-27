namespace Configucius.Core
{
    public interface IConfigucius
    {
        T GetValue<T>(string key);
    }
}