using System.Collections;
using Interactions;
using UnityEngine;

public class Allumer : Trigger
{
    [SerializeField] private VLight[] m_Vlights;
    [SerializeField] private float m_LightGainBySecond = 0.1f;
    [SerializeField] private Vector3 m_ElevatorStartPos;
    [SerializeField] private GameObject m_ElevatorCapsule;
  

    protected override void AfterTrigger()
    {
        AudioManager.m_Instance.PlaySFX("GenericBeep");
        m_ElevatorCapsule.transform.localPosition = m_ElevatorStartPos;
        StartCoroutine(LightOn());
    }

    private IEnumerator LightOn()
    {
        if (m_Vlights.Length == 0) yield break;
        while (m_Vlights[0].lightMultiplier < 0.4)
        {
            foreach (VLight vLight in m_Vlights)
            {
                vLight.lightMultiplier += m_LightGainBySecond * Time.deltaTime;
            }

            yield return null;
        }
    }
}