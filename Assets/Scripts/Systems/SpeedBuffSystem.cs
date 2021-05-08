using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class SpeedBuffSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<SpeedBuffComponent>().Inc<PlayerComponent>().End();
            var speedBuffPool = ecsSystems.GetWorld().GetPool<SpeedBuffComponent>();
            var playerBuffPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var i in filter)
            {
                ref var speedBuffComponent = ref speedBuffPool.Get(i);
                ref var playerComponent = ref playerBuffPool.Get(i);

                speedBuffComponent.timer -= Time.deltaTime;

                if (speedBuffComponent.timer <= 0)
                {
                    playerComponent.playerSpeed /= 2f;
                    speedBuffPool.Del(i);
                }
            }
        }
    }
}