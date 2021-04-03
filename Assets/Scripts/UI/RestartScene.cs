using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartScene : MonoBehaviour
    {
        public void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}