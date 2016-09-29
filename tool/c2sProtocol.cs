// Generated by sprotodump. DO NOT EDIT!
using System;
using Sproto;
using System.Collections.Generic;

public class C2sProtocol : ProtocolBase {
	public static  C2sProtocol Instance = new C2sProtocol();
	private C2sProtocol() {
		Protocol.SetProtocol<handshake> (handshake.Tag);
		Protocol.SetResponse<C2sSprotoType.handshake.response> (handshake.Tag);

		Protocol.SetProtocol<join> (join.Tag);
		Protocol.SetRequest<C2sSprotoType.join.request> (join.Tag);
		Protocol.SetResponse<C2sSprotoType.join.response> (join.Tag);

	}

	public class handshake {
		public const int Tag = 1;
	}

	public class join {
		public const int Tag = 2;
	}

}