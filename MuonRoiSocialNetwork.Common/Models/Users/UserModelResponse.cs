namespace MuonRoiSocialNetwork.Common.Models.Users
{
    public class UserModelResponse : UserModelRequest
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string RoleName { get; set; }
        public string GroupName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
