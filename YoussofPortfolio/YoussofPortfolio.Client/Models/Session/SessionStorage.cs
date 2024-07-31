
namespace YoussofPortfolio.Client.Models.Session
{
    public class SessionStorage
    {
        public List<Project>? Projects { get; set; }
        public bool IsDone { get; set; }
        public bool IsResumeOpened { get; set; }

        public event EventHandler? OnDoneChanged;

        public void RefreshDoneBool()
        {
            OnDoneChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
