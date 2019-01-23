using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeikirTest
{
    /*
     * Control strategy reading the movement command from an "AI".
     * (for the sake of time it's not AI per se as it isn't intelligent at all)
     * Will follow a path consisting in a list of target points, one point after the other.
     * Each instance of the strategy can define its own path.
     * (see ControlStrategy.cs)
     */
    [CreateAssetMenu(menuName = "Control Strategies/AI Control")]
    public class AIControl : ControlStrategy
    {
        [Tooltip("List of points defining the path of the character. Each point is relative to the starting point of the character")]
        public List<Vector3> m_path;
        [Tooltip("Starting index of the target point in the path")]
        public int m_currentTargetIdx = 0;
        [Tooltip("Threshold under which the character is close enough to its target to move on to the next point in the path")]
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
            Debug.Assert(m_initialPosition != null, "Initial Position has not been initialized.");  // Right now we can't hotswap strategies
            // Compute target position
            Vector3 targetPosition = m_initialPosition + m_path[m_currentTargetIdx];
            Vector3 targetDirection = Vector3.zero;

            // Check if we are close enough to the target to allow moving on to the next
            if ((targetPosition - m_controllerTransform.position).magnitude < m_distanceThreshold)
            {
                m_currentTargetIdx = (m_currentTargetIdx + 1) % m_path.Count;
            }
            // Else define direction to the target
            else
            {
                targetDirection = MaxSpeed * (targetPosition - m_controllerTransform.position).normalized;
                // Note: This could be lerped for fancier, less robotic turns.
            }

            return targetDirection;
        }
    }
}