using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Enum;

namespace VentingHere.Application.Interface
{
    public interface IHandleMessage
    {
        Dictionary<HandleMessageType, string> Add(HandleMessageType messageType, string message);
    }
}
