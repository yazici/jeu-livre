using System;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamierController : MonoBehaviour
{
    [SerializeField] private GameObject m_DamierTileWhite;
    [SerializeField] private GameObject m_DamierTileBlack;
    [SerializeField] private int m_Size = 8;
    [SerializeField] private Renderer m_IndicatorRenderer;
    [SerializeField] private Texture2D[] m_Textures;
    [SerializeField] private float m_Timer = 10f;

    private GameObject[,] m_Tiles;
    private DamierTileController[] m_TilesControllers;

    private LabyPattern m_LabyPattern;
    private int m_LastLabyPatternIndex;
    private float m_TimeLeft;
    private bool m_Win;

    public LabyScriptableObject m_Laby;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        GenerateDamier();

        // Draw a random pattern
        m_LastLabyPatternIndex = Random.Range(0, m_Laby.m_Patterns.Length);
        m_LabyPattern = m_Laby.m_Patterns[m_LastLabyPatternIndex];

        // Set the indicator letter
        m_IndicatorRenderer.material.SetTexture(MainTex, m_Textures[m_LastLabyPatternIndex]);
    }

    private void Update()
    {
        if (m_Win || m_TimeLeft < 0f) return;

        m_TimeLeft -= Time.deltaTime;

        if (m_TimeLeft < 0f)
        {
            ResetDamier();
            DrawRandomPattern();
        }
    }

    private void GenerateDamier()
    {
        m_Tiles = new GameObject[m_Size, m_Size];

        for (int i = 0; i < m_Size; i++)
        {
            for (int j = 0; j < m_Size; j++)
            {
                GameObject tileType = ((j % 2) + (i % 2)) % 2 == 0 ? m_DamierTileWhite : m_DamierTileBlack;
                var pos = new Vector3(i, tileType.transform.localScale.y / 2, j);
                GameObject newTile = Instantiate(tileType, pos, Quaternion.identity);
                newTile.transform.SetParent(transform, false);
                m_Tiles[i, j] = newTile;
            }
        }

        m_TilesControllers = m_Tiles.Cast<GameObject>().Select(t => t.GetComponent<DamierTileController>()).ToArray();
    }

    private void ResetDamier()
    {
        foreach (DamierTileController tileController in m_TilesControllers)
        {
            tileController.StartCoroutine(tileController.LightDown());
        }
    }

    private void DrawRandomPattern()
    {
        // Exclude last pattern chosen
        LabyPattern[] labyPatterns = m_Laby.m_Patterns.Where((pattern, i) => i != m_LastLabyPatternIndex).ToArray();

        // Draw a random pattern
        int index = Random.Range(0, m_Laby.m_Patterns.Length - 1);
        m_LabyPattern = labyPatterns[index];
        m_LastLabyPatternIndex = Array.IndexOf(m_Laby.m_Patterns, m_LabyPattern);

        // Set the indicator letter
        m_IndicatorRenderer.material.SetTexture(MainTex, m_Textures[m_LastLabyPatternIndex]);
    }

    public void CheckNextTile(Vector2 pos)
    {
        int lightenedTilesNb = m_TilesControllers.Count(tc => tc.m_IsLightened);

        // Shouldn't happen because of the success trigger but just in case
        if (lightenedTilesNb >= m_LabyPattern.m_Coords.Length)
            return;

        Vector2 rightPos = m_LabyPattern.m_Coords[lightenedTilesNb];

        if (rightPos == pos)
        {
            GameObject tile = m_Tiles[(int) pos.x, (int) pos.y];
            var tileController = tile.GetComponent<DamierTileController>();
            tileController.StartCoroutine(tileController.LightUp());
            m_TimeLeft = m_Timer;

            if (lightenedTilesNb + 1 == m_LabyPattern.m_Coords.Length)
            {
                m_Win = true;
                print("WIN!");
            }
        }
        // Failed
        else
        {
            // Light down the entire damier
            ResetDamier();

            // If player has not (re)started to pattern yet, that's all
            if (lightenedTilesNb == 0)
                return;

            // Otherwise, re-pick another pattern
            DrawRandomPattern();
        }
    }
}