using System.Collections;
using UnityEngine;

public class GameShortcuts : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Vector3 m_MoleculesPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(MovePlayer(m_MoleculesPos));
        }
    }

    private IEnumerator MovePlayer(Vector3 target)
    {
        GameManager.m_Instance.m_CinematicMode = true;
        yield return null;
        m_Player.transform.localPosition = target;
        yield return null;
        GameManager.m_Instance.m_CinematicMode = false;

    }
}