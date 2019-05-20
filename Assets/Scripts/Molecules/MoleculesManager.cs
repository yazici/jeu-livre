using MiseEnRoute;
using UnityEngine;

namespace Molecules
{
    public class MoleculesManager : MonoBehaviour
    {
        [SerializeField] private InitDrone m_InitDrone;

        [HideInInspector] public bool m_SyntheseValide;
        
        // Singleton
        public static MoleculesManager m_Instance;

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
    }
}