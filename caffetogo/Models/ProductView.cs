﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;

namespace caffetogo.Models
{
    public class ProductView
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string item { get; set; }
        [Required]
        [Display(Name ="Price")]
        [Range(1,int.MaxValue,ErrorMessage ="Nagyobbnak kell lennie mint 0")]
        public int price { get; set; }
        public IFormFile Pictures { get; set; }
    }
}