namespace NetIrc2.Capabilities;

public class EndCapability : AbstractCapability
{
    public override void Negotiate(IrcClient client, params IrcString[] parameters)
    {
        client.IrcCommand("CAP :END");
    }
}