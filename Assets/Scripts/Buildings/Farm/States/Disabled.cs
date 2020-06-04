using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildings.Farm
{
    class Disabled : IFarmingState
    {
        public void Start(Farm owner)
        {

        }

        public void Execute(Farm owner)
        {
            if(owner.CompareTag("Farm"))
                owner.ChangeState(new Empty());

        }

        public void Stop(Farm owner)
        {
        }
    }
}
