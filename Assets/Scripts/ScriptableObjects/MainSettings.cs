using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MainSettings", menuName = "GameSettings/Main", order = 1)]
    public class MainSettings : ScriptableObject
    {
        /// <summary>
        /// The name of the scene to load after the starting console
        /// </summary>
        public string m_MainScene = "VerticalSlice";
        
        /// <summary>
        /// How fast the player moves when walking (default move speed).
        /// </summary>
        public float m_WalkSpeed = 6.0f;

        /// <summary>
        /// How fast the player moves when running.
        /// </summary>
        public float m_RunSpeed = 11.0f;

        /// <summary>
        /// Margin in percentage between top 3D object pos and its label in canvas
        /// </summary>
        public float m_MarginObjectLabel = 10;

        /// <summary>
        /// The delay between each letter in typing effects
        /// </summary>
        public float m_LetterDelay = 0.1f;

        /// <summary>
        /// Max range for object interaction by arm range
        /// </summary>
        public float m_RangeArm = 2f;

        /// <summary>
        /// Max range for object interaction by foot range
        /// </summary>
        public float m_RangeToFoot = 2f;

        /// <summary>
        /// Id name for the starting console log-in step
        /// </summary>
        public string m_IdName = "aurorechamrouge";
        
        /// <summary>
        /// Password for the starting console log-in step
        /// </summary>
        public string m_Password = "%SRghatN895";
    }
}