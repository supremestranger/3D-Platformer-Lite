using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class RestartScene : MonoBehaviour
    {
        public void RestartGame()
        {
            Service<SceneService>.Get().ReloadScene();
        }
    }
}