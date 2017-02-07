using Prism.Windows.Mvvm;

namespace Display.Views
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Prism.Windows.Mvvm.SessionStateAwarePage" />
    public abstract class CommonPage : SessionStateAwarePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonPage"/> class.
        /// </summary>
        protected CommonPage()
        {
            ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
