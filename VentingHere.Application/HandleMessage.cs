using System.Collections.Generic;
using VentingHere.Application.Enum;
using VentingHere.Application.Interface;

namespace VentingHere.Application
{
    public class HandleMessage : IHandleMessage
    {
        public Dictionary<HandleMessageType, string> Add(HandleMessageType messageType, string message)
        {
            var result = new Dictionary<HandleMessageType, string>();

            result.Add(messageType, message);

            return result;
        }
    }
}
