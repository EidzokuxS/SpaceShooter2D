using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class SelfDestruction : MonoBehaviour
    {
        [SerializeField] private float lifeTime;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}

