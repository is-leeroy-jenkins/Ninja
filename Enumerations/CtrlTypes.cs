
namespace Ninja.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum CtrlTypes : uint
    {
        CtrlCEvent = 0,

        CtrlBreakEvent,

        CtrlCloseEvent,

        CtrlLogoffEvent = 5,

        CtrlShutdownEvent
    }
}
