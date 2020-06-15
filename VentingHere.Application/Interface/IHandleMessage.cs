using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Enum;

namespace VentingHere.Application.Interface
{
    public interface IHandleMessage<TEntity> where TEntity : class
    {
        Tuple<HandleMessageType, string, TEntity> Add(HandleMessageType messageType, string message, TEntity entity);
    }
}
