using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeikirTest
{
    [CreateAssetMenu(menuName = "Control Strategies/AI Control")]
    public class AIControl : ControlStrategy
    {
        public List<Vector3> m_path;
        public int m_currentTargetIdx = 0;
        public float m_distanceThreshold = 0.1f;

        private Transform m_controllerTransform;
        private Vector3 m_initialPosition;

        public override void Init(LeikirTest.CharacterController _controller)
        {
            m_controllerTransform = _controller.transform;
            m_initialPosition = m_controllerTransform.position;
        }

        public override Vector3 ReadInputMovement()
        {
            Vector3 targetPosition = m_initialPosition + m_path[m_currentTargetIdx];
            Vector3 targetDirection = Vector3.zero;

            if ((targetPosition - m_controllerTransform.position).magnitude < m_distanceThreshold)
            {
                m_currentTargetIdx = (m_currentTargetIdx + 1) % m_path.Count;
            } else
            {
                targetDirection = MaxSpeed * (targetPosition - m_controllerTransform.position).normalized;
            }

            return targetDirection;
        }
    }
}