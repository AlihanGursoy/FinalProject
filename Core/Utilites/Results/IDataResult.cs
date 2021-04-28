using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public interface IDataResult<T>:IResults
    {
        T Data { get; }
    }
}
