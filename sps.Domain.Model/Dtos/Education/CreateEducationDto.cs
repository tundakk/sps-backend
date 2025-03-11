// using System;
// using System.ComponentModel.DataAnnotations;

// namespace sps.Domain.Model.Dtos.Education
// {
//     /// <summary>
//     /// DTO for creating a new education program
//     /// </summary>
//     public class CreateEducationDto
//     {
//         /// <summary>
//         /// The name of the education program
//         /// </summary>
//         [Required]
//         [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
//         public string Name { get; set; }
        
//         /// <summary>
//         /// The ID of the education category
//         /// </summary>
//         [Required(ErrorMessage = "Education category is required")]
//         public Guid EduCategoryId { get; set; }
//     }
// }