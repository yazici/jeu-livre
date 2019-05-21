using UnityEngine;

namespace Molecules
{
    public class MoleculesManager : MonoBehaviour
    {
        // [SerializeField] private InitDrone m_InitDrone;

        [HideInInspector] public bool m_SyntheseValide;
        
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private Sprite m_SpriteValide;
        
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

        public void PutSyntheseValide()
        {
            m_SyntheseValide = true;
            m_SpriteRenderer.sprite = m_SpriteValide;
        }

        private void Start()
        {
            // m_InitDrone.Init();
        }
    }
}