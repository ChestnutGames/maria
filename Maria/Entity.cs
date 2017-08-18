using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Maria {
    public class Entity : Actor {

        private int _idx;
        private Dictionary<string, Component> _components = new Dictionary<string, Component>();

        public Entity(Context ctx, Controller controller)
            : base(ctx, controller) {
        }

        public Entity(Context ctx, Controller controller, GameObject go)
            : base(ctx, controller, go)
        {
        }

        public T GetComponent<T>() where T : Component {
            Type t = typeof(T);
            return _components[t.FullName] as T;
        }

        public void AddComponent<T>() where T : Component {
            Component o = Activator.CreateInstance(typeof(T), this) as T;
            string name = o.GetType().FullName;
            _components[name] = o;
        }

        public void RemoveComponent<T>() where T : Component {
            Type t = typeof(T);
            string name = t.FullName;
            _components.Remove(name);
        }

    }
}
