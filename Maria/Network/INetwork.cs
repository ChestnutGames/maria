using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maria.Network {
    public interface INetwork {
        void GateAuthed(int code);
        void GateDisconnected();

        void UdpAuthed(uint session);

    }
}
