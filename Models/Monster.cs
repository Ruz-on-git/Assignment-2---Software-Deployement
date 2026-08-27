using System.ComponentModel.DataAnnotations;

namespace Assignment_2.Models
{
    public class Monster
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Range(0, 30)]
        public double ChallengeRating { get; set; }

        [Range(1, 1000)]
        public int MaxHealth { get; set; }

        public int CurrentHealth { get; set; }

        [Range(1, 30)]
        public int ArmorClass { get; set; }

        [Range(0, 200)]
        public int Speed { get; set; }

        [Range(1, 30)]
        public int Strength { get; set; }

        [Range(1, 30)]
        public int Dexterity { get; set; }

        [Range(1, 30)]
        public int Constitution { get; set; }

        [Range(1, 30)]
        public int Intelligence { get; set; }

        [Range(1, 30)]
        public int Wisdom { get; set; }

        [Range(1, 30)]
        public int Charisma { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}