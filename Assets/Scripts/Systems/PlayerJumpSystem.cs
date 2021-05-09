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
            var playerGroundedFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().Inc<GroundedComponent>().End();
            var tryJumpFilter = ecsSystems.GetWorld().Filter<TryJump>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var i in tryJumpFilter)
            {
                ecsSystems.GetWorld().DelEntity(i);
                foreach (var j in playerGroundedFilter)
                {
                    ref var playerComponent = ref playerPool.Get(j);

                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}