
namespace Snack.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Snack.Model;

    [TestClass]
    public class SnackUnitTest
    {
        [TestMethod]
        public void MoveStepTest()
        {
            int snackUnitQuantitry = 10;
            int SideLength = 2;
            SnackUnit snackUnit = null;
            SnackUnit preSnackUnit = new SnackUnit();
            Enumerable.Range(0, snackUnitQuantitry).ToList().ForEach(index =>
            {
                var isFirstUnit = false;
                SnackUnit currentSnackUnit;
                if (snackUnit == null)
                {
                    snackUnit = new SnackUnit();
                    currentSnackUnit = snackUnit;
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
                currentSnackUnit.X1 = (snackUnitQuantitry - index) * SideLength;
                currentSnackUnit.X2 = (snackUnitQuantitry - index - 1) * SideLength;
                currentSnackUnit.Y1 = SideLength;
                currentSnackUnit.Y2 = 0;

                preSnackUnit = currentSnackUnit;
            });

            var count = 0;
            var currentMoveSnackUnit = snackUnit;
            currentMoveSnackUnit.MoveNextStep();
            while (currentMoveSnackUnit != null)
            {   
                Assert.AreEqual((snackUnitQuantitry - count + 1) * SideLength, currentMoveSnackUnit.X1);
                Assert.AreEqual((snackUnitQuantitry - count) * SideLength, currentMoveSnackUnit.X2);
                currentMoveSnackUnit = currentMoveSnackUnit.NextSnackUnit;
                count++;
            }
        }
    }
}
