using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Basic class for every interactive object on a scene
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Object name for a user
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;

    }
}
