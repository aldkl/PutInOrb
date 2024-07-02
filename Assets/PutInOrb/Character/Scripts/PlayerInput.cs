using UnityEngine;

namespace PutInOrb
{
    public class PlayerInput : MonoBehaviour
    {
        public string moveAxisName = "Vertical";
        public string rotateAxisName = "Horizontal";
        public string chatchButtonName = "ChatchOrb";
        public string putButtonName = "PutOrb";

        public JoystickManager joystick;

        public float move { get; private set; }
        public float rotate { get; private set; }
        public bool chatch { get; private set; }
        public bool put { get; private set; }


        void Update()
        {
            if (GameManager.instance != null && GameManager.instance.isGameover)
            {
                move = 0;
                rotate = 0;
                chatch = false; put = false;
                return;
            }

            if (joystick != null)
            {
                move = joystick.Vertical;
                rotate = joystick.Horizontal;
                chatch = joystick.chatch;
                put = joystick.put;
            }
            else
            {
                move = Input.GetAxis(moveAxisName);
                rotate = Input.GetAxis(rotateAxisName);
                chatch = Input.GetButtonDown(chatchButtonName);
                put = Input.GetButtonDown(putButtonName);
            }
        }

        void Start()
        {

        }
    }
}