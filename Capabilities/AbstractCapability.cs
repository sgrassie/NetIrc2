#nullable enable
namespace NetIrc2.Capabilities;

public abstract class AbstractCapability
{
    protected CapabilityContext Context = null!;
    
    public void SetContext(CapabilityContext capabilityContext)
    {
        Context = capabilityContext;
    }

    public abstract void Negotiate(IrcClient client, params IrcString[] parameters);
}