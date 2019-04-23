using UnityEngine;

public class ShakeableTransform : MonoBehaviour
{
    // TODO: fix it so it takes in account the current camera transform, so it resets it correctly at the end
    [SerializeField] private float m_Frequency = 25;
    [SerializeField] private Vector3 m_MaximumTranslationShake = Vector3.one * 0.5f;
    [SerializeField] private Vector3 m_MaximumAngularShake = Vector3.one * 2;
    [SerializeField] private float m_RecoverySpeed = 1.5f;
    [SerializeField] private float m_TraumaExponent = 2;

    private float m_Seed;
    [SerializeField] private float m_Trauma = 0;

    private void Awake()
    {
        m_Seed = Random.value;
    }

    private void Update()
    {
        float shake = Mathf.Pow(m_Trauma, m_TraumaExponent);

        transform.localPosition = new Vector3(
                                      m_MaximumTranslationShake.x *
                                      (Mathf.PerlinNoise(m_Seed, Time.time * m_Frequency) * 2 - 1),
                                      m_MaximumTranslationShake.y *
                                      (Mathf.PerlinNoise(m_Seed + 1, Time.time * m_Frequency) * 2 - 1),
                                      m_MaximumTranslationShake.z *
                                      (Mathf.PerlinNoise(m_Seed + 2, Time.time * m_Frequency) * 2 - 1)
                                  ) * shake;

        transform.localRotation = Quaternion.Euler(new Vector3(
                                                       m_MaximumAngularShake.x *
                                                       (Mathf.PerlinNoise(m_Seed + 3, Time.time * m_Frequency) * 2 - 1),
                                                       m_MaximumAngularShake.y *
                                                       (Mathf.PerlinNoise(m_Seed + 4, Time.time * m_Frequency) * 2 - 1),
                                                       m_MaximumAngularShake.z *
                                                       (Mathf.PerlinNoise(m_Seed + 5, Time.time * m_Frequency) * 2 - 1)
                                                   ) * shake);

        m_Trauma = Mathf.Clamp01(m_Trauma - m_RecoverySpeed * Time.deltaTime);
    }
}