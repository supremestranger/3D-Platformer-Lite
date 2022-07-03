using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class WinHitSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();
            var hitFilter = ecsSystems.GetWorld().Filter<HitComponent>().End();
            var hitPool = ecsSystems.GetWorld().GetPool<HitComponent>();
            var playerFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var hitEntity in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(hitEntity);

                foreach (var playerEntity in playerFilter)
                {
                    ref var playerComponent = ref playerPool.Get(playerEntity);

                    if (hitComponent.other.CompareTag(Constants.Tags.WinPointTag))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(playerEntity);
                        gameData.playerWonPanel.SetActive(true);
                    }
                }

            }
        }
    }
}