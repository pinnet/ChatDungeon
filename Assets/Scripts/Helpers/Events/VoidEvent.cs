using UnityEngine;
namespace Helpers.Events
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Game Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }
}