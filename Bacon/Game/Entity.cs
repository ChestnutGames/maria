using Maria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Bacon.Game
{
    class Entity : Maria.Actor
    {
        protected uint _session = 0;
        protected uint _idx;

        public Entity(Context ctx, Controller controller)
            : base(ctx, controller, null) {
        }

        public Entity(Context ctx, Controller controller, GameObject go)
            : base(ctx, controller, go)
        {
        }


    }
}
