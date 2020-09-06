using System;
using UnityEngine;

namespace DanmakU.Fireables
{
    /// <summary>
    /// 静态子弹，需要使用Modifier来更新子弹
    /// </summary>
    [Serializable]
    public class Static : Fireable
    {
        public override void Fire(DanmakuConfig state)
        {
            var currentState = state;
            currentState.Position = state.Position;
            currentState.Angle = state.Angle;
            currentState.Speed = 0;
            Subfire(currentState);
        }

    }

}