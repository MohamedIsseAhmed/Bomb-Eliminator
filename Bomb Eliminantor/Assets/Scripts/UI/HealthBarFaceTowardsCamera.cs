using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class HealthBarFaceTowardsCamera : MonoBehaviour
    {
        private Camera camera;
        [SerializeField] private Transform bar;
        private RectTransform rectTransform;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        void Start()
        {
            camera = Camera.main;
        }


        void Update()
        {
            LookCamera();
        }
        private void LookCamera()
        {
            Vector3 directionToCamera = camera.transform.position - transform.localPosition;
            directionToCamera.x = 0;

            Quaternion lookRotation = Quaternion.LookRotation(camera.transform.forward);

            rectTransform.rotation = lookRotation;
        }
    }
}

