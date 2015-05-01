using System;
using System.Threading.Tasks;

namespace MachineKeyGenerator.Web
{
    public class ElmahLogger : ILogger
    {
        public void LogException(Exception exception)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
        }

        public async Task LogExceptionAsync(Exception exception)
        {
            await Task.Factory.StartNew(() =>
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            });
        }
    }
}