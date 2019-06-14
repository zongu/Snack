
namespace Snack.Model
{
    using System;

    public class SnackUnit : PointUnit
    {
        public int MaxWidth { get; set; }

        public int MaxHeight { get; set; }

        public SnackUnit NextSnackUnit { get; set; }

        public SnackUnit PreSnackUnit { get; set; }

        public Tuple<int, int, int, int> NextStepPoint
        {
            get
            {
                switch (Direction)
                {
                    case SnackDirection.Up:
                        return Tuple.Create<int, int, int, int>(LargeX, SmallX, LargeY - SideLength, SmallY - SideLength);
                    case SnackDirection.Left:
                        return Tuple.Create<int, int, int, int>(LargeX - SideLength, SmallX - SideLength, LargeY, SmallY);
                    case SnackDirection.Down:
                        return Tuple.Create<int, int, int, int>(LargeX, SmallX, LargeY + SideLength, SmallY + SideLength);
                    case SnackDirection.Right:
                        return Tuple.Create<int, int, int, int>(LargeX + SideLength, SmallX + SideLength, LargeY, SmallY);
                    case SnackDirection.None:
                    default:
                        return Tuple.Create<int, int, int, int>(0, 0, 0, 0);
                }
            }
        }

        public SnackDirection Direction { get; set; }

        public SnackDirection NextDirection { get; set; }

        public SnackDirection ReDirection
        {
            get
            {
                switch (Direction)
                {
                    case SnackDirection.Up:
                        return SnackDirection.Down;
                    case SnackDirection.Left:
                        return SnackDirection.Right;
                    case SnackDirection.Down:
                        return SnackDirection.Up;
                    case SnackDirection.Right:
                        return SnackDirection.Left;
                    default:
                        return SnackDirection.None;
                }
            }
        }

        public void ChangeFistSnackUnitDirection(SnackDirection direction)
        {
            if (direction == ReDirection || direction == Direction)
            {
                return;
            }

            Direction = direction;
            NextDirection = direction;
            if (NextSnackUnit != null)
            {
                NextSnackUnit.NextDirection = direction;
            }
        }

        public void MoveNextStep()
        {
            switch (Direction)
            {
                case SnackDirection.Up:
                    Y1 -= SideLength;
                    Y2 -= SideLength;
                    break;
                case SnackDirection.Left:
                    X1 -= SideLength;
                    X2 -= SideLength;
                    break;
                case SnackDirection.Down:
                    Y1 += SideLength;
                    Y2 += SideLength;
                    break;
                case SnackDirection.Right:
                    X1 += SideLength;
                    X2 += SideLength;
                    break;
                case SnackDirection.None:
                default:
                    break;
            }

            Direction = Direction == NextDirection ? Direction : NextDirection;
            NextDirection = PreSnackUnit != null && PreSnackUnit.Direction != Direction ? PreSnackUnit.Direction : Direction;
            if (NextSnackUnit != null)
            {
                NextSnackUnit.MoveNextStep();
            }
        }

        public void GrowUp()
        {
            var lastSnackUnit = this;
            while(lastSnackUnit.NextSnackUnit != null)
            {
                lastSnackUnit = lastSnackUnit.NextSnackUnit;
            }

            lastSnackUnit.NextSnackUnit = new SnackUnit()
            {
                PreSnackUnit = lastSnackUnit,
                SideLength = lastSnackUnit.SideLength,
                Direction = lastSnackUnit.Direction,
                NextDirection = lastSnackUnit.Direction,
                MaxHeight = lastSnackUnit.MaxHeight,
                MaxWidth = lastSnackUnit.MaxWidth
            };

            switch (lastSnackUnit.NextSnackUnit.Direction)
            {
                case SnackDirection.Up:
                    lastSnackUnit.NextSnackUnit.X1 = lastSnackUnit.X1;
                    lastSnackUnit.NextSnackUnit.X2 = lastSnackUnit.X2;
                    lastSnackUnit.NextSnackUnit.Y1 = lastSnackUnit.Y1 + SideLength;
                    lastSnackUnit.NextSnackUnit.Y2 = lastSnackUnit.Y2 + SideLength;
                    break;
                case SnackDirection.Left:
                    lastSnackUnit.NextSnackUnit.X1 = lastSnackUnit.X1 + SideLength;
                    lastSnackUnit.NextSnackUnit.X2 = lastSnackUnit.X2 + SideLength;
                    lastSnackUnit.NextSnackUnit.Y1 = lastSnackUnit.Y1;
                    lastSnackUnit.NextSnackUnit.Y2 = lastSnackUnit.Y2;
                    break;
                case SnackDirection.Down:
                    lastSnackUnit.NextSnackUnit.X1 = lastSnackUnit.X1;
                    lastSnackUnit.NextSnackUnit.X2 = lastSnackUnit.X2;
                    lastSnackUnit.NextSnackUnit.Y1 = lastSnackUnit.Y1 - SideLength;
                    lastSnackUnit.NextSnackUnit.Y2 = lastSnackUnit.Y2 - SideLength;
                    break;
                case SnackDirection.Right:
                    lastSnackUnit.NextSnackUnit.X1 = lastSnackUnit.X1 - SideLength;
                    lastSnackUnit.NextSnackUnit.X2 = lastSnackUnit.X2 - SideLength;
                    lastSnackUnit.NextSnackUnit.Y1 = lastSnackUnit.Y1;
                    lastSnackUnit.NextSnackUnit.Y2 = lastSnackUnit.Y2;
                    break;
                case SnackDirection.None:
                default:
                    break;
            }
        }
    }
}
