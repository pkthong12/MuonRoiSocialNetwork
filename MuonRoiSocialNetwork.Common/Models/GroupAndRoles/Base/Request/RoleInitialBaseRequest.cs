namespace MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Request
{
    public class RoleInitialBaseRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int GroupId { get; set; } = int.MinValue;
    }
}
