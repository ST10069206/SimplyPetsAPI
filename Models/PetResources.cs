namespace PetAPI.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("PetResources")]
    public class PetResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }

}
