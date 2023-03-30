namespace MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Response
{
    public class RoleInitialBaseResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public string GroupName { get; set; } = string.Empty;
        public int GroupId { get; set; } = int.MinValue;
    }
}
