using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamierTileController : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    [SerializeField] private Color m_EmissionColor;
    [SerializeField] private float m_LightUpSpeed = 1f;
    [SerializeField] private float m_LightDownSpeed = 2f;

    private Material m_Material;
    private DamierController m_DamierController;

    private Renderer m_Renderer;
    private MaterialPropertyBlock m_PropBlock;
    private bool isTransitionning = false;

    public bool m_IsLightened = false;

    private void Awake()
    {
        m_PropBlock = new MaterialPropertyBlock();
        m_Renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        // m_Material = GetComponent<Renderer>().material;
        // m_Material.EnableKeyword("_EMISSION");
        
        m_Renderer.GetPropertyBlock(m_PropBlock);
        //print("PROP: " + m_PropBlock.GetColor());
        m_PropBlock.SetColor(EmissionColor, Color.black);
        m_Renderer.SetPropertyBlock(m_PropBlock);

        m_DamierController = GetComponentInParent<DamierController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Vector3 position = transform.localPosition;
        m_DamierController.CheckNextTile(new Vector2(position.x, position.z));
    }

    public IEnumerator LightUp()
    {
        if (isTransitionning || m_IsLightened) yield break;
        
        m_IsLightened = true;
        isTransitionning = true;
        float t = 0f;

        while (t <= 1.0f)
        {
            float emission = Mathf.Lerp(0.0f, 1.0f, t);
            Color color = m_EmissionColor * Mathf.LinearToGammaSpace(emission);
            
            m_Renderer.GetPropertyBlock(m_PropBlock);
            m_PropBlock.SetColor(EmissionColor, color);
            m_Renderer.SetPropertyBlock(m_PropBlock);
            
            t += Time.deltaTime * m_LightUpSpeed;
            yield return null;
        }

        isTransitionning = false;
    }

    public IEnumerator LightDown()
    {
        if (isTransitionning || !m_IsLightened) yield break;
        
        m_IsLightened = false;
        isTransitionning = true;
        float t = 0f;

        while (t <= 1.0f)
        {
            float emission = Mathf.Lerp(1.0f, 0.0f, t);
            Color color = m_EmissionColor * Mathf.LinearToGammaSpace(emission);
            
            m_Renderer.GetPropertyBlock(m_PropBlock);
            m_PropBlock.SetColor(EmissionColor, color);
            m_Renderer.SetPropertyBlock(m_PropBlock);
            
            t += Time.deltaTime * m_LightDownSpeed;
            yield return null;
        }

        isTransitionning = false;
    }
}