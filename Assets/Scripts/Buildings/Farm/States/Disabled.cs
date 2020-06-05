namespace Buildings.Farm.States
{
    public class Disabled : FarmingState
    {
        public override void Execute(Farm owner)
        {
            if(owner.CompareTag("Farm"))
                owner.ChangeState(Empty.Instance);
            
        }
    }
}
