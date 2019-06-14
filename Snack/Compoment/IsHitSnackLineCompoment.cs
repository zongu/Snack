
namespace Snack.Compoment
{
    public class IsHitSnackLineCompoment : SnackCompoment
    {
        public override bool Process()
        {
            var masterUnit = SnackUt;
            var currentSnackUnit = masterUnit.NextSnackUnit;
            while(currentSnackUnit != null)
            {
                if(masterUnit.NextStepPoint.Item1 == currentSnackUnit.LargeX &&
                   masterUnit.NextStepPoint.Item2 == currentSnackUnit.SmallX &&
                   masterUnit.NextStepPoint.Item3 == currentSnackUnit.LargeY &&
                   masterUnit.NextStepPoint.Item4 == currentSnackUnit.SmallY)
                {
                    return false;
                }

                currentSnackUnit = currentSnackUnit.NextSnackUnit;
            }

            return true;
        }
    }
}
