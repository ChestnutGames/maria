// Generated by sprotodump. DO NOT EDIT!
// source: ./../../../tool/c2s.sproto

using System;
using Sproto;
using System.Collections.Generic;

namespace C2sSprotoType { 
	public class battleinitdata : SprotoTypeBase {
		private static int max_field_count = 8;
		
		
		private Int64 _userid; // tag 0
		public Int64 userid {
			get { return _userid; }
			set { base.has_field.set_field (0, true); _userid = value; }
		}
		public bool HasUserid {
			get { return base.has_field.has_field (0); }
		}

		private Int64 _carid; // tag 1
		public Int64 carid {
			get { return _carid; }
			set { base.has_field.set_field (1, true); _carid = value; }
		}
		public bool HasCarid {
			get { return base.has_field.has_field (1); }
		}

		private bool _ai; // tag 2
		public bool ai {
			get { return _ai; }
			set { base.has_field.set_field (2, true); _ai = value; }
		}
		public bool HasAi {
			get { return base.has_field.has_field (2); }
		}

		private string _name; // tag 3
		public string name {
			get { return _name; }
			set { base.has_field.set_field (3, true); _name = value; }
		}
		public bool HasName {
			get { return base.has_field.has_field (3); }
		}

		private Int64 _x; // tag 4
		public Int64 x {
			get { return _x; }
			set { base.has_field.set_field (4, true); _x = value; }
		}
		public bool HasX {
			get { return base.has_field.has_field (4); }
		}

		private Int64 _y; // tag 5
		public Int64 y {
			get { return _y; }
			set { base.has_field.set_field (5, true); _y = value; }
		}
		public bool HasY {
			get { return base.has_field.has_field (5); }
		}

		private Int64 _z; // tag 6
		public Int64 z {
			get { return _z; }
			set { base.has_field.set_field (6, true); _z = value; }
		}
		public bool HasZ {
			get { return base.has_field.has_field (6); }
		}

		private Int64 _ori; // tag 7
		public Int64 ori {
			get { return _ori; }
			set { base.has_field.set_field (7, true); _ori = value; }
		}
		public bool HasOri {
			get { return base.has_field.has_field (7); }
		}

		public battleinitdata () : base(max_field_count) {}

