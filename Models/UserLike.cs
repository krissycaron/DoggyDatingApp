using System.ComponentModel.DataAnnotations;

namespace serverSideCapstone.Models
{
    public class UserLike
    {
        [Key]
        public int UserLikeId {get; set;}
        public ApplicationUser CurrentUser {get; set;}
        public ApplicationUser LikedUser {get; set;}

        public bool IsLiked {get; set;}
    }
}