
namespace Snack.Model
{
    public class PointUnit
    {
        public int X1 { get; set; }

        public int X2 { get; set; }

        public int Y1 { get; set; }

        public int Y2 { get; set; }

        public int SideLength { get; set; }

        public int LargeX
        {
            get => X1 > X2 ? X1 : X2;
        }

        public int SmallX
        {
            get => X1 > X2 ? X2 : X1;
        }

        public int LargeY
        {
            get => Y1 > Y2 ? Y1 : Y2;
        }

        public int SmallY
        {
            get => Y1 > Y2 ? Y2 : Y1;
        }
    }
}
