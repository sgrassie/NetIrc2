using NetIrc2.Events;

namespace NetIrc2
{
    public partial class IrcClient
    {
        protected virtual void OnCapabilityNegotiationChange(params IrcString[] parameters)
        {
            _capabilityContext.Negotiate(this, parameters);
        }

        private void RaiseCapabilityNegotiationChange(CapabilityNegotiationEventArgs e)
        {
            Dispatch(GotCapabilityChange, e);
        }
    }
}