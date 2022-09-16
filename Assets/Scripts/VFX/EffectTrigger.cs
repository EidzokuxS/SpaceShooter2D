using UnityEngine;

namespace SpaceShooter
{
    public class EffectTrigger : MonoBehaviour
    {
        #region Properties

        [SerializeField] private GameObject _effect;

        #endregion

        #region Unity Events;

        /* private void Start()
         {
             var Object = GetComponentInParent<Destuctible>();

             Object.EventOnDeath.AddListener(TriggerEffect);

         }
        */
        #endregion

        #region Private API

        public void TriggerEffect(Vector3 objectPos)
        {
            Instantiate(_effect, objectPos, Quaternion.identity);
        }

        #endregion
    }
}

