// Generated by sprotodump. DO NOT EDIT!
// source: ./../tool/s2c.sproto

using System;
using Sproto;
using System.Collections.Generic;

namespace S2cSprotoType { 
	public class achi : SprotoTypeBase {
		private static int max_field_count = 2;
		
		
		private Int64 _csv_id; // tag 0
		public Int64 csv_id {
			get { return _csv_id; }
			set { base.has_field.set_field (0, true); _csv_id = value; }
		}
		public bool HasCsv_id {
			get { return base.has_field.has_field (0); }
		}

		private Int64 _finished; // tag 1
		public Int64 finished {
			get { return _finished; }
			set { base.has_field.set_field (1, true); _finished = value; }
		}
		public bool HasFinished {
			get { return base.has_field.has_field (1); }
		}

		public achi () : base(max_field_count) {}

		public achi (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 0:
					this.csv_id = base.deserialize.read_integer ();
					break;
				case 1:
					this.finished = base.deserialize.read_integer ();
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
				base.serialize.write_integer (this.csv_id, 0);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_integer (this.finished, 1);
			}

			return base.serialize.close ();
		}
	}


	public class finish_achi {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private achi _which; // tag 0
			public achi which {
				get { return _which; }
				set { base.has_field.set_field (0, true); _which = value; }
			}
			public bool HasWhich {
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
						this.which = base.deserialize.read_obj<achi> ();
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
					base.serialize.write_obj (this.which, 0);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			private string _msg; // tag 1
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (1, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (1); }
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
						this.msg = base.deserialize.read_string ();
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
					base.serialize.write_string (this.msg, 1);
				}

				return base.serialize.close ();
			}
		}


	}


	public class lilian_update {
	
		public class response : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			private string _msg; // tag 1
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (1, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (1); }
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
						this.msg = base.deserialize.read_string ();
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
					base.serialize.write_string (this.msg, 1);
				}

				return base.serialize.close ();
			}
		}


	}


	public class mail {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 4;
			
			
			private Int64 _from; // tag 0
			public Int64 from {
				get { return _from; }
				set { base.has_field.set_field (0, true); _from = value; }
			}
			public bool HasFrom {
				get { return base.has_field.has_field (0); }
			}

			private Int64 _to; // tag 1
			public Int64 to {
				get { return _to; }
				set { base.has_field.set_field (1, true); _to = value; }
			}
			public bool HasTo {
				get { return base.has_field.has_field (1); }
			}

			private string _head; // tag 2
			public string head {
				get { return _head; }
				set { base.has_field.set_field (2, true); _head = value; }
			}
			public bool HasHead {
				get { return base.has_field.has_field (2); }
			}

			private string _msg; // tag 3
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (3, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (3); }
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
						this.from = base.deserialize.read_integer ();
						break;
					case 1:
						this.to = base.deserialize.read_integer ();
						break;
					case 2:
						this.head = base.deserialize.read_string ();
						break;
					case 3:
						this.msg = base.deserialize.read_string ();
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
					base.serialize.write_integer (this.from, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_integer (this.to, 1);
				}

				if (base.has_field.has_field (2)) {
					base.serialize.write_string (this.head, 2);
				}

				if (base.has_field.has_field (3)) {
					base.serialize.write_string (this.msg, 3);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private Int64 _errorcode; // tag 0
			public Int64 errorcode {
				get { return _errorcode; }
				set { base.has_field.set_field (0, true); _errorcode = value; }
			}
			public bool HasErrorcode {
				get { return base.has_field.has_field (0); }
			}

			private string _msg; // tag 1
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (1, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (1); }
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
						this.msg = base.deserialize.read_string ();
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
					base.serialize.write_string (this.msg, 1);
				}

				return base.serialize.close ();
			}
		}


	}


	public class package : SprotoTypeBase {
		private static int max_field_count = 2;
		
		
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

			return base.serialize.close ();
		}
	}


}

