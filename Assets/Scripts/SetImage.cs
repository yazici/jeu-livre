using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    private void Start()
    {
        if (ScannerTrigger.scanImage != null)
        {
            GetComponent<Image>().sprite = ScannerTrigger.scanImage;
        }
    }
}