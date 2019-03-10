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
    [SerializeField] private GameObject m_Indicator;
    [SerializeField] private Texture2D[] m_Textures;

    private GameObject[,] m_Tiles;
    private LabyPattern m_LabyPattern;
    private int m_LastLabyPatternIndex;

    public LabyScriptableObject m_Laby;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        GenerateDamier();
        // Draw a random pattern
        m_LastLabyPatternIndex = Random.Range(0, m_Laby.m_Patterns.Length);
        m_LabyPattern = m_Laby.m_Patterns[m_LastLabyPatternIndex];
        // Set the indicator letter
        var indicatorRenderer = m_Indicator.GetComponent<Renderer>();
        indicatorRenderer.material.SetTexture(MainTex, m_Textures[m_LastLabyPatternIndex]);
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
    }


    public void CheckNextTile(Vector2 pos)
    {
        GameObject[] allTiles = m_Tiles.Cast<GameObject>().ToArray();
        int lightenedTilesNb = allTiles.Count(t => t.GetComponent<DamierTileController>().m_IsLightened);

        // Shouldn't happen because of the success trigger but just in case
        if (lightenedTilesNb >= m_LabyPattern.m_Coords.Length)
            return;

        Vector2 rightPos = m_LabyPattern.m_Coords[lightenedTilesNb];

        if (rightPos == pos)
        {
            GameObject tile = m_Tiles[(int) pos.x, (int) pos.y];
            var tileController = tile.GetComponent<DamierTileController>();
            tileController.StartCoroutine(tileController.LightUp());

            if (lightenedTilesNb + 1 == m_LabyPattern.m_Coords.Length)
            {
                print("WIN!");
            }
        }
        // Failed
        else
        {
            // Light down the entire damier
            foreach (GameObject tile in allTiles)
            {
                var tileController = tile.GetComponent<DamierTileController>();
                tileController.StartCoroutine(tileController.LightDown());
            }

            // If player has not (re)started to pattern yet
            if (lightenedTilesNb == 0)
                return;

            LabyPattern[] labyPatterns = m_Laby.m_Patterns.Where((pattern, i) => i != m_LastLabyPatternIndex).ToArray();
            // Draw a random pattern
            int index = Random.Range(0, m_Laby.m_Patterns.Length - 1);
            m_LabyPattern = labyPatterns[index];
            m_LastLabyPatternIndex = Array.IndexOf(m_Laby.m_Patterns, m_LabyPattern);
            // Set the indicator letter
            var indicatorRenderer = m_Indicator.GetComponent<Renderer>();
            indicatorRenderer.material.SetTexture(MainTex, m_Textures[m_LastLabyPatternIndex]);
        }
    }
}