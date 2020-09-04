using DanmakU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public enum PlayerState
    {
        Default = 0,
        SlowMove = 1
    }
    public class Player : MonoBehaviour
    {
        private PlayerInput playerInput;
        public float speed = 4.5f;
        private PlayerState State = PlayerState.Default;

        private int counter = 0;
        // Start is called before the first frame update
        void Start()
        {
            playerInput = new PlayerInput();
        }

        // Update is called once per frame
        void Update()
        {
            State = PlayerState.Default;
            float hInput = playerInput.HorizontalInput;
            float vInput = playerInput.VerticalInput;

            float horizontalMove = hInput * speed;
            float VerticalMove = vInput * speed;
            //Debug.LogFormat("{0} {1}", hInput, vInput);
            if (vInput * hInput != 0)
            {
                horizontalMove /= 1.414f; // Sqrt(2)
                VerticalMove /= 1.414f;
            }
            if (playerInput.SlowButton)
            {
                horizontalMove /= 2.5f;
                VerticalMove /= 3.0f;
                State = PlayerState.SlowMove;
            }
            Vector2 playerPos = (Vector2)transform.position;
            Vector2 tempPos = (Vector2)transform.position +
                new Vector2(horizontalMove, VerticalMove) * Time.deltaTime;
            if (!(tempPos.x < -Setting.WidthF || tempPos.x > Setting.WidthF))
            {
                playerPos.x += horizontalMove * Time.deltaTime;
            }
            if (!(tempPos.y < -Setting.HeightF || tempPos.y > Setting.HeightF))
            {
                playerPos.y += VerticalMove * Time.deltaTime;
            }
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);


            if (playerInput.ShotButton)
            {
                //Debug.Log("Shot");
                if (counter % 2 == 0)
                {
                    Debug.Log("shot "+ counter);
                }

            }
            counter++;
        }

    }

}
