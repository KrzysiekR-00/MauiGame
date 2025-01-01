namespace GameRules
{
    internal class ActivityPointsCalculator
    {
        private int _registeredSteps = 0;
        private int _earnedActivityPoints = 0;

        internal int StepsToNextActivityPoint { get; private set; } = 0;

        internal delegate void ActivityPointsEarnedEventHandler(int activityPoints);

        internal event ActivityPointsEarnedEventHandler? ActivityPointsEarnedEvent;

        internal ActivityPointsCalculator()
        {
            StepsToNextActivityPoint = ActivityPointsToSteps(_earnedActivityPoints + 1) - _registeredSteps;
        }

        internal void RegisterSteps(int steps)
        {
            int earnedActivityPoints = CalculateEarnedActivityPointsFromNewSteps(steps);

            if (earnedActivityPoints > 0)
            {
                ActivityPointsEarnedEvent?.Invoke(earnedActivityPoints);
            }
        }

        private int CalculateEarnedActivityPointsFromNewSteps(int steps)
        {
            if (steps < StepsToNextActivityPoint)
            {
                _registeredSteps += steps;
                StepsToNextActivityPoint -= steps;
                return 0;
            }
            else
            {
                _registeredSteps += StepsToNextActivityPoint;
                steps -= StepsToNextActivityPoint;

                _earnedActivityPoints++;

                StepsToNextActivityPoint = ActivityPointsToSteps(_earnedActivityPoints + 1) - _registeredSteps;

                return CalculateEarnedActivityPointsFromNewSteps(steps) + 1;
            }
        }

        private static int ActivityPointsToSteps(int activityPoints)
        {
            var steps = 2.5f * Math.Pow(activityPoints, 2) + 97.5f * activityPoints;
            return (int)Math.Floor(steps);
        }
    }
}
