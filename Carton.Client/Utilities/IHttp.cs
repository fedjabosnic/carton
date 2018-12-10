using System;
using System.Threading.Tasks;
using Carton.Client.Utilities;

namespace Carton.Client.Utilities
{
    public interface IHttp : IDisposable
    {
        Task<Response<TOut>> Post<TIn,TOut>(string address, TIn content);
        Task<Response<TOut>> Put<TIn,TOut>(string address, TIn content);
        Task<Response<TOut>> Get<TOut>(string address);
        Task<Response<T>> Delete<T>(string address);
    }
}
