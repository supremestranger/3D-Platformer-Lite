using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().Inc<GroundedComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var i in filter)
            {
                ref var playerComponent = ref playerPool.Get(i);
                ref var playerInputComponent = ref playerInputPool.Get(i);

                if (playerInputComponent.jumpInput)
                {
                    playerInputComponent.jumpInput = false;
                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}