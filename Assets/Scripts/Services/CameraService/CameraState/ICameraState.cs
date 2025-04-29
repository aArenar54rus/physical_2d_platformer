using Cinemachine;
using System.Collections.Generic;
using UnityEngine;


namespace Arenar.CameraService
{
    public interface ICameraState
    {
        bool IsActive { get; }


        void SetStateActive(Camera camera, Dictionary<CinemachineCameraType, CinemachineVirtualCamera> cinemachineVirtualCameras, Transform followTarget, Transform lookAtTarget);

        void OnUpdate();
        
        void OnLateUpdate();

        void SetStateDeactive();
    }
}