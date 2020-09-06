using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanmakU.Emitters
{

    public abstract class Emitter : DanmakuBehaviour, IEmitter
    {
        public abstract void Init();
    }

}
