using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class WinHitSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();
            var hitFilter = ecsSystems.GetWorld().Filter<HitComponent>().End();
            var hitPool = ecsSystems.GetWorld().GetPool<HitComponent>();
            var playerFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var i in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(i);

                foreach (var j in playerFilter)
                {
                    ref var playerComponent = ref playerPool.Get(j);

                    if (hitComponent.other.CompareTag(Constants.Tags.WinPointTag))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(j);
                        gameData.playerWonPanel.SetActive(true);
                    }
                }

            }
        }
    }
}