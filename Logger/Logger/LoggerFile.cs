﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T>
        where T : Enum
    {
        private readonly LogfileHandler<T> fileHandler;

    }
}
