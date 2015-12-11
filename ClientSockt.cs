using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sproto;
using SprotoType;

public class ClientSockt : MonoBehaviour {

    Client mClient = new Client();

	// Use this for initialization
	void Start () {

        AddressBook address = new AddressBook();
        address.person = new System.Collections.Generic.List<Person>();
        Person person = new Person();
        person.name = "Alice";
        person.id = 10000;
        person.phone = new System.Collections.Generic.List<Person.PhoneNumber>();
        Person.PhoneNumber num1 = new Person.PhoneNumber();
        num1.number = "1234567899";
        num1.type = 1;
        person.phone.Add(num1);
        address.person.Add(person);

        byte[] data = address.encode();  // encode to bytes
        Sproto.SprotoStream stream = new SprotoStream();
        address.encode(stream);          // encode t0 stream

        Sproto.SprotoPack spack = new SprotoPack();
        byte[] pack_data = spack.pack(data);

        byte[] unpack_data = spack.unpack(pack_data);
        AddressBook obj = new AddressBook(unpack_data);

        var t = GameObject.Find("Text");
        var text = t.GetComponent<Text>();
        text.text = address.person[0].name;

        mClient.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
