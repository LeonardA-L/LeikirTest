using UnityEngine;

namespace LeikirTest
{

    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        public ControlStrategy m_controlStrategy;

        private Rigidbody rb;
        private Vector3 lastDirection;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            lastDirection = transform.forward;
            Debug.Assert(m_controlStrategy != null, "No control strategy provided");
        }

        void FixedUpdate()
        {
            lastDirection = m_controlStrategy.ReadInputMovement();
            rb.MovePosition(rb.position + lastDirection * Time.fixedDeltaTime);
        }

        void Update()
        {
            transform.forward = Vector3.Slerp(transform.forward, lastDirection, m_controlStrategy.RotationSpeed * Time.deltaTime);
                                                                                                        //Note: Here the direction lerp is purely cosmetic. Direction does not impact movement (cf how FixedUpdate does not take direction into account)
        }
    }
}