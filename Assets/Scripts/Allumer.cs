using System.Collections;
using Interactions;
using UnityEngine;

public class Allumer : Trigger
{
    public VLight m_Vlight;

    [SerializeField] private float m_LightGainBySecond = 0.1f;

    protected override void AfterTrigger()
    {
        StartCoroutine(LightOn());
        StartCoroutine(OpenDoor());
    }

    private IEnumerator LightOn()
    {
        while (m_Vlight.lightMultiplier < 0.4)
        {
            m_Vlight.lightMultiplier += m_LightGainBySecond * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator OpenDoor()
    {
        // TODO
        yield return null;
    }
}