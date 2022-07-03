using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var playerGroundedFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().Inc<GroundedComponent>().End();
            var tryJumpFilter = ecsSystems.GetWorld().Filter<TryJump>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var tryJumpEntity in tryJumpFilter)
            {
                ecsSystems.GetWorld().DelEntity(tryJumpEntity);
                foreach (var playerEntity in playerGroundedFilter)
                {
                    ref var playerComponent = ref playerPool.Get(playerEntity);

                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}