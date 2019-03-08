
using UnityEngine;

[CreateAssetMenu(fileName = "LabyData", menuName = "GameSettings/Labyrinthe", order = 1)]
public class LabyScriptableObject : ScriptableObject
{
    public Vector2[] m_Pattern;
}
