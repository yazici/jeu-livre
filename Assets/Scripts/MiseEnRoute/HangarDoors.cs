using UnityEngine;

namespace MiseEnRoute
{
    public class HangarDoors : MonoBehaviour
    {
        private Animator m_Animator;
        private static readonly int Doors = Animator.StringToHash("OpenDoors");

        private void Start()
        {
            m_Animator = GetComponent<Animator>();
        }

        private void Update()
        {
        }

        public void OpenDoors()
        {
            AudioManager.m_Instance.PlaySFX("GarageDoors", gameObject);
            m_Animator.SetTrigger(Doors);
        }

        public void OnDoorsOpened()
        {
            AudioManager.m_Instance.StopSFX("GarageDoors", gameObject);
        }
    }
}