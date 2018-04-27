using System.Collections.Generic;

namespace Configucius.Core
{
    public interface IConfigRepository
    {
        IEnumerable<Config> GetValues(string domain, string environment);
    }
}