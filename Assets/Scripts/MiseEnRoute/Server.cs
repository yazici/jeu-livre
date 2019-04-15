using Interactions;

namespace MiseEnRoute
{
    public class Server : Switch
    {
        public string m_ServerName;

        private new void Start()
        {
            base.Start();
            SetLabel("Rebrancher");
        }


        protected override void BeforeSwitch()
        {
            if (!IsActivated())
            {
                MiseEnRouteManager.m_Instance.Plug(m_ServerName);
                SetLabel("Débrancher");
            }
            else
            {
                MiseEnRouteManager.m_Instance.Unplug(m_ServerName);
                SetLabel("Rebrancher");
            }
        }
    }
}