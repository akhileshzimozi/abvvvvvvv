using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Blabyap.Common.Model;

namespace Blabyapp.API.DataModels
{
    public class Comments
    {
        [Key]
        public string CommentID { get; set; }
        public string UserEmail { get; set; }
        public string Description { get; set; }
        public string Ratings { get; set; }
        public DateTime? CommentDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CommentBy { get; set; }

        public static implicit operator ProfileComments(Comments cm)
        {
            return new ProfileComments()
            {
                CommentID = cm.CommentID,
                UserEmail = cm.UserEmail,
                Description = cm.Description,
                Ratings = cm.Ratings,
                CommentBy = cm.CommentBy,
                UpdatedDate = cm.UpdatedDate,
                CommentDate = cm.CommentDate
            };
        }
        public static implicit operator Comments(ProfileComments pcm)
        {
            return new Comments()
            {
                CommentID = pcm.CommentID,
                UserEmail = pcm.UserEmail,
                Description = pcm.Description,
                Ratings = pcm.Ratings,
                CommentBy = pcm.CommentBy,
                UpdatedDate = pcm.UpdatedDate,
                CommentDate = pcm.CommentDate
            };
        }
    }
}