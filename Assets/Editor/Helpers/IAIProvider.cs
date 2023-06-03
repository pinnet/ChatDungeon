
namespace Helpers.Interfaces
{
    internal interface IAIProvider
    {
        public abstract event System.EventHandler<UIEventArgs> OnAnswerReceived;
        public abstract void AskQuestion(string question,string? context = null);
        public abstract UIEventArgs CreateEventArgs(string? question = null, string? answer = null, string? context = null);
    }
}
