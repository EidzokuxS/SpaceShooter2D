using UnityEngine;
using UnityEngine.EventSystems;


namespace SpaceShooter
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Properties

        private bool _hold;
        public bool IsHold => _hold;

        #endregion

        #region Unity Events

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _hold = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _hold = false;
        }
        #endregion

    }

}
