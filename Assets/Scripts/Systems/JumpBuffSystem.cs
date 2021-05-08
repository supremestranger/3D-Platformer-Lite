using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class JumpBuffSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSysetms)
        {
            var filter = ecsSysetms.GetWorld().Filter<JumpBuffComponent>().Inc<PlayerComponent>().End();
            var jumpBuffPool = ecsSysetms.GetWorld().GetPool<JumpBuffComponent>();
            var playerPool = ecsSysetms.GetWorld().GetPool<PlayerComponent>();

            foreach (var i in filter)
            {
                ref var jumpBuffComponent = ref jumpBuffPool.Get(i);
                ref var playerComponent = ref playerPool.Get(i);

                jumpBuffComponent.timer -= Time.deltaTime;

                if (jumpBuffComponent.timer <= 0)
                {
                    playerComponent.playerJumpForce /= 2f;
                    jumpBuffPool.Del(i);
                }
            }
        }
    }

}
