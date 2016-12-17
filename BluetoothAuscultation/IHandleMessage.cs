using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtocalData.Protocol;

namespace BluetoothAuscultation
{
    public  interface IHandleMessage<M>  
        where M : CodeBase
    {
       void HandleMessage(M message);
    }
    public interface IStopStethoscope
    {
        void Stop();
    }
}
