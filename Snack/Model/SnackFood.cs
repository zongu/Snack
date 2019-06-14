
namespace Snack.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SnackFood : PointUnit
    {
        public static SnackFood RandomSnackFood(SnackUnit snackUnit)
        {
            var existXLinePosition = new List<int>();
            var existYLinePosition = new List<int>();

            var currentSnackUnit = snackUnit;
            while (currentSnackUnit != null)
            {
                var xPosition = Enumerable.Range(currentSnackUnit.SmallX, currentSnackUnit.SideLength + 1);
                var yPosition = Enumerable.Range(currentSnackUnit.SmallY, currentSnackUnit.SideLength + 1);

                existXLinePosition.AddRange(xPosition.Except(existXLinePosition));
                existYLinePosition.AddRange(yPosition.Except(existYLinePosition));

                currentSnackUnit = currentSnackUnit.NextSnackUnit;
            }

            //// todo 考慮到點跟點交錯部分需要再想一下

            var smallXPosition = Enumerable.Range(0, snackUnit.MaxWidth).Except(existXLinePosition).Where(p => p % snackUnit.SideLength == 0).ToArray();
            var smallYPosition = Enumerable.Range(0, snackUnit.MaxHeight).Except(existYLinePosition).Where(p => p % snackUnit.SideLength == 0).ToArray();
            var randomX = new Random(Guid.NewGuid().GetHashCode()).Next(smallXPosition.Count() - 1);
            var randomY = new Random(Guid.NewGuid().GetHashCode()).Next(smallYPosition.Count() - 1);

            return new SnackFood()
            {
                X1 = smallXPosition[randomX] + snackUnit.SideLength,
                X2 = smallXPosition[randomX],
                Y1 = smallYPosition[randomY] + snackUnit.SideLength,
                Y2 = smallYPosition[randomY],
                SideLength = snackUnit.SideLength
            };
        }
    }
}
