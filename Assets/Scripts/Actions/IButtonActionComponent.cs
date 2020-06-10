using System.Collections.Generic;

namespace Actions
{
    public interface IButtonActionComponent
    {
        IEnumerable<GameActionButtonModel> ButtonModels { get; }
    }
}