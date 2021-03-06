﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Core.Interfaces {
    public interface ICustomLogger {
        //This interface would be expanded to encapuslate the LogLevelEnum and prevent our code 
        //needing to use Microsoft.Extensions.Logging everywhere.
        public void Log(LogLevel level, string message);

        public void LogException(Exception ex, string message);
    }
}
