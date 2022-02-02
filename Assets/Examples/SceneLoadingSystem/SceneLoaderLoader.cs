using UnityEngine;

namespace OBubbleKit.Samples
{
    /// <summary>
    /// 一般来说SceneLoader都是在Initial场景加载并DontDestroyOnLoad的
    /// 这里没有Initial场景所以写个工具类代替
    /// </summary>
    public class SceneLoaderLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _sceneLoader;

        private static SceneLoaderLoader _instance;

        private void Start()
        {
            if(_instance == null)
            {
                _instance = this;
                Instantiate(_sceneLoader, transform);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}