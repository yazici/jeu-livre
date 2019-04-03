using UnityEngine;

namespace Interactions
{
    public class DoorOpen : Trigger
    {
        private void Start()
        {
            m_IsInfiniteTrigger = false;
            m_NbLimit = 1;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
