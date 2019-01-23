using UnityEngine;

namespace LeikirTest
{
    [CreateAssetMenu(menuName = "Control Strategies/Gamepad Control", order = 1)]
    public class GamepadControl : ControlStrategy
    {
        public string m_verticalAxis = "Vertical";
        public string m_horizontalAxis = "Horizontal";

        public override void Init(LeikirTest.CharacterController _controller)
        {

        }

        public override Vector3 ReadInputMovement()
        {
            Transform cam = Camera.main.transform;
            Debug.Assert(cam != null, "No Main Camera");

            Vector2 joy = new Vector2(Input.GetAxis(m_horizontalAxis), Input.GetAxis(m_verticalAxis));
            float magnitude = Mathf.Clamp(joy.magnitude, 0, 1);
            joy.Normalize();
            joy *= magnitude * MaxSpeed;

            Vector3 camForward = cam.forward;
            camForward.y = 0;
            //Debug.Assert(camForward.sqrMagnitude != 0);   // With the hack'n'slash view we assume we don't need this
            camForward.Normalize();

            return joy.y * camForward
                 + joy.x * cam.right;
        }

    }
}