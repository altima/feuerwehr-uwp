namespace Display.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        void Restart();
    }
}
