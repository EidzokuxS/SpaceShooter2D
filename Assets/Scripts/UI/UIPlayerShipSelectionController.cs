using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{

    public class UIPlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip _prefab;

        [SerializeField] private TMP_Text _shipname;
        [SerializeField] private TMP_Text _hitpoints;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _mobility;

        [SerializeField] private Image _preview;

        private void Start()
        {
            if (_prefab != null)
            {
                _shipname.text = _prefab.Nickname;
                _hitpoints.text = "HP : " + _prefab.HitPoints.ToString();
                _speed.text = "Speed : " + _prefab.MaxLinearVelocity.ToString();
                _mobility.text = "Mobility : " + _prefab.MaxAngularVelocity.ToString();
                _preview.sprite = _prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = _prefab;
        }


    }
}