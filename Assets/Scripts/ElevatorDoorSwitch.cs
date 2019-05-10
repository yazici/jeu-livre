using System.Collections;
using System.Collections.Generic;
using Interactions;
using UnityEngine;

public class ElevatorDoorSwitch : Switch
{
    [SerializeField] private Animator m_Animator;
    private static readonly int ToggleDoors = Animator.StringToHash("ToggleDoors");
    public bool m_IsAnimating;
    private new void Start()
    {
        base.Start();
        m_State = false;
        SetLabel("Ouvrir les portes de l'ascenseur");
    }

    protected override void AfterSwitch()
    {
        m_CanInteractWith = false;
        StopLooking();
        m_Animator.SetTrigger(ToggleDoors);
        m_IsAnimating = true;
        // AudioManager.m_Instance.PlaySFX("Lever"); TODO
        StartCoroutine(ResetSwitchAfterAnim());
    }

    private IEnumerator ResetSwitchAfterAnim()
    {
        yield return StartCoroutine(WaitForAnim());
        SetLabel((m_State ? "Fermer" : "Ouvrir") + " les portes de l'ascenseur");
        m_CanInteractWith = true;
        m_IsAnimating = false;
    }


    private IEnumerator WaitForAnim()
    {
        yield return null;
        while (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1 || m_Animator.IsInTransition(0))
        {
            yield return null;
        }
    }
}