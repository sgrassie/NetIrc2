using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetIrc2.Capabilities;

public class Capability : IEquatable<Capability>
{
    public Capability(string name, params IrcString[] parameters)
    {
        Name = name;
        Parameters = parameters.ToImmutableList();
    }

    public string Name { get; }

    public IReadOnlyCollection<IrcString> Parameters { get; }

    public static Capability Parse(IrcString rawCapability)
    {
        var exploded = rawCapability.Split((byte)'=');
        var name = exploded[0];
        var parameters = exploded.Length > 1 
            ? exploded[1].Split((byte)',') 
            : Array.Empty<IrcString>();

        return new Capability(name, parameters);
    }

    public override string ToString()
    {
        return Name;
    }

    public bool Equals(Capability other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Capability)obj);
    }

    public override int GetHashCode()
    {
        return (Name != null ? Name.GetHashCode() : 0);
    }

    public static bool operator ==(Capability left, Capability right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Capability left, Capability right)
    {
        return !Equals(left, right);
    }
}