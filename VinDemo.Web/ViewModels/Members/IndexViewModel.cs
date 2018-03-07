namespace VinDemo.Web.ViewModels.Members
{
    public class IndexViewModel
    {
        public MemberTableViewModel MemberTableViewModel { get; set; }

        public string SortBy { get; set; } = "Username";
        public bool SortByDescending { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}