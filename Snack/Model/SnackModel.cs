

namespace Snack.Model
{
    using System;
    using System.Linq;
    using System.Reflection;

    public enum MoveStepType
    {
        Stop,
        Pass,
        EatFood
    }

    public abstract class SnackModel
    {
        protected bool enableChangeDirection;

        protected SnackUnit snackUnit;

        public SnackUnit SnackUnit
        {
            get
                => this.snackUnit;
        }

        public static SnackModel GenerateInstance(string modelName, int maxWidth, int maxHeight, int sideLength, int snackUnitQuantitry)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name == modelName);
            var result = (SnackModel)Activator.CreateInstance(type);
            result.enableChangeDirection = true;

            SnackUnit preSnackUnit = new SnackUnit();
            Enumerable.Range(0, snackUnitQuantitry).ToList().ForEach(index =>
            {
                var isFirstUnit = false;
                SnackUnit currentSnackUnit;
                if (result.snackUnit == null)
                {
                    result.snackUnit = new SnackUnit();
                    currentSnackUnit = result.snackUnit;
                    isFirstUnit = true;
                }
                else
                {
                    preSnackUnit.NextSnackUnit = new SnackUnit();
                    currentSnackUnit = preSnackUnit.NextSnackUnit;
                }

                currentSnackUnit.PreSnackUnit = isFirstUnit ? null : preSnackUnit;
                currentSnackUnit.SideLength = sideLength;
                currentSnackUnit.Direction = SnackDirection.Right;
                currentSnackUnit.NextDirection = SnackDirection.Right;
                currentSnackUnit.MaxHeight = maxHeight;
                currentSnackUnit.MaxWidth = maxWidth;
                currentSnackUnit.X1 = (snackUnitQuantitry - index) * sideLength;
                currentSnackUnit.X2 = (snackUnitQuantitry - index - 1) * sideLength;
                currentSnackUnit.Y1 = sideLength;
                currentSnackUnit.Y2 = 0;

                preSnackUnit = currentSnackUnit;
            });

            return result;
        }

        public bool ChangeDirection(SnackDirection snackDirection)
        {
            if (!this.enableChangeDirection)
            {
                return false;
            }

            this.enableChangeDirection = false;
            this.snackUnit.ChangeFistSnackUnitDirection(snackDirection);
            return true;
        }

        public abstract MoveStepType TryMoveStep(SnackFood snackFood);
    }
}
