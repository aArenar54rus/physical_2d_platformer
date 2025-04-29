using Arenar.CameraService;
using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Arenar.Services
{
    public class CameraServiceInstaller : MonoInstaller
    {
        [SerializeField]
        private Camera mainCamera = default;
        [SerializeField]
        private CinemachineData[] cinemachineVirtualCameraDatas;


        public override void InstallBindings()
        {
            Dictionary<CinemachineCameraType, CinemachineVirtualCamera> cinemachineVirtualCameras =
                new Dictionary<CinemachineCameraType, CinemachineVirtualCamera>();

            foreach (var data in cinemachineVirtualCameraDatas)
                cinemachineVirtualCameras.Add(data.CameraType, data.CinemachineVirtualCamera);
            
            Container.Bind<Camera>()
                .FromInstance(mainCamera)
                .AsSingle()
                .NonLazy();
            
            Container.BindInstance(cinemachineVirtualCameras)
                .AsSingle()
                .NonLazy();

            Container.Bind<ICameraService>()
                .To<Arenar.CameraService.CameraService>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CameraStatesFactory>()
                .AsSingle()
                .NonLazy();
        }



        [Serializable]
        public class CinemachineData
        {
            [SerializeField]
            private CinemachineCameraType cinemachineCameraType;
            [SerializeField]
            private CinemachineVirtualCamera cinemachineVirtualCamera;
            
            
            public CinemachineCameraType CameraType => cinemachineCameraType;
            public CinemachineVirtualCamera CinemachineVirtualCamera => cinemachineVirtualCamera;
        }
    }
}