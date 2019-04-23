using Interactions;

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
                SetLabel("Débrancher le serveur");
            }
            else
            {
                MiseEnRouteManager.m_Instance.Unplug(m_ServerName);
                AudioManager.m_Instance.StopSFX("ServerMotor", gameObject);
                SetLabel("Rebrancher le serveur");
            }
        }

        protected override void AfterSwitch()
        {
            m_LeverSwitch.ResetSwitch();
        }
    }
}