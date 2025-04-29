using Cinemachine;
using System.Collections.Generic;
using UnityEngine;


namespace Arenar.CameraService
{
    public class CameraStateGameplay : CameraState
    {
        private Camera _camera;
        
        public override void SetStateActive(Camera camera, Dictionary<CinemachineCameraType, CinemachineVirtualCamera> cinemachineVirtualCameras, Transform followTarget, Transform lookAtTarget)
        {
            base.SetStateActive(camera, cinemachineVirtualCameras, followTarget, lookAtTarget);
            _camera = camera;

            foreach (var cinemachine in cinemachineVirtualCameras)
            {
                cinemachine.Value.enabled = false;
            }
            
            _camera.transform.position = followTarget.transform.position;
            
            cinemachineVirtualCameras[CinemachineCameraType.Gameplay].Follow = followTarget;
            cinemachineVirtualCameras[CinemachineCameraType.Gameplay].LookAt = lookAtTarget;
            cinemachineVirtualCameras[CinemachineCameraType.Gameplay].enabled = true;
        }

        public override void OnUpdate()
        {

        }

        public override void OnLateUpdate()
        {

        }
    }
}