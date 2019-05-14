using System.Collections;
using Interactions;
using UnityEngine;

namespace MiseEnRoute
{
    public class Server : Switch
    {
        public string m_ServerName;
        public LeverSwitch m_LeverSwitch;

        private new void Start()
        {
            base.Start();
            SetLabel("Rebrancher le serveur");
        }


        protected override void BeforeSwitch()
        {
            if (!IsActivated())
            {
                MiseEnRouteManager.m_Instance.Plug(m_ServerName);
                AudioManager.m_Instance.PlaySFX("ServerMotor", gameObject);
            }
            else
            {
                MiseEnRouteManager.m_Instance.Unplug(m_ServerName);
                AudioManager.m_Instance.StopSFX("ServerMotor", gameObject);
            }
        }

        protected override void AfterSwitch()
        {
            m_CanInteractWith = false;
            StopLooking();
            StartCoroutine(ResetAfterSeconds());
            m_LeverSwitch.ResetSwitch();
        }

        private IEnumerator ResetAfterSeconds()
        {
            yield return new WaitForSeconds(2f);
            m_CanInteractWith = true;
            SetLabel(IsActivated() ? "Débrancher le serveur" : "Rebrancher le serveur");
        }
    }
}