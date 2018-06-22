using System.Collections.Generic;

public struct Telegram
{
    public BaseGameEntity Sender { get; private set; }
    public BaseGameEntity Receiver { get; private set; }
    public MessageType Message { get; private set; }
    public float DispatchTime { get; private set; }
    public Dictionary<string, string> ExtraInfo { get; private set; }

    public Telegram(float time, BaseGameEntity sender, BaseGameEntity receiver, MessageType message,
        Dictionary<string, string> extraInfo = null) : this()
    {
        Sender = sender;
        Receiver = receiver;
        DispatchTime = time;
        Message = message;
        ExtraInfo = extraInfo;
    }
}