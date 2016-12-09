// Generated by sprotodump. DO NOT EDIT!
using System;
using Sproto;
using System.Collections.Generic;

public class C2sProtocol : ProtocolBase {
	public static  C2sProtocol Instance = new C2sProtocol();
	private C2sProtocol() {
		Protocol.SetProtocol<createbuff> (createbuff.Tag);
		Protocol.SetRequest<C2sSprotoType.createbuff.request> (createbuff.Tag);
		Protocol.SetResponse<C2sSprotoType.createbuff.response> (createbuff.Tag);

		Protocol.SetProtocol<die> (die.Tag);
		Protocol.SetRequest<C2sSprotoType.die.request> (die.Tag);
		Protocol.SetResponse<C2sSprotoType.die.response> (die.Tag);

		Protocol.SetProtocol<eitbloodentity> (eitbloodentity.Tag);
		Protocol.SetRequest<C2sSprotoType.eitbloodentity.request> (eitbloodentity.Tag);
		Protocol.SetResponse<C2sSprotoType.eitbloodentity.response> (eitbloodentity.Tag);

		Protocol.SetProtocol<enter_room> (enter_room.Tag);
		Protocol.SetRequest<C2sSprotoType.enter_room.request> (enter_room.Tag);
		Protocol.SetResponse<C2sSprotoType.enter_room.response> (enter_room.Tag);

		Protocol.SetProtocol<exitroom> (exitroom.Tag);
		Protocol.SetRequest<C2sSprotoType.exitroom.request> (exitroom.Tag);
		Protocol.SetResponse<C2sSprotoType.exitroom.response> (exitroom.Tag);

		Protocol.SetProtocol<joinroom> (joinroom.Tag);
		Protocol.SetRequest<C2sSprotoType.joinroom.request> (joinroom.Tag);
		Protocol.SetResponse<C2sSprotoType.joinroom.response> (joinroom.Tag);

		Protocol.SetProtocol<ping> (ping.Tag);
		Protocol.SetResponse<C2sSprotoType.ping.response> (ping.Tag);

		Protocol.SetProtocol<updateblood> (updateblood.Tag);
		Protocol.SetRequest<C2sSprotoType.updateblood.request> (updateblood.Tag);
		Protocol.SetResponse<C2sSprotoType.updateblood.response> (updateblood.Tag);

	}

	public class createbuff {
		public const int Tag = 3;
	}

	public class die {
		public const int Tag = 7;
	}

	public class eitbloodentity {
		public const int Tag = 6;
	}

	public class enter_room {
		public const int Tag = 8;
	}

	public class exitroom {
		public const int Tag = 5;
	}

	public class joinroom {
		public const int Tag = 1;
	}

	public class ping {
		public const int Tag = 2;
	}

	public class updateblood {
		public const int Tag = 4;
	}

}