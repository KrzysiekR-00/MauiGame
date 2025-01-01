using GameRules.Math;

namespace GameRules
{
    internal class DailyActivityPointsCalculator
    {
        private readonly QuadraticFunction _stepsFormula = new(2.5, 97.5, 0);

        private DateOnly? _currentDate = null;
        private int _stepsToday = 0;
        private int _activityPointsToday = 0;

        internal int StepsNeededToNextActivityPoint { get; private set; } = 0;

        internal delegate void ActivityPointsEarnedEventHandler(int activityPoints);

        internal event ActivityPointsEarnedEventHandler? ActivityPointsEarnedEvent;

        internal DailyActivityPointsCalculator()
        {
            StepsNeededToNextActivityPoint = StepsRequiredForActivityPoints(_activityPointsToday + 1);
        }

        internal void RegisterSteps(int steps, DateOnly stepsDate)
        {
            if (!_currentDate.HasValue || stepsDate > _currentDate)
            {
                _currentDate = stepsDate;
                NewDayReset();
            }

            _stepsToday += steps;

            StepsNeededToNextActivityPoint -= steps;

            if (StepsNeededToNextActivityPoint <= 0)
            {
                HandleNewActivityPoints();
            }
        }

        private void NewDayReset()
        {
            _stepsToday -= StepsRequiredForActivityPoints(_activityPointsToday);

            _activityPointsToday = 0;

            StepsNeededToNextActivityPoint = StepsRequiredForActivityPoints(_activityPointsToday + 1);
        }

        private void HandleNewActivityPoints()
        {
            var activityPointsSoFarToday = _activityPointsToday;

            CalculateActivityPointsFromStepsToday();

            StepsNeededToNextActivityPoint = StepsRequiredForActivityPoints(_activityPointsToday + 1);

            if (_activityPointsToday > activityPointsSoFarToday)
            {
                ActivityPointsEarnedEvent?.Invoke(_activityPointsToday - activityPointsSoFarToday);
            }
        }

        private void CalculateActivityPointsFromStepsToday()
        {
            while (_stepsToday >= StepsRequiredForActivityPoints(_activityPointsToday + 1))
            {
                _activityPointsToday++;
            }
        }

        private int StepsRequiredForActivityPoints(int activityPoints)
        {
            return (int)System.Math.Floor(_stepsFormula.Calculate(activityPoints));
        }
    }
}
