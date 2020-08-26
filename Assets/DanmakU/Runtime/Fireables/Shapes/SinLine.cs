using System;
using UnityEngine;

namespace DanmakU.Fireables
{

    [Serializable]
    public class SinLine : Fireable
    {

        public Range Radius;
        private int frame = 0;
        private Vector2 Position;
        public SinLine(Vector2 position, Range radius)
        {
            Position = position;
            Radius = radius;
            frame = 0;
        }
        public override void Fire(DanmakuConfig state)
        {
            float radius = Radius.GetValue();
            var rotation = state.Rotation.GetValue();
            var currentState = state;
            var move = new Vector2();
            move.x = 0.1f;
            move.y = Mathf.Sin(Mathf.PI / 12 * frame) ;
            var angle = Mathf.Atan2(move.x, move.y);
            Position += radius * move;
            currentState.Position = state.Position + Position;
            currentState.Rotation = angle;
            currentState.Speed = 0;
            Subfire(currentState);
            frame++;


        }

    }

}