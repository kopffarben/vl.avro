namespace VL.Avro
{
    public class AttachDebuger
    {
        public AttachDebuger(bool launch)
        {
            if (launch)
            {
                System.Diagnostics.Debugger.Launch();
            }   
        }
    }
}
