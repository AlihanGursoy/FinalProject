using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public interface IResults
        //Temel voidler için başlangıç
    {
        bool Success { get; }
        string Message { get; }
    }
}
