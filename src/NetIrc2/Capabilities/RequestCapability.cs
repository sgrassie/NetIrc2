using System;
using System.Collections.Generic;
using System.Linq;
using NetIrc2.Details;

#nullable enable
namespace NetIrc2.Capabilities;

public class RequestCapability : AbstractCapability
{
    public override void Negotiate(IrcClient client, params IrcString[] parameters)
    {
        Throw.If.NullElements(parameters, nameof(parameters));

        var action = parameters[1];
        var givenCapabilities = parameters[2];

        switch (action)
        {
            case "LS":
                AcceptCapabilities(givenCapabilities);
                RequestCapabilities(client);
                break;
            case "ACK":
                AcknowledgeCapabilities(givenCapabilities);
                Context.NextCapability(new SaslAuthCapability());
                Context.Negotiate(client);
                break;
        }
    }

    private void AcceptCapabilities(IrcString ircString)
    {
        var capabilities = ircString
            .Split((byte)' ')
            .Select(Capability.Parse);
        Context.ServerCapabilities.AddRange(capabilities);
    }

    private void RequestCapabilities(IrcClient client)
    {
        if (client.Options.UseSasl)
        {
            if (Context.ServerCapabilities.Any(x => x.Name == "sasl"))
            {
                Context.RequiredCapabilities.Add(new Capability("sasl"));
            }
            else
            {
                throw new ArgumentException("SASL was requested, but the server does not support it.");
            }
        }

        var capabilities = Context.RequiredCapabilities
            .Intersect(Context.ServerCapabilities)
            .ToList();

        if (capabilities.Any())
        {
            //Negotiate capabilities with the server
            client.IrcCommand($"CAP REQ :{string.Join(' ', capabilities)}");
        }
    }

    private void AcknowledgeCapabilities(IrcString ircString)
    {
        var capabilities = ircString
            .Split((byte)' ')
            .Select(Capability.Parse);
        Context.NegotiatedCapabilities.AddRange(capabilities);
    }
}