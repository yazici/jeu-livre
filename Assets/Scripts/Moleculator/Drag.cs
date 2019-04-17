using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public RectTransform dropZone0;
    public RectTransform dropZone1;
    public RectTransform dropZone2;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;


        //RectTransform panel = dropZone as RectTransform;

       if(RectTransformUtility.RectangleContainsScreenPoint(dropZone0, Input.mousePosition))
        {
            print("dropped");
            transform.position = dropZone0.position;


            //CancelButton.droppedItems[dropZone.GetSiblingIndex()] = this.gameObject;

            //dropZone.GetChild(0).gameObject.SetActive(false);

        }


        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone1, Input.mousePosition))
        {
            print("dropped");
            transform.position = dropZone1.position;


            //CancelButton.droppedItems[dropZone.GetSiblingIndex()] = this.gameObject;

            //dropZone.GetChild(0).gameObject.SetActive(false);

        }

        if (RectTransformUtility.RectangleContainsScreenPoint(dropZone2, Input.mousePosition))
        {
            print("dropped");
            transform.position = dropZone2.position;


            //CancelButton.droppedItems[dropZone.GetSiblingIndex()] = this.gameObject;

            //dropZone.GetChild(0).gameObject.SetActive(false);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
