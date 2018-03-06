using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface IClientUi
    {
        void updateClientUi();  // Implement platform specific Update UI Thread
    }
}
