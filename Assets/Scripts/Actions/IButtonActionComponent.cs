﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions
{
    public interface IButtonActionComponent
    {
        IEnumerable<GameActionButtonModel> ButtonModels { get; }
    }
}