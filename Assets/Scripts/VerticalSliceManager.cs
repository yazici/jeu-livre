using MiseEnRoute;
using UnityEngine;

public class VerticalSliceManager : MonoBehaviour
{
    [SerializeField] private InitDrone m_InitDrone;
    [SerializeField] private Light m_TorchLight;
    
    private void Start()
    {
        m_InitDrone.Init();
        UIManager.m_Instance.m_TorchLight = m_TorchLight;
    }
}
