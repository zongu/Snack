
namespace Snack.Compoment
{
    using Snack.Model;

    public abstract class SnackCompoment
    {
        public SnackUnit SnackUt { get; set; }

        public bool Next { get; set; }

        public void SetCompoment(SnackCompoment compoment)
        {
            if (Next)
            {
                compoment.SnackUt = SnackUt;
                Next = compoment.Process();
            }
        }

        public abstract bool Process();
    }

    public class BasicCompoment : SnackCompoment
    {
        public override bool Process()
        {
            return true;
        }
    }
}
