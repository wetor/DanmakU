using System;
using UnityEngine;

namespace DanmakU.Fireables
{

    [Serializable]
    public class SinLine : Fireable
    {

        private int frame = 0;
        public override void Fire(DanmakuConfig state)
        {
            var currentState = state;
            currentState.Position = state.Position;
            currentState.Rotation = 0;
            currentState.Speed = 0;
            Subfire(currentState);
            frame++;


        }

    }

}