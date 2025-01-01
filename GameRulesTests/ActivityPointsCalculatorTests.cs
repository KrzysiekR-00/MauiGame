using GameRules;

namespace GameRulesTests
{
    public class ActivityPointsCalculatorTests
    {
        [Fact]
        public void StepsToNextActivityPoint_NewObject_RequiredStepsGreaterThanZero()
        {
            ActivityPointsCalculator activityPointsCalculator = new();

            Assert.True(activityPointsCalculator.StepsToNextActivityPoint > 0);
        }

        [Fact]
        public void RegisterSteps_RequiredSteps_OnePoint()
        {
            ActivityPointsCalculator activityPointsCalculator = new();

            int requiredSteps = activityPointsCalculator.StepsToNextActivityPoint;

            int pointsEarned = 0;

            activityPointsCalculator.ActivityPointsEarnedEvent += (int points) => { pointsEarned += points; };

            activityPointsCalculator.RegisterSteps(requiredSteps);

            Assert.True(pointsEarned == 1);
        }

        [Fact]
        public void RegisterSteps_LessThanRequiredSteps_ZeroPoints()
        {
            ActivityPointsCalculator activityPointsCalculator = new();

            int requiredSteps = activityPointsCalculator.StepsToNextActivityPoint;

            int pointsEarned = 0;

            activityPointsCalculator.ActivityPointsEarnedEvent += (int points) => { pointsEarned += points; };

            activityPointsCalculator.RegisterSteps(requiredSteps - 1);

            Assert.True(pointsEarned == 0);
        }

        [Fact]
        public void RegisterSteps_StepsToEarnTwoPoints_TwoPoints()
        {
            int requiredStepsToEarn2Points = 0;

            ActivityPointsCalculator dummyActivityPointsCalculator = new();
            int requiredStepsToEarn1Point = dummyActivityPointsCalculator.StepsToNextActivityPoint;
            dummyActivityPointsCalculator.RegisterSteps(requiredStepsToEarn1Point);
            requiredStepsToEarn2Points = requiredStepsToEarn1Point + dummyActivityPointsCalculator.StepsToNextActivityPoint;

            ActivityPointsCalculator activityPointsCalculator = new();

            int pointsEarned = 0;

            activityPointsCalculator.ActivityPointsEarnedEvent += (int points) =>
            {
                pointsEarned += points;
            };

            activityPointsCalculator.RegisterSteps(requiredStepsToEarn2Points);

            Assert.True(pointsEarned == 2);
        }
    }
}
