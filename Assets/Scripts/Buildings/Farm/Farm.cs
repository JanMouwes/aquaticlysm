using System;
using UnityEngine;

namespace Buildings.Farm
{
    public class Farm : MonoBehaviour
    {
        private IFarmingState _state;

        private void Start()
        {
            _state = new Empty();
            _state.Start(this);
        }

        private void Update()
        {
            _state.Execute(this);
        }
        
        public void ChangeState(IFarmingState state)
        {
            _state.Stop(this);
            _state = state;
            _state.Start(this);
        }

        public Character FindFarmer()
        {
            return FindObjectOfType<Character>();
        }
    }
}