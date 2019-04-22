using Interactions;
using UnityEngine;

namespace Molecules
{
    public class Moleculator : Trigger
    {

        private LoadInterface m_LoadInterface;

        private new void Start()
        {
            base.Start();
            m_LoadInterface = GameObject.FindWithTag("UIManager").GetComponent<LoadInterface>();
        }

        protected override void BeforeTrigger()
        {
            // Don't open interface if it's already opened
            if (Cursor.lockState == CursorLockMode.None) return;
            StartCoroutine(m_LoadInterface.OpenInterface("moleculatorUI"));
        }
    }
}