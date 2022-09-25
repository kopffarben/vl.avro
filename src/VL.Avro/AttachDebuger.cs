namespace VL.Avro
{
    /// <summary>
    /// Attach a debuger like VisualStudio.
    /// </summary>
    public class AttachDebuger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachDebuger"/> class.
        /// </summary>
        /// <param name="launch">Lauch the debugger.</param>
        public AttachDebuger(bool launch)
        {
            if (launch)
            {
                System.Diagnostics.Debugger.Launch();
            }
        }
    }
}
