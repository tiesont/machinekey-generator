using System;
using System.Threading.Tasks;

namespace MachineKeyGenerator.Web
{
    public interface ILogger
    {
        void LogException(Exception exception);
        Task LogExceptionAsync(Exception exception);
    }
}
