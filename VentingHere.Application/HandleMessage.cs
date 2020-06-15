using System;
using VentingHere.Application.Enum;
using VentingHere.Application.Interface;

namespace VentingHere.Application
{
    public class HandleMessage<TEntity> : IHandleMessage<TEntity> where TEntity : class
    {
        public Tuple<HandleMessageType, string, TEntity> Add(HandleMessageType messageType, string message, TEntity entity)
        {
            var result = Tuple.Create(messageType, message, entity);
            //new Tuple<HandleMessageType, string, TEntity>(messageType, message,entity);

            return result;
        }
    }
}