		public battleinitdata (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 0:
					this.userid = base.deserialize.read_integer ();
					break;
				case 1:
					this.carid = base.deserialize.read_integer ();
					break;
				case 2:
					this.ai = base.deserialize.read_boolean ();
					break;
				case 3:
					this.name = base.deserialize.read_string ();
					break;
				case 4:
					this.x = base.deserialize.read_integer ();
					break;
				case 5:
					this.y = base.deserialize.read_integer ();
					break;
				case 6:
					this.z = base.deserialize.read_integer ();
					break;
				case 7:
					this.ori = base.deserialize.read_integer ();
					break;
				default:
					base.deserialize.read_unknow_data ();
					break;
				}
			}
		}

		public override int encode (SprotoStream stream) {
			base.serialize.open (stream);

			if (base.has_field.has_field (0)) {
				base.serialize.write_integer (this.userid, 0);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_integer (this.carid, 1);
			}

			if (base.has_field.has_field (2)) {
				base.serialize.write_boolean (this.ai, 2);
			}

			if (base.has_field.has_field (3)) {
				base.serialize.write_string (this.name, 3);
			}

			if (base.has_field.has_field (4)) {
				base.serialize.write_integer (this.x, 4);
			}

			if (base.has_field.has_field (5)) {
				base.serialize.write_integer (this.y, 5);
			}

			if (base.has_field.has_field (6)) {
				base.serialize.write_integer (this.z, 6);
			}

			if (base.has_field.has_field (7)) {
				base.serialize.write_integer (this.ori, 7);
			}

			return base.serialize.close ();
		}
	}


	public class createbuff {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private Int64 _userid; // tag 0
			public Int64 userid {
				get { return _userid; }
				set { base.has_field.set_field (0, true); _userid = value; }
			}
			public bool HasUserid {
				get { return base.has_field.has_field (0); }
			}

			private Int64 _buff_id; // tag 1
			public Int64 buff_id {
				get { return _buff_id; }
				set { base.has_field.set_field (1, true); _buff_id = value; }
			}
			public bool HasBuff_id {
				get { return base.has_field.has_field (1); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.userid = base.deserialize.read_integer ();
						break;
					case 1:
						this.buff_id = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.userid, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_integer (this.buff_id, 1);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class die {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _userid; // tag 0
			public Int64 userid {
				get { return _userid; }
				set { base.has_field.set_field (0, true); _userid = value; }
			}
			public bool HasUserid {
				get { return base.has_field.has_field (0); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.userid = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.userid, 0);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class eitbloodentity {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private Int64 _userid; // tag 0
			public Int64 userid {
				get { return _userid; }
				set { base.has_field.set_field (0, true); _userid = value; }
			}
			public bool HasUserid {
				get { return base.has_field.has_field (0); }
			}

			private Int64 _id; // tag 1
			public Int64 id {
				get { return _id; }
				set { base.has_field.set_field (1, true); _id = value; }
			}
			public bool HasId {
				get { return base.has_field.has_field (1); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.userid = base.deserialize.read_integer ();
						break;
					case 1:
						this.id = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.userid, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_integer (this.id, 1);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class enter_room {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _type; // tag 0
			public Int64 type {
				get { return _type; }
				set { base.has_field.set_field (0, true); _type = value; }
			}
			public bool HasType {
				get { return base.has_field.has_field (0); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.type = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.type, 0);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class exitroom {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _userid; // tag 0
			public Int64 userid {
				get { return _userid; }
				set { base.has_field.set_field (0, true); _userid = value; }
			}
			public bool HasUserid {
				get { return base.has_field.has_field (0); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.userid = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.userid, 0);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class joinroom {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _roomid; // tag 0
			public Int64 roomid {
				get { return _roomid; }
				set { base.has_field.set_field (0, true); _roomid = value; }
			}
			public bool HasRoomid {
				get { return base.has_field.has_field (0); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.roomid = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.roomid, 0);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 5;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			private List<battleinitdata> _battleinitdatalst; // tag 1
			public List<battleinitdata> battleinitdatalst {
				get { return _battleinitdatalst; }
				set { base.has_field.set_field (1, true); _battleinitdatalst = value; }
			}
			public bool HasBattleinitdatalst {
				get { return base.has_field.has_field (1); }
			}

			private Int64 _session; // tag 2
			public Int64 session {
				get { return _session; }
				set { base.has_field.set_field (2, true); _session = value; }
			}
			public bool HasSession {
				get { return base.has_field.has_field (2); }
			}

			private string _ip; // tag 3
			public string ip {
				get { return _ip; }
				set { base.has_field.set_field (3, true); _ip = value; }
			}
			public bool HasIp {
				get { return base.has_field.has_field (3); }
			}

			private Int64 _port; // tag 4
			public Int64 port {
				get { return _port; }
				set { base.has_field.set_field (4, true); _port = value; }
			}
			public bool HasPort {
				get { return base.has_field.has_field (4); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					case 1:
						this.battleinitdatalst = base.deserialize.read_obj_list<battleinitdata> ();
						break;
					case 2:
						this.session = base.deserialize.read_integer ();
						break;
					case 3:
						this.ip = base.deserialize.read_string ();
						break;
					case 4:
						this.port = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_obj (this.battleinitdatalst, 1);
				}

				if (base.has_field.has_field (2)) {
					base.serialize.write_integer (this.session, 2);
				}

				if (base.has_field.has_field (3)) {
					base.serialize.write_string (this.ip, 3);
				}

				if (base.has_field.has_field (4)) {
					base.serialize.write_integer (this.port, 4);
				}

				return base.serialize.close ();
			}
		}


	}


	public class package : SprotoTypeBase {
		private static int max_field_count = 4;
		
		
		private Int64 _type; // tag 0
		public Int64 type {
			get { return _type; }
			set { base.has_field.set_field (0, true); _type = value; }
		}
		public bool HasType {
			get { return base.has_field.has_field (0); }
		}

		private Int64 _session; // tag 1
		public Int64 session {
			get { return _session; }
			set { base.has_field.set_field (1, true); _session = value; }
		}
		public bool HasSession {
			get { return base.has_field.has_field (1); }
		}

		private Int64 _index; // tag 2
		public Int64 index {
			get { return _index; }
			set { base.has_field.set_field (2, true); _index = value; }
		}
		public bool HasIndex {
			get { return base.has_field.has_field (2); }
		}

		private Int64 _version; // tag 3
		public Int64 version {
			get { return _version; }
			set { base.has_field.set_field (3, true); _version = value; }
		}
		public bool HasVersion {
			get { return base.has_field.has_field (3); }
		}

		public package () : base(max_field_count) {}

		public package (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 0:
					this.type = base.deserialize.read_integer ();
					break;
				case 1:
					this.session = base.deserialize.read_integer ();
					break;
				case 2:
					this.index = base.deserialize.read_integer ();
					break;
				case 3:
					this.version = base.deserialize.read_integer ();
					break;
				default:
					base.deserialize.read_unknow_data ();
					break;
				}
			}
		}

		public override int encode (SprotoStream stream) {
			base.serialize.open (stream);

			if (base.has_field.has_field (0)) {
				base.serialize.write_integer (this.type, 0);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_integer (this.session, 1);
			}

			if (base.has_field.has_field (2)) {
				base.serialize.write_integer (this.index, 2);
			}

			if (base.has_field.has_field (3)) {
				base.serialize.write_integer (this.version, 3);
			}

			return base.serialize.close ();
		}
	}


	public class ping {
	
		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class updateblood {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 3;
			
			
			private Int64 _sourceuserid; // tag 0
			public Int64 sourceuserid {
				get { return _sourceuserid; }
				set { base.has_field.set_field (0, true); _sourceuserid = value; }
			}
			public bool HasSourceuserid {
				get { return base.has_field.has_field (0); }
			}

			private Int64 _targetuserid; // tag 1
			public Int64 targetuserid {
				get { return _targetuserid; }
				set { base.has_field.set_field (1, true); _targetuserid = value; }
			}
			public bool HasTargetuserid {
				get { return base.has_field.has_field (1); }
			}

			private Int64 _bloodvalue; // tag 2
			public Int64 bloodvalue {
				get { return _bloodvalue; }
				set { base.has_field.set_field (2, true); _bloodvalue = value; }
			}
			public bool HasBloodvalue {
				get { return base.has_field.has_field (2); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.sourceuserid = base.deserialize.read_integer ();
						break;
					case 1:
						this.targetuserid = base.deserialize.read_integer ();
						break;
					case 2:
						this.bloodvalue = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.sourceuserid, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_integer (this.targetuserid, 1);
				}

				if (base.has_field.has_field (2)) {
					base.serialize.write_integer (this.bloodvalue, 2);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.errorcode = base.deserialize.read_integer ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_integer (this.errorcode, 0);
				}

				return base.serialize.close ();
			}
		}


	}


}

