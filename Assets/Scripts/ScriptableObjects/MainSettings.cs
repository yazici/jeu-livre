using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MainSettings", menuName = "GameSettings/Main", order = 1)]
    public class MainSettings : ScriptableObject
    {
        /**
         * How fast the player moves when walking (default move speed).
         */
        public float m_WalkSpeed = 6.0f;

        /**
         * How fast the player moves when running.
         */
        public float m_RunSpeed = 11.0f;

        /**
         * Margin in percentage between top 3D object pos and its label in canvas
         */
        public float m_MarginObjectLabel = 10;

        /**
         * The delay between each letter in typing effects
         */
        public float m_LetterDelay = 0.1f;

        /**
         * Max range for object interaction by arm range
         */
        public float m_RangeArm = 2f;

        /**
         * Max range for object interaction by foot range
         */
        public float m_RangeToFoot = 2f;
    }
}