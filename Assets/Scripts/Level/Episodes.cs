using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class Episodes : ScriptableObject
    {
        #region Properties

        [SerializeField] private string _episodeName;
        public string EpisodeName => _episodeName;

        [SerializeField] private string[] _levels;
        public string[] Levels => _levels;

        [SerializeField] private Sprite _previewImage;
        public Sprite PreviewImage => _previewImage;


        #endregion

        #region Unity Events

        #endregion

        #region Private API

        #endregion
    }

}
