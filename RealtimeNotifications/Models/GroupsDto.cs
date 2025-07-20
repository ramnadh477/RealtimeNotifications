namespace RealtimeNotifications.Models
{
    public class GroupsDto
    {
        public int Id { get; set; }
        public bool isJoined { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
    }
    public class UserGroupDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserGroupId { get; set; }
        public string? GroupName { get; set; }
        public string? UserName { get; set; }
        public bool isJoined { get; set; }
        public string Connectionid { get; set; } = "";
    }
}
