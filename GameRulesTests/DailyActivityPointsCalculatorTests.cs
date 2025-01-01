using GameRules;

namespace GameRulesTests
{
    public class DailyActivityPointsCalculatorTests
    {
        private readonly int _stepsNeededToEarnOnePoint = 0;
        private readonly int _stepsNeededToEarnTwoPoints = 0;

        public DailyActivityPointsCalculatorTests()
        {
            DailyActivityPointsCalculator dummyActivityPointsCalculator = new();
            _stepsNeededToEarnOnePoint = dummyActivityPointsCalculator.StepsNeededToNextActivityPoint;

            dummyActivityPointsCalculator.RegisterSteps(_stepsNeededToEarnOnePoint, DateOnly.MinValue);
            _stepsNeededToEarnTwoPoints = _stepsNeededToEarnOnePoint + dummyActivityPointsCalculator.StepsNeededToNextActivityPoint;
        }

        [Fact]
        public void StepsNeededToNextActivityPoint_EarnOnePoint_RequiredStepsGreaterThanZero()
        {
            Assert.True(_stepsNeededToEarnOnePoint > 0);
        }

        [Fact]
        public void StepsNeededToNextActivityPoint_EarnTwoPoint_RequiredStepsGreaterThanStepsForOnePoint()
        {
            Assert.True(_stepsNeededToEarnTwoPoints > _stepsNeededToEarnOnePoint);
        }

        [Fact]
        public void ActivityPointsEarnedEvent_StepsToEarnOnePoint_OnePoint()
        {
            var values = new Tuple<int, DateOnly>[]
            {
                new(_stepsNeededToEarnOnePoint, DateOnly.MinValue)
            };

            var activityPoints = ActivityPointsFromSteps(values);

            Assert.True(activityPoints == 1);
        }

        [Fact]
        public void ActivityPointsEarnedEvent_LessThanRequiredSteps_ZeroPoints()
        {
            var values = new Tuple<int, DateOnly>[]
            {
                new(_stepsNeededToEarnOnePoint - 1, DateOnly.MinValue)
            };

            var activityPoints = ActivityPointsFromSteps(values);

            Assert.True(activityPoints == 0);
        }

        [Fact]
        public void ActivityPointsEarnedEvent_StepsToEarnTwoPoints_TwoPoints()
        {
            var values = new Tuple<int, DateOnly>[]
            {
                new(_stepsNeededToEarnTwoPoints, DateOnly.MinValue)
            };

            var activityPoints = ActivityPointsFromSteps(values);

            Assert.True(activityPoints == 2);
        }

        [Fact]
        public void RegisterSteps_OnePointForTwoDifferentDays_TwoPoints()
        {
            var values = new Tuple<int, DateOnly>[]
            {
                new(_stepsNeededToEarnOnePoint, DateOnly.MinValue),
                new(_stepsNeededToEarnOnePoint, DateOnly.MinValue.AddDays(1))
            };

            var activityPoints = ActivityPointsFromSteps(values);

            Assert.True(activityPoints == 2);
        }

        [Fact]
        public void RegisterSteps_OnePointInSameDay_OnePoint()
        {
            var values = new Tuple<int, DateOnly>[]
            {
                new(_stepsNeededToEarnOnePoint, DateOnly.MinValue),
                new(_stepsNeededToEarnOnePoint, DateOnly.MinValue)
            };

            var activityPoints = ActivityPointsFromSteps(values);

            Assert.True(activityPoints == 1);
        }

        private static int ActivityPointsFromSteps(Tuple<int, DateOnly>[] stepsToRegister)
        {
            DailyActivityPointsCalculator activityPointsCalculator = new();

            int requiredSteps = activityPointsCalculator.StepsNeededToNextActivityPoint;

            int pointsEarned = 0;

            activityPointsCalculator.ActivityPointsEarnedEvent += (int points) => { pointsEarned += points; };

            foreach (var s in stepsToRegister)
            {
                activityPointsCalculator.RegisterSteps(s.Item1, s.Item2);
            }

            return pointsEarned;
        }
    }
}
