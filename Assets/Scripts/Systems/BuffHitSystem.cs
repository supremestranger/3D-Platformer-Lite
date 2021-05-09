using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BuffHitSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();
            var hitFilter = ecsSystems.GetWorld().Filter<HitComponent>().End();
            var hitPool = ecsSystems.GetWorld().GetPool<HitComponent>();
            var playerFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var jumpPool = ecsSystems.GetWorld().GetPool<JumpBuffComponent>();
            var speedPool = ecsSystems.GetWorld().GetPool<SpeedBuffComponent>();

            foreach (var i in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(i);

                foreach (var j in playerFilter)
                {
                    ref var playerComponent = ref playerPool.Get(j);

                    if (hitComponent.other.CompareTag(Constants.Tags.SpeedBuffTag))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerSpeed *= 2f;
                        speedPool.Add(j);
                        ref var speedBuffComponent = ref speedPool.Get(j);
                        speedBuffComponent.timer = gameData.configuration.speedBuffDuration;
                    }

                    if (hitComponent.other.CompareTag(Constants.Tags.JumpBuffTag))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerJumpForce *= 2f;
                        jumpPool.Add(j);
                        ref var jumpBuffComponent = ref jumpPool.Get(j);
                        jumpBuffComponent.timer = gameData.configuration.jumpBuffDuration;
                    }
                }

            }
        }
    }
}
