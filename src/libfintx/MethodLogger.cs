﻿/*	
 * 	
 *  This file is part of libfintx.
 *  
 *  Copyright (C) 2018 Bjoern Kuensting
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *  
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *  
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 	
 */

using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace libfintx
{
    internal class MethodLogger : IDisposable
    {
        private readonly ILogger _log;
        private readonly string _msgExit;

        private const string _withClazz = "{0}.{1} {2}";
        private const string _withoutClazz = "{0} {1}";

        public MethodLogger(ILogger logger, bool useClazzName = true)
        {
            if (!EbicsLogging.MethodLoggingEnabled) return;
            _log = logger;
            var frame = new StackFrame(1);
            var mth = frame.GetMethod();
            var mname = mth.Name;
            var clazz = mth.DeclaringType.Name;
            var msgEntry = useClazzName
                ? string.Format(_withClazz, clazz, mname, "entry")
                : string.Format(_withoutClazz, mname, "entry");
            _msgExit = useClazzName
                ? string.Format(_withClazz, clazz, mname, "exit")
                : string.Format(_withoutClazz, mname, "exit");
            _log.LogDebug(msgEntry);
        }

        public void Dispose()
        {
            if (!EbicsLogging.MethodLoggingEnabled) return;
            _log.LogDebug(_msgExit);
        }
    }
}
