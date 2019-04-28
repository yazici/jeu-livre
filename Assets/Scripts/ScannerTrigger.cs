using Interactions;
using UnityEngine;
using UnityEngine.UI;

public class ScannerTrigger : Trigger
{

    public static Sprite scanImage;
    public Sprite image;

    private LoadInterface m_LoadInterface;

    private new void Start()
    {
        base.Start();
        m_LoadInterface = GameObject.FindWithTag("UIManager").GetComponent<LoadInterface>();
    }

    protected override void BeforeTrigger()
    {
        // Don't open interface if it's already opened
        if (Cursor.lockState == CursorLockMode.None) return;
        scanImage = image;
        StartCoroutine(m_LoadInterface.OpenInterface("ScannerUI", false));
    }
}