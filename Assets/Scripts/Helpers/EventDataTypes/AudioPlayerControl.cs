namespace Helpers.Events
{
    public struct AudioPlayerControl
    {
        private AudioPlayerEvents trigger;
        public AudioPlayerEvents Trigger => trigger;

        public AudioPlayerControl(AudioPlayerEvents triggerEvent)
        {
            this.trigger = triggerEvent;
        }
    }
}
