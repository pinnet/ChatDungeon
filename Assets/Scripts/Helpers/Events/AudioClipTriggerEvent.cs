using UnityEngine;
namespace Helpers.Events
{
    [CreateAssetMenu(fileName = "AudioClipPlayEvent", menuName = "Game Events/Audio Clip Play Event")]
    public class AudioClipTriggerEvent : BaseGameEvent<AudioPlayerControl>
    {
    }
}