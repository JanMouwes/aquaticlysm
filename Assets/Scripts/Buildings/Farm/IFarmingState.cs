namespace Buildings.Farm
{
    public interface IFarmingState
    {
        void Start(Farm owner);
        
        void Execute(Farm owner);

        void Stop(Farm owner);
    }
}