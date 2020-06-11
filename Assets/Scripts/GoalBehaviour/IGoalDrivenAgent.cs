namespace GoalBehaviour
{
    public interface IGoalDrivenAgent
    {
        void AddSubgoal(IGoal goal);

        void PrioritiseSubgoal(IGoal goal);
    }
}