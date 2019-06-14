
namespace Snack.Compoment
{
    using Snack.Model;

    public class IsTheEndCompoment : SnackCompoment
    {
        public override bool Process()
        {
            switch (SnackUt.Direction)
            {
                case SnackDirection.Up:
                    if (SnackUt.SmallY <= 0)
                    {
                        return false;
                    }

                    break;
                case SnackDirection.Left:
                    if (SnackUt.SmallX <= 0)
                    {
                        return false;
                    }

                    break;
                case SnackDirection.Down:
                    if (SnackUt.LargeY >= SnackUt.MaxHeight)
                    {
                        return false;
                    }

                    break;
                case SnackDirection.Right:
                    if (SnackUt.LargeX >= SnackUt.MaxWidth)
                    {
                        return false;
                    }

                    break;
                case SnackDirection.None:
                default:
                    return false;
            }

            return true;
        }
    }
}
