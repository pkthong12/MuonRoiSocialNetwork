﻿using BaseConfig.EntityObject.Entity;
using MuonRoi.Social_Network.Storys;
using System.ComponentModel.DataAnnotations;

namespace MuonRoi.Social_Network.Tags
{
    /// <summary>
    /// Table Tag for story
    /// </summary>
    public class TagInStory : Entity
    {
        /// <summary>
        /// Story Guid
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT05))]
        public Guid StoryGuid { get; set; }
        /// <summary>
        /// Tag id
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT06))]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public Story Story { get; set; }

    }
}
