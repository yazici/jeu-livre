using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
    public void Look()
    {
        UIManager.m_Instance.SetReticule();
        // foreach(MaterialSwitch ms in this.transform.GetComponentsInChildren<MaterialSwitch>())
        // {
        //     if (ms != null)
        //     {
        //         ms.HighLightMat();
        //     }
        // }
    }

    public void StopLooking()
    {
        UIManager.m_Instance.SetReticule(false);
        // foreach (MaterialSwitch ms in this.transform.GetComponentsInChildren<MaterialSwitch>())
        // {
        //     if (ms != null)
        //     {
        //         ms.StandardMat();
        //     }
        // }
    }

    public abstract void Interact();
}