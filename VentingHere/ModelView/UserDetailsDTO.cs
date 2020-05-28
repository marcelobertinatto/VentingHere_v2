namespace VentingHere.ModelView
{
    public class UserDetailsDTO
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserImage { get; set; }
    }
}
