using System;
using System.Collections.Generic;
using System.Text;

namespace Caliburn.Micro.Mobile.iOS.Bindings
{
    [Flags]
    public enum BindingDirection : ushort
    {
        Both = FromViewModel | ToViewModel,
        FromViewModel = 0x01,
        ToViewModel = 0x02,
    }
}
