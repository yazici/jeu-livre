﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MiseEnRoute
{
    public class InitDrone : MonoBehaviour
    {
        private ColorGrading m_ColorGradingLayer;
        private Animation m_Animation;
        
        [SerializeField]
        private PixelBoy m_PixelBoy;

        [SerializeField] private float m_AnimSpeed = 1;

        private void Awake()
        {
            var volume = gameObject.GetComponent<PostProcessVolume>();
            volume.profile.TryGetSettings(out m_ColorGradingLayer);
            m_Animation = GetComponent<Animation>();
        }

        public void Init()
        {
            m_Animation.Play();
            m_ColorGradingLayer.saturation.value = -100f;
            m_ColorGradingLayer.contrast.value = -100f;
            StartCoroutine(PlayColorAnim());
        }

        private IEnumerator PlayColorAnim()
        {
            float t = 0f;
            while (t <= 1)
            {
                float v = Mathf.Lerp(-100, 0, t);
                m_ColorGradingLayer.saturation.value = v;
                m_ColorGradingLayer.contrast.value = v;
                t += 0.5f * Time.deltaTime * m_AnimSpeed;
                yield return null;
            }
            
            m_ColorGradingLayer.saturation.value = 0;
            m_ColorGradingLayer.contrast.value = 0;
            m_PixelBoy.enabled = false;
        }
    }
}