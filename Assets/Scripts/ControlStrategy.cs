using UnityEngine;

namespace LeikirTest
{
    public abstract class ControlStrategy : ScriptableObject
    {
        public float m_rotationSpeed = 10;
        public float m_maxSpeed = 7;

        public abstract Vector3 ReadInputMovement();
        public abstract void Init(LeikirTest.CharacterController _controller);

        public float MaxSpeed
        {
            get
            {
                return m_maxSpeed;
            }
        }
        public float RotationSpeed
        {
            get
            {
                return m_rotationSpeed;
            }
        }
    }
}