
namespace Snack
{
    using Snack.Model;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private const int snackWidth = 6;

        private const int trInteval = 100;

        private const int snackLength = 10;

        private SnackLines snack;

        private SnackFood snackFood;

        public Form1()
        {
            InitializeComponent();
            this.snack = SnackLines.GenerateInstance(this.PB.Width, this.PB.Height, snackWidth, snackLength);
            this.snackFood = SnackFood.RandomSnackFood(this.snack.SnackUnit);

            this.DrawPicture();
            this.TR.Interval = trInteval;
            this.TR.Start();
        }

        private void TR_Tick(object sender, System.EventArgs e)
        {
            switch (this.snack.TryMoveStep(this.snackFood))
            {
                case MoveStepType.Stop:
                    this.TR.Stop();
                    MessageBox.Show("Game Over");
                    break;
                case MoveStepType.EatFood:
                    this.snackFood = SnackFood.RandomSnackFood(this.snack.SnackUnit);
                    break;
                case MoveStepType.Pass:
                default:
                    break;
            }

            this.DrawPicture();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.snack.ChangeDirection(SnackDirection.Up);
                    break;
                case Keys.Left:
                    this.snack.ChangeDirection(SnackDirection.Left);
                    break;
                case Keys.Down:
                    this.snack.ChangeDirection(SnackDirection.Down);
                    break;
                case Keys.Right:
                    this.snack.ChangeDirection(SnackDirection.Right);
                    break;
                default:
                    break;
            }
        }

        private void DrawPicture()
        {
            var bmp = new Bitmap(this.PB.Width, this.PB.Height);
            var sb = new SolidBrush(Color.Black);
            var g = Graphics.FromImage(bmp);

            //// draw snack
            var currentSnackUnit = this.snack.SnackUnit;
            while (currentSnackUnit != null)
            {
                g.FillRectangle(sb, currentSnackUnit.SmallX, currentSnackUnit.SmallY, currentSnackUnit.SideLength, currentSnackUnit.SideLength);
                currentSnackUnit = currentSnackUnit.NextSnackUnit;
            }

            //// draw snack food
            g.FillRectangle(sb, this.snackFood.SmallX, this.snackFood.SmallY, this.snackFood.SideLength, this.snackFood.SideLength);

            this.PB.Image = bmp;
        }
    }
}
