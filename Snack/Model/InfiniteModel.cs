
namespace Snack.Model
{
    using Snack.Compoment;

    public class InfiniteModel : SnackModel
    {
        public override MoveStepType TryMoveStep(SnackFood snackFood)
        {
            var compoment = new BasicCompoment()
            {
                Next = true,
                SnackUt = this.snackUnit
            };
            
            compoment.SetCompoment(new IsHitSnackLineCompoment());
            if (!compoment.Next)
            {
                return MoveStepType.Stop;
            }

            this.snackUnit.MoveNextStep();
            this.enableChangeDirection = true;

            if (this.snackUnit.LargeX == snackFood.LargeX &&
               this.snackUnit.SmallX == snackFood.SmallX &&
               this.snackUnit.LargeY == snackFood.LargeY &&
               this.snackUnit.SmallY == snackFood.SmallY)
            {
                this.snackUnit.GrowUp();
                return MoveStepType.EatFood;
            }

            return MoveStepType.Pass;
        }
    }
}
