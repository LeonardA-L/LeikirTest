using UnityEngine;

namespace LeikirTest
{
    public class CameraManager : Singleton<CameraManager>
    {
        protected CameraManager() { }

        public float m_distance = 12;

        private Transform m_cam;
        private Transform m_character;

        void Start()
        {
            m_cam = Camera.main.transform;
            var character = (LeikirTest.CharacterController)FindObjectOfType(typeof(LeikirTest.CharacterController)); // Assume there is only one character
            // In an actual production, player character(s) would be dealt by a PlayerManager, which would expose a GetPlayers() accessor
            Debug.Assert(character != null, "No LeikirTest.CharacterController found on scene");
            m_character = character.transform;
        }

        void Update()
        {
            m_cam.transform.position = Vector3.Lerp(m_cam.transform.position, m_character.position - m_cam.forward * m_distance, 0.1f);
        }
    }
}