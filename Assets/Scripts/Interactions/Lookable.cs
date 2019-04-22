using System.Collections;
using System.Linq;
using UnityEngine;

namespace Interactions
{
    public class Lookable : MonoBehaviour
    {
        [SerializeField] private string m_Label;
        [SerializeField] private bool m_IsLabelCentered;

        private Camera m_MainCamera;
        private PlayerRaycast m_PlayerRaycast;
        private BoxCollider m_Collider;
        private UIManager m_UIManager;

        private float m_LabelMargin;

        private float m_RangeToFoot;
        private float m_RangeArm;

        private bool m_IsLooking;
        private bool m_IsRewritingLabel;

        private Renderer m_Renderer;
        private Shader m_DefaultShader;
        private Shader m_OutlineShader;

        protected void Start()
        {
            m_MainCamera = Camera.main;
            m_PlayerRaycast = GameObject.FindWithTag("Player").GetComponent<PlayerRaycast>();
            m_Collider = GetComponent<BoxCollider>();
            m_UIManager = UIManager.m_Instance;
            m_LabelMargin = m_UIManager.m_MainSettings.m_MarginObjectLabel;

            m_RangeArm = m_UIManager.m_MainSettings.m_RangeArm;
            m_RangeToFoot = m_UIManager.m_MainSettings.m_RangeToFoot;

            // TODO: debug this with multiple material objects
            // m_Renderer = GetComponent<Renderer>();
            // m_DefaultShader = m_Renderer.material.shader;
            // m_OutlineShader = Shader.Find("Outlined/UltimateOutline");
        }

        protected void Update()
        {
            Transform camTransform = m_MainCamera.transform;

            float maxDistance = camTransform.localRotation.x > 0.38f ? m_RangeToFoot : m_RangeArm;
            Vector3 distanceFromCamera = transform.position - camTransform.position;

            // If too far
            if (distanceFromCamera.magnitude > maxDistance)
            {
                StopLooking();
                return;
            }

            // Otherwise try to raycast
            m_PlayerRaycast.AttemptRaycast(this, maxDistance);
        }


        public void Look()
        {
            m_IsLooking = true;

            if (this is Interactive)
            {
                // Change reticule aspect
                m_UIManager.SetReticule();
            }

            // Calculate label positioning
            Transform tr = transform;
            Vector3 worldPos = tr.TransformPoint(m_Collider.center);
            worldPos.y += m_Collider.size.y * tr.localScale.y / 2;
            Vector3 viewPos = m_MainCamera.WorldToViewportPoint(worldPos);

            float offset = m_LabelMargin / 100;

            RectTransform rectTransform = m_UIManager.m_LabelTextRectTransform;
            if (m_IsLabelCentered)
            {
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f + offset);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f + offset);
            }
            else
            {
                rectTransform.anchorMin = new Vector2(viewPos.x, viewPos.y + offset);
                rectTransform.anchorMax = new Vector2(viewPos.x, viewPos.y + offset);
            }


            // Type text if different
            if (m_UIManager.GetCurrentLabelText() != m_Label)
                m_UIManager.ChangeLabelText(m_Label);

            // Change shader (TODO: debug this with multiple material objects)
            // m_Renderer.materials.ToList().ForEach(material => material.shader = m_OutlineShader);
        }

        public void StopLooking()
        {
            if (!m_IsLooking) return;
            m_IsLooking = false;

            // Change reticule aspect
            m_UIManager.SetReticule(false);

            // Reset text
            m_UIManager.ResetLabelText();

            // Reset shader (TODO: debug this with multiple material objects)
            // m_Renderer.materials.ToList().ForEach(material => material.shader = m_DefaultShader);
        }

        protected void SetLabel(string label)
        {
            m_Label = label;
            if (!m_IsLooking) return;
            StartCoroutine(ForceRewriteLabel());
        }

        private IEnumerator ForceRewriteLabel()
        {
            if (m_IsRewritingLabel) yield break;
            m_IsRewritingLabel = true;
            m_UIManager.ResetLabelText();
            yield return null;
            m_UIManager.ChangeLabelText(m_Label);
            m_IsRewritingLabel = false;
        }
    }
}