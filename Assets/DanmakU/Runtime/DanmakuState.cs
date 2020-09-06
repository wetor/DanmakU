using System;
using UnityEngine;

namespace DanmakU
{

    /// <summary>
    /// A config for creating <see cref="DanmakU.DanmakuState"/>.
    /// </summary>
    [Serializable]
    public struct DanmakuConfig
    {

        public Vector2 Position;
        public Vector2 Displacement;
        [Radians] public Range Rotation;
        [Radians] public Range Angle;
        public Range Speed;
        [Radians] public Range AngularSpeed;
        public Color Color;

        /// <summary>
        /// Creates an state based on the config.
        /// </summary>
        /// <returns>a sampled state from the config's state space.</returns>
        public DanmakuState CreateState()
        {
            return new DanmakuState
            {
                Position = Position,
                Displacement = Displacement,
                Rotation = Rotation.GetValue(),
                Angle = Angle.GetValue(),
                Speed = Speed.GetValue(),
                AngularSpeed = AngularSpeed.GetValue(),
                Color = Color
            };
        }

    }

    /// <summary>
    /// A snapshot of a <see cref="DanmakU.Danmaku"/>'s state.
    /// </summary>
    [Serializable]
    public struct DanmakuState
    {
        public Vector2 Position;
        public Vector2 Displacement;
        [Radians] public float Rotation;
        [Radians] public float Angle;
        public float Speed;
        [Radians] public float AngularSpeed;
        public Color Color;
    }

}