namespace NetIrc2.Capabilities;

public class BeginCapability : AbstractCapability
{
    public override void Negotiate(IrcClient client, params IrcString[] parameters)
    {
        client.IrcCommand("CAP LS :302");
        Context.NextCapability(new RequestCapability());
    }
}