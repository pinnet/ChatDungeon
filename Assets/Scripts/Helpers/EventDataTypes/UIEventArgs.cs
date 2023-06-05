#nullable enable
using System;

public class UIEventArgs : EventArgs 
{ 
    public string? Answer   { get; set; }
    public string? Question { get; set; }
    public string? Context  { get; set; }
    public string? Status   { get; set; }

    public UIEventArgs(string? question = null, string? answer = null, string? context = null, string? status = null)
    {
        this.Question = question;
        this.Answer = answer;
        this.Context = context;
        this.Status = status;
    }
     
}
