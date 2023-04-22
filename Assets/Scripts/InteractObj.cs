using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace BNG {

    /// <summary>
    /// This is an example of how to highlight an object on hover / activate. This is used in the Demo scene in conjunction with the "PointerEvents" component.
    /// </summary>
    public class InteractObj : MonoBehaviour {

        public Material HighlightMaterial;
        public Material ActiveMaterial;
        public Material AlertMaterial;

        public float minTimeSpawn = 3f;
        public float maxTimeSpawn = 7f;
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _notifSource;
        private AudioSource _gunSource;
        [SerializeField] private ParticleSystem correctParticle;

        // Currently activating the object?
        bool active = false;

        // Currently hovering over the object?
        bool hovering = false;
        
        // Selectable?
        public bool isSelectable;
        private bool alerting;

        Material initialMaterial;
        MeshRenderer render;

        void Start() {
            render = GetComponent<MeshRenderer>();
            initialMaterial = render.sharedMaterial;
            if (isSelectable)
            {
                StartCoroutine("ChangeMat");
            }

            _gunSource = GameObject.Find("SPAWN MANAGER").GetComponent<SpawnerManager>().gunAudioSource;
        }

        public void ClickEvent(PointerEventData eventData)
        {
            _gunSource.Play();
            
            if (alerting)
            {
                _audioSource.Play();
                correctParticle.Play();
                alerting = false;
                UpdateMaterial();
            }
        }

        // Holding down activate
        public void SetActive(PointerEventData eventData) {
            active = true;

            UpdateMaterial();
            
        }

        // No longer holding down activate
        public void SetInactive(PointerEventData eventData) {
            active = false;

            UpdateMaterial();
        }

        // Hovering over our object
        public void SetHovering(PointerEventData eventData) {
            hovering = true;

            UpdateMaterial();
        }

        // No longer hovering over our object
        public void ResetHovering(PointerEventData eventData) {
            hovering = false;
            active = false;

            UpdateMaterial();
        }

        private void UpdateMaterial() {
            if (active) {
                render.sharedMaterial = ActiveMaterial;
            }
            else if (hovering) {
                render.sharedMaterial = HighlightMaterial;
            }
            else {
                render.sharedMaterial = alerting ? AlertMaterial : initialMaterial;
            }
        }

        IEnumerator ChangeMat()
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            alerting = true;
            _notifSource.Play();
            UpdateMaterial();
        }
    }
}

