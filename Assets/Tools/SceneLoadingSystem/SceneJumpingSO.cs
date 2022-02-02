using UnityEngine;

namespace OBubbleKit.SceneLoadingSystem
{
    /// <summary>
    /// 负责简单的场景跳转工作，但是不能添加初始后台任务
    /// </summary>
    [CreateAssetMenu(menuName = "SO/SceneJumping")]
    public class SceneJumpingSO : ScriptableObject
    {
        public LoadEventChannelSO JumpingEventChannel;
        public GameSceneSO SceneToJump;
        public string TransitionID;

        [ContextMenu("Jump")]
        public void Jump()
        {
            JumpingEventChannel?.RaiseEvent(SceneToJump, null, TransitionID);
        }
    }
}