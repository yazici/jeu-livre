using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamierController : MonoBehaviour
{
    [SerializeField] private GameObject m_DamierTileWhite;
    [SerializeField] private GameObject m_DamierTileBlack;
    [SerializeField] private int m_Size = 8;

    private GameObject[,] m_Tiles;
    
    public LabyScriptableObject m_Laby;

    private void Start()
    {
        GenerateDamier();
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
        if (lightenedTilesNb >= m_Laby.m_Pattern.Length)
            return;
        
        Vector2 rightPos = m_Laby.m_Pattern[lightenedTilesNb];

        if (rightPos == pos)
        {
            GameObject tile = m_Tiles[(int) pos.x, (int) pos.y];
            DamierTileController tileController = tile.GetComponent<DamierTileController>();
            tileController.StartCoroutine(tileController.LightUp());

            if (lightenedTilesNb + 1 == m_Laby.m_Pattern.Length)
            {
                print("WIN!");
            }
                
        }
        // Failed
        else
        {
            foreach (GameObject tile in allTiles)
            {
                DamierTileController tileController = tile.GetComponent<DamierTileController>();
                tileController.StartCoroutine(tileController.LightDown());
            }
        }
    }
}