using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.BuilderManager
{
    public interface IBuilderManager:IDisposable
    {
        Task Run();
    }
}
