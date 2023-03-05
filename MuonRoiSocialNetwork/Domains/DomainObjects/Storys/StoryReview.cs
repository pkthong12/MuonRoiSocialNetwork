using BaseConfig.EntityObject.Entity;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using System.ComponentModel.DataAnnotations;

namespace ConnectVN.Social_Network.Storys
{
    /// <summary>
    /// Table Review
    /// </summary>
    public class StoryReview : Entity
    {
        /// <summary>
        /// Story guid
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT06))]
        public Guid StoryGuid { get; set; }
        /// <summary>
        /// User guid
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Vote for story
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Content for story
        /// </summary>
        public string Content { get; set; }
        public AppUser UserMember { get; set; }
    }
}
