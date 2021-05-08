using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int cameraEntity;

        public void Init(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();

            var cameraEntity = ecsSystems.GetWorld().NewEntity();

            var cameraPool = ecsWorld.GetPool<CameraComponent>();
            cameraPool.Add(cameraEntity);
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            cameraComponent.cameraTransform = Camera.main.transform;
            cameraComponent.cameraSmoothness = gameData.configuration.cameraFollowSmoothness;
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0f, 1f, -9f);

            this.cameraEntity = cameraEntity;
        }

        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();

            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            foreach(var i in filter)
            {
                ref var playerComponent = ref playerPool.Get(i);

                Vector3 currentPosition = cameraComponent.cameraTransform.position;
                Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

                cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
            }    
        }
    }
}
