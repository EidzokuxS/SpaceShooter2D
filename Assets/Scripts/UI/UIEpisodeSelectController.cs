using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIEpisodeSelectController : MonoBehaviour
    {
        #region Properties
        [SerializeField] private Episodes _episode;
        [SerializeField] private TMP_Text _episodeName;
        [SerializeField] private Image _previevImage;
        #endregion

        #region Unity Events
        private void Start()
        {
            if (_episodeName != null)
                _episodeName.text = _episode.EpisodeName;
            if (_previevImage != null)
                _previevImage.sprite = _episode.PreviewImage;
        }
        #endregion

        #region Public API
        public void OnEpisodeStartClick()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }
        #endregion
    }

}
