using System.ComponentModel.DataAnnotations;

namespace serverSideCapstone.Models
{
    public class UserLikes
    {
        [Key]
        public int UserLikesId {get; set;}
        public ApplicationUser CurrentUser {get; set;}
        public ApplicationUser LikedUserId {get; set;}

        public bool IsLiked {get; set;}
    }
}