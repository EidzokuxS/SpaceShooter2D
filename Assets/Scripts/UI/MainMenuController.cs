using UnityEngine;

namespace SpaceShooter
{
    public class SpaceShipSelectionController : MonoBehaviour
    {
        #region Properties


        [SerializeField] private SpaceShip _defaultSpaceShip;

        #endregion

        #region Unity Events
        private void Start()
        {
            LevelSequenceController.PlayerShip = _defaultSpaceShip;
        }
        #endregion

        #region Public API

        public void OnSelectShip()
        {

        }

        #endregion
    }
}

