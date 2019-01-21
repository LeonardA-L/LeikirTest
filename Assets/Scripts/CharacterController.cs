using UnityEngine;

namespace LeikirTest
{

    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 lastDirection;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            lastDirection = transform.forward;
        }

        void FixedUpdate()
        {
            lastDirection = ReadInputForceGamepad();
            rb.MovePosition(rb.position + lastDirection);
        }

        void Update()
        {
            transform.forward = Vector3.Slerp(transform.forward, lastDirection, 10 * Time.deltaTime);   // TODO move to member
                                                                                                        //Note: Here the direction lerp is purely cosmetic. Direction does not impact movement (cf how FixedUpdate does not take direction into account)
        }

        Vector3 ReadInputForce()
        {
            return new Vector3(1, 0, 0);
        }

        Vector3 ReadInputForceGamepad()
        {
            Transform cam = Camera.main.transform;          // TODO move to member
            Debug.Assert(cam != null, "No Main Camera");

            Vector2 joy = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            joy.Normalize();
            joy *= 0.1f;    // TODO maxSpeed member

            Vector3 camForward = cam.forward;
            camForward.y = 0;
            //Debug.Assert(camForward.sqrMagnitude != 0);   // With the hack'n'slash view we assume we don't need this
            camForward.Normalize();

            return joy.y * camForward
                 + joy.x * cam.right;
        }
    }
}