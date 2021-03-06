#region License
/*
NetIRC2
Copyright (c) 2013 James F. Bellinger <http://www.zer7.com>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.Security.Cryptography;

namespace NetIrc2
{
    partial class IrcClient
    {
        // This slightly convoluted approach to CTCP Time means we don't reveal our system time.
        // That information might be useful if the same PC is running other services that use
        // a RNG seeded by it. We apply a random offset of up to +/- 5 minutes to our responses.
        static double _ctcpTimeOffset;

        static void CtcpTimeInit()
        {
            var offsetBytes = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(offsetBytes);
            double fraction = (double)BitConverter.ToUInt32(offsetBytes, 0) / uint.MaxValue;
            _ctcpTimeOffset = (fraction - 0.5) * 600;
        }

        DateTime CtcpTimeGetNow()
        {
            return DateTime.Now.AddSeconds(_ctcpTimeOffset);
        }
    }
}
