using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Molecules
{
    public class Moleculator : MonoBehaviour
    {
        public RectTransform m_DropZone0;
        public RectTransform m_DropZone1;
        public RectTransform m_DropZone2;

        public TextMeshProUGUI m_ConsoleText;
        public Button m_ValidateButton;
        public ScrollRect m_ScrollView;

        private static GameObject[] _droppedItems;

        private void Start()
        {
            _droppedItems = new GameObject[3] { null, null, null };
            m_ValidateButton.interactable = false;
        }

        public void DoDrag(GameObject gameObj)
        {
            gameObj.transform.position = Input.mousePosition;
        }

        public void DoEndDrag(GameObject gameObj)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(m_DropZone0, Input.mousePosition))
            {
                if (_droppedItems[0] != null)
                {
                    _droppedItems[0].transform.localPosition = Vector3.zero;
                }

                _droppedItems[0] = gameObj;
                gameObj.transform.position = m_DropZone0.position;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(m_DropZone1, Input.mousePosition))
            {
                if (_droppedItems[1] != null)
                {
                    _droppedItems[1].transform.localPosition = Vector3.zero;
                }

                gameObj.transform.position = m_DropZone1.position;
                _droppedItems[1] = gameObj;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(m_DropZone2, Input.mousePosition))
            {
                if (_droppedItems[2] != null)
                {
                    _droppedItems[2].transform.localPosition = Vector3.zero;
                }

                gameObj.transform.position = m_DropZone2.position;
                _droppedItems[2] = gameObj;
            }
            else
            {
                gameObj.transform.localPosition = Vector3.zero;
                int foundIndex = Array.FindIndex(_droppedItems, droppedItem => droppedItem == gameObj);
                if (foundIndex != -1)
                {
                    _droppedItems[foundIndex] = null;
                    m_ValidateButton.interactable = false;
                }
            }

            if (_droppedItems[0] != null && _droppedItems[1] != null && _droppedItems[2] != null)
            {
                m_ValidateButton.interactable = true;
            }
            else
            {
                m_ValidateButton.interactable = false;
            }
        }

        public void DoReset()
        {
            if (_droppedItems[0] != null)
            {
                _droppedItems[0].transform.localPosition = Vector3.zero;
                _droppedItems[0] = null;
            }

            if (_droppedItems[1] != null)
            {
                _droppedItems[1].transform.localPosition = Vector3.zero;
                _droppedItems[1] = null;
            }

            if (_droppedItems[2] != null)
            {
                _droppedItems[2].transform.localPosition = Vector3.zero;
                _droppedItems[2] = null;
            }

            m_ValidateButton.interactable = false;
        }

        public void Validate()
        {
            if (_droppedItems[0].GetComponentInChildren<Text>().text == "Fucosyllactose"
                && _droppedItems[1].GetComponentInChildren<Text>().text == "Adrénaline"
                && _droppedItems[2].GetComponentInChildren<Text>().text == "Vitamine B2")
            {
                m_ConsoleText.text +=
                    "\n\nAnalyse de la solution...\n\nRésultat : solution valide, la synthèse peut commencer";
                AudioManager.m_Instance.PlaySFX("ValidationBeep");
            }
            else
            {
                m_ConsoleText.text += "\n\nAnalyse de la solution...\n\nERREUR - Synthétisation impossible";
                AudioManager.m_Instance.PlaySFX("ErrorBeep");
            }

            Canvas.ForceUpdateCanvases();
            m_ScrollView.verticalNormalizedPosition = 0f;
        }

        public void SetFocus(Transform window)
        {
            window.SetSiblingIndex(1);
        }
    }
}