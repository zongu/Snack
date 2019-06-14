
namespace Snack.Model
{
    using Snack.Compoment;
    using System.Linq;

    public enum MoveStepType
    {
        Stop,
        Pass,
        EatFood
    }

    public class SnackLines
    {
        private bool enableChangeDirection;

        private SnackUnit snackUnit;

        public SnackUnit SnackUnit
        {
            get
                => snackUnit;
        }

        public static SnackLines GenerateInstance(int pbWidth, int pbHeight, int SideLength, int snackUnitQuantitry)
        {
            var result = new SnackLines()
            {
                enableChangeDirection = true
            };

            SnackUnit preSnackUnit = new SnackUnit();
            Enumerable.Range(0, snackUnitQuantitry).ToList().ForEach(index =>
            {
                var isFirstUnit = false;
                SnackUnit currentSnackUnit;
                if(result.snackUnit == null)
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
                currentSnackUnit.SideLength = SideLength;
                currentSnackUnit.Direction = SnackDirection.Right;
                currentSnackUnit.NextDirection = SnackDirection.Right;
                currentSnackUnit.MaxHeight = pbHeight;
                currentSnackUnit.MaxWidth = pbWidth;
                currentSnackUnit.X1 = (snackUnitQuantitry - index) * SideLength;
                currentSnackUnit.X2 = (snackUnitQuantitry - index -1) * SideLength;
                currentSnackUnit.Y1 = SideLength;
                currentSnackUnit.Y2 = 0;

                preSnackUnit = currentSnackUnit;
            });

            return result;
        }

        public MoveStepType TryMoveStep(SnackFood snackFood)
        {
            var compoment = new BasicCompoment()
            {
                Next = true,
                SnackUt = this.snackUnit
            };

            compoment.SetCompoment(new IsTheEndCompoment());
            compoment.SetCompoment(new IsHitSnackLineCompoment());
            if (!compoment.Next)
            {
                return MoveStepType.Stop;
            }

            this.snackUnit.MoveNextStep();
            this.enableChangeDirection = true;

            if(this.snackUnit.LargeX == snackFood.LargeX &&
               this.snackUnit.SmallX == snackFood.SmallX &&
               this.snackUnit.LargeY == snackFood.LargeY &&
               this.snackUnit.SmallY == snackFood.SmallY)
            {
                this.snackUnit.GrowUp();
                return MoveStepType.EatFood;
            }

            return MoveStepType.Pass;
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
    }
}
