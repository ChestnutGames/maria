﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maria {
    public class User {
        private Dictionary<string, Module> _modules = new Dictionary<string, Module>();

        public string Server { get; set; }    // 没有啥用
        public string Username { set; get; }  // 没有啥用
        public string Password { set; get; }  // 没有啥用
        public int Uid { get; set; }
        public int Subid { set; get; }
        public byte[] Secret { set; get; }
        public Context Context { get; set; }
        public Dictionary<string, Module> Modules { get { return _modules; } }

        public T GetModule<T>() where T : Module
        {
            Type t = typeof(T);
            return _modules[t.FullName] as T;
        }

        public void AddModule<T>() where T : Module
        {
            Module o = Activator.CreateInstance(typeof(T), this) as T;
            string name = o.GetType().FullName;
            _modules[name] = o;
        }

        public void RemoveModule<T>() where T : Module
        {
            Type t = typeof(T);
            string name = t.FullName;
            _modules.Remove(name);
        }

    }
}
