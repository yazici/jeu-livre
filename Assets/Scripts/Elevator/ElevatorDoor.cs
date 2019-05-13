using JetBrains.Annotations;
using UnityEngine;

namespace Elevator
{

    public class ElevatorDoor : MonoBehaviour
    {
        public bool m_State;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">0: close; 1: open</param>
        [UsedImplicitly]
        public void PlayDoorSFX(int state)
        {
            AudioManager.m_Instance.PlaySFX(
                state == 0 ? "ElevatorDoorsClosing" : "ElevatorDoorsOpening"
            );
        }

        [UsedImplicitly]
        public void PlayDoorCloseTap()
        {
            AudioManager.m_Instance.PlaySFX("ElevatorDoorsClosingTap");
        }
    }
}