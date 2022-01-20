using System;
using NetIrc2.Details;

namespace NetIrc2.Events;

/// <summary>
/// Stores a change in a IRCv3 capability negotiation.
/// </summary>
public class CapabilityNegotiationEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new instance of <see cref="CapabilityNegotiationEventArgs"/>.
    /// </summary>
    /// <param name="capability">The capability negotiation.</param>
    public CapabilityNegotiationEventArgs(IrcString capability)
    {
        Throw.If.Null(capability);
        Capability = capability;
    }
        
    /// <summary>
    /// Gets the capability negotiation.
    /// </summary>
    public IrcString Capability { get; }
}