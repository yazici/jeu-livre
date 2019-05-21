using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private SuddenShutdown m_SuddenShutdown;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        m_SuddenShutdown.CloseSystem();
    }
}