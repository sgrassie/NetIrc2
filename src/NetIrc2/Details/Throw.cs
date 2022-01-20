﻿#region License
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
using System.Collections.Generic;
using System.Linq;

namespace NetIrc2.Details
{
    sealed class Throw
    {
        Throw()
        {

        }

        public static Throw If
        {
            get { return null; }
        }
    }

    static class ThrowExtensions
    {
        public static Throw True(this Throw self, bool condition, string paramName = null)
        {
            if (condition) { throw new ArgumentException(paramName); }
            return null;
        }

        public static Throw False(this Throw self, bool condition, string paramName = null)
        {
            if (!condition) { throw new ArgumentException(paramName); }
            return null;
        }

        public static Throw Negative(this Throw self, int value, string paramName = null)
        {
            if (value < 0) { throw new ArgumentOutOfRangeException(paramName); }
            return null;
        }

        public static Throw Null<T>(this Throw self, T value, string paramName = null)
        {
            if (value == null) { throw new ArgumentNullException(paramName); }
            return null;
        }

        public static Throw NullElements<T>(this Throw self, IEnumerable<T> values, string paramName = null)
        {
            Throw.If.Null(values, paramName);
            Throw.If.True(values.Any(value => value == null), paramName);
            return null;
        }

        public static Throw OutOfRange<T>(this Throw self, IList<T> buffer, int offset, int count,
                                          string bufferName = "buffer", string offsetName = "offset", string countName = "count")
        {
            Throw.If.Null(buffer, bufferName);
            if (offset < 0 || offset > buffer.Count) { throw new ArgumentOutOfRangeException(offsetName); }
            if (count < 0 || count > buffer.Count - offset) { throw new ArgumentOutOfRangeException(countName); }
            return null;
        }
    }
}
