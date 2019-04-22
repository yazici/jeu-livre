using System.Linq;
using UnityEngine;

namespace MiseEnRoute
{
    public class MiseEnRouteManager : MonoBehaviour
    {
        /// <summary>
        /// Puzzle step representing player progression:
        /// <list type="bullet">
        /// <item>
        /// <term>0 :</term>
        /// <description>Re-plug the server</description>
        /// </item>
        /// <item>
        /// <term>1 :</term>
        /// <description>Push the light switch button</description>
        /// </item>
        /// <item>
        /// <term>2 :</term>
        /// <description>Interact with the keyboard</description>
        /// </item>
        /// <item>
        /// <term>3 :</term>
        /// <description>Win</description>
        /// </item>
        /// </list>
        /// </summary>
        [HideInInspector] public int m_PuzzleStep;

        [HideInInspector] public string m_ServerPlugged;

        [SerializeField] public Server[] m_Servers;

        [SerializeField] private string m_RightServerName;

        [SerializeField] private InitDrone m_InitDrone;

        // Singleton
        public static MiseEnRouteManager m_Instance;

        // Singleton initialization
        private void Awake()
        {
            if (m_Instance == null)
                m_Instance = this;
            else if (m_Instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            m_InitDrone.Init();
        }

        public void Plug(string serverName)
        {
            m_ServerPlugged = serverName;
            // Disable other servers
            m_Servers
                .Where(server => server.m_ServerName != serverName)
                .ToList()
                .ForEach(s => s.enabled = false);


            if (serverName == m_RightServerName && m_PuzzleStep == 0)
            {
                m_PuzzleStep = 1;
            }
        }

        public void Unplug(string serverName)
        {
            m_ServerPlugged = null;
            // Enable other servers
            m_Servers
                .Where(server => server.m_ServerName != serverName)
                .ToList()
                .ForEach(s => s.enabled = true);

            m_PuzzleStep = 0;
        }
    }
}