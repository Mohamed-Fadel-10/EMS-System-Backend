﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EMS_SYSTEM;

[Table("FACULTY_TYPE")]
public partial class FacultyType
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("FacultyType")]
    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
