namespace NetIrc2.Capabilities;

public class SaslAuthCapability : AbstractCapability
{
    public override void Negotiate(IrcClient client, params IrcString[] parameters)
    {
        if (client.Options.UseSasl)
        {
            client.IrcCommand("AUTHENTICATE :PLAIN");
            Context.NextCapability(new EndCapability());
        }
        else
        {
            Context.NextCapability(new EndCapability());
            Context.Negotiate(client);
        }
    }
}