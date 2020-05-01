namespace TheSneakersMob.Models
{
    public class ClientFollower
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int FollowerId { get; set; }
        public Client Follower { get; set; }
    }
}