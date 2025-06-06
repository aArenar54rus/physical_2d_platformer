using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;


namespace Arenar.CameraService
{
    public class CameraService : ICameraService, ITickable, ILateTickable
    {
        public Camera GameCamera { get; private set; }
        public Dictionary<CinemachineCameraType, CinemachineVirtualCamera> CinemachineVirtualCameras { get; private set; }
        public Dictionary<Type, ICameraState> CameraStates { get; private set; }

        private ICameraState lastActiveState;
        private bool isInitialize = false;
        private CinemachineCameraType lastActiveType = CinemachineCameraType.None;

        private TickableManager _tickableManager;


        [Inject]
        public void Construct(Camera camera,
                              Dictionary<CinemachineCameraType, CinemachineVirtualCamera> cinemachineVirtualCameras,
                              ICameraState[] states,
                              CameraStatesFactory cameraStatesFactory,
                              TickableManager tickableManager)
        {
            GameCamera = camera;
            CinemachineVirtualCameras = cinemachineVirtualCameras;
            
            Type baseType = typeof(ICameraState);
            Type[] implementations = baseType.GetImplementations();

            CameraStates = new Dictionary<Type, ICameraState>();
            foreach (Type cameraStateType in implementations)
            {
                var state = cameraStatesFactory.Create(cameraStateType);
                CameraStates.Add(cameraStateType, state);
            }

            _tickableManager = tickableManager;
            _tickableManager.Add(this);
            _tickableManager.AddLate(this);
            
            isInitialize = true;
        }
        
        public void SetCameraState<TCameraState>(Transform followTarget, Transform lookAtTarget)
            where TCameraState : ICameraState
        {
            TCameraState currentState = (TCameraState)CameraStates[typeof(TCameraState)];

            if (lastActiveState != null)
                lastActiveState.SetStateDeactive();
            
            currentState.SetStateActive(GameCamera, CinemachineVirtualCameras, followTarget, lookAtTarget);
            lastActiveState = currentState;
        }
        
        public void SetCinemachineVirtualCamera(CinemachineCameraType cinemachineCameraType)
        {
            if (lastActiveType == cinemachineCameraType)
                return;

            lastActiveType = cinemachineCameraType;
            foreach (var virtualCamera in CinemachineVirtualCameras)
                virtualCamera.Value.enabled = (virtualCamera.Key == lastActiveType);
        }

        public void Tick()
        {
            if (lastActiveState == null)
                return;
            
            lastActiveState.OnUpdate();
        }

        public void LateTick()
        {
            if (lastActiveState == null)
                return;
            
            lastActiveState.OnLateUpdate();
        }
    }
}