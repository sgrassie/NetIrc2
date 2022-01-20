using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetIrc2.Capabilities;

public class CapabilityContext
{
    private AbstractCapability _currentCapability;

    public CapabilityContext(AbstractCapability initialCapability)
    {
        ServerCapabilities = new List<Capability>();
        NegotiatedCapabilities = new List<Capability>();
        RequiredCapabilities = new List<Capability>
        {
            new("away-notify"),
            new("chghost"),
            new("extended-join"),
            new("message-tags"),
            new("multi-prefix"),
            new("userhost-in-names")
        };
        NextCapability(initialCapability);
    }
    
    public List<Capability> ServerCapabilities { get; }
    public List<Capability> NegotiatedCapabilities { get; }
    public IList<Capability> RequiredCapabilities { get; }

    public void NextCapability(AbstractCapability capability)
    {
        _currentCapability = capability;
        _currentCapability.SetContext(this);
    }

    public void Negotiate(IrcClient client, params IrcString[] parameters)
    {
        _currentCapability.Negotiate(client, parameters);
    }
}