using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LabyData", menuName = "GameSettings/Labyrinthe", order = 1)]
    public class LabyScriptableObject : ScriptableObject
    {
        public LabyPattern[] m_Patterns;
    }
}