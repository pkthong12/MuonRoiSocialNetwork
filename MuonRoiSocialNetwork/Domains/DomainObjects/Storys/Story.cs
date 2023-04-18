using BaseConfig.EntityObject.Entity;
using MuonRoi.Social_Network.Categories;
using MuonRoi.Social_Network.Chapters;
using MuonRoi.Social_Network.Tags;
using System.ComponentModel.DataAnnotations;

namespace MuonRoi.Social_Network.Storys
{
    /// <summary>
    /// Table Story
    /// </summary>
    public class Story : Entity
    {
        /// <summary>
        /// Guid in story 
        /// </summary>
        public override Guid Guid { get; set; }
        /// <summary>
        /// Title of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST00))]
        [MaxLength(255, ErrorMessage = nameof(EnumStoryErrorCode.ST01))]
        [MinLength(3, ErrorMessage = nameof(EnumStoryErrorCode.ST02))]
        public string Story_Title { get; set; }
        /// <summary> 
        /// Description of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST03))]
        [MaxLength(5000, ErrorMessage = nameof(EnumStoryErrorCode.ST04))]
        [MinLength(100, ErrorMessage = nameof(EnumStoryErrorCode.ST05))]
        public string Story_Synopsis { get; set; }
        /// <summary>
        /// Url img of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST06))]
        [MaxLength(1000, ErrorMessage = nameof(EnumNotificationStoryErrorCodes.NT01))]
        public string Img_Url { get; set; }
        /// <summary>
        /// Is show ? tru : false
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST08))]
        public bool IsShow { get; set; }
        /// <summary>
        /// Total view of story
        /// </summary>
        public int TotalView { get; set; }
        /// <summary>
        /// Total like of story
        /// </summary>
        public int TotalFavorite { get; set; }
        /// <summary>
        /// Rating of story
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Slug of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST00))]
        [MaxLength(255, ErrorMessage = nameof(EnumStoryErrorCode.ST01))]
        [MinLength(3, ErrorMessage = nameof(EnumStoryErrorCode.ST02))]
        public string Slug { get; set; }
        /// <summary>
        /// Foreign key category
        /// </summary>
        public int CategoryId { get; set; }
        /// Foreign key
        /// </summary>
        public List<Chapter> Chapters { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<StoryNotifications> StoryNotifications { get; set; }
        /// <summary>
        /// Foreign key
        /// </summary>
        public List<TagInStory> TagInStory { get; set; }
    }
}
