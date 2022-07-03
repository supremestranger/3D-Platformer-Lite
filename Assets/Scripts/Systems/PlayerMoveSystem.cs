using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
            }
            
        }
    }
}