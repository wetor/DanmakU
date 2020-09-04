using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class PlayerInput
    {

        private bool GoRight => Input.GetKey(KeyCode.RightArrow);

        private bool GoLeft => Input.GetKey(KeyCode.LeftArrow);

        private bool GoUp => Input.GetKey(KeyCode.UpArrow);

        private bool GoDown => Input.GetKey(KeyCode.DownArrow);

        public float VerticalInput
        {
            get
            {
                if (GoUp && GoDown) return 0;
                if (GoUp) return 1;
                if (GoDown) return -1;
                return 0;
            }
        }


        public float HorizontalInput
        {
            get
            {
                if (GoRight && GoLeft) return 0;
                if (GoRight) return 1;
                if (GoLeft) return -1;
                return 0;
            }
        }

        public bool ShotButton
        {
            get
            {
                return Input.GetKey(KeyCode.Z);
            }
        }

        public bool ShotButtonUp
        {
            get
            {
                return Input.GetKeyUp(KeyCode.Z);
            }
        }

        public bool ShotButtonDown
        {
            get
            {
                return Input.GetKeyDown(KeyCode.Z);
            }
        }

        public bool SlowButton
        {
            get
            {
                return Input.GetKey(KeyCode.LeftShift);
            }
        }

        public bool SlowButtonUp
        {
            get
            {
                return Input.GetKeyUp(KeyCode.LeftShift);
            }
        }

        public bool SlowButtonDown
        {
            get
            {
                return Input.GetKeyDown(KeyCode.LeftShift);
            }
        }

        public bool BombButton
        {
            get
            {
                return Input.GetKey(KeyCode.X);
            }
        }


        public bool BombButtonDown
        {
            get
            {
                return Input.GetKeyDown(KeyCode.X);
            }
        }

        public bool BombButtonUp
        {
            get
            {
                return Input.GetKeyUp(KeyCode.X);
            }
        }


        public void Update()
        {

        }
    }
}