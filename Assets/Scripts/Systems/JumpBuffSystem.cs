using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class JumpBuffSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<JumpBuffComponent>().Inc<PlayerComponent>().End();
            var jumpBuffPool = ecsSystems.GetWorld().GetPool<JumpBuffComponent>();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var entity in filter)
            {
                ref var jumpBuffComponent = ref jumpBuffPool.Get(entity);
                ref var playerComponent = ref playerPool.Get(entity);

                jumpBuffComponent.timer -= Time.deltaTime;

                if (jumpBuffComponent.timer <= 0)
                {
                    playerComponent.playerJumpForce /= 2f;
                    jumpBuffPool.Del(entity);
                }
            }
        }
    }

}
