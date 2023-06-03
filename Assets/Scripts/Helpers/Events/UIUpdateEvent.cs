using UnityEngine;
namespace Helpers.Events
{
    [CreateAssetMenu(fileName = "UIUpdateEvent", menuName = "Game Events/UI Update Event")]
    public class UIUpdateEvent : BaseGameEvent<UIData>
    {
    }
}