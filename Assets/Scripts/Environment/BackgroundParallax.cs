using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundParallax : MonoBehaviour
    {
        #region Properties

        [Range(0.0f, 16.0f)]
        [SerializeField] private float m_ParallaxPower;

        [SerializeField] private float m_TextureScale;

        private Material m_QuadMaterial;
        private Vector2 m_InitialOffset;

        #endregion

        #region UnityEvents

        private void Start()
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material;
            m_InitialOffset = Random.insideUnitCircle;

            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale;
        }

        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            m_QuadMaterial.mainTextureOffset = offset;
        }
        #endregion
    }
}

