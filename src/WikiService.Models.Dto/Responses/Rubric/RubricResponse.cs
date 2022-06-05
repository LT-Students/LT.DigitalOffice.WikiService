﻿using LT.DigitalOffice.WikiService.Models.Dto.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.WikiService.Models.Dto.Responses.Rubric
{
  public record RubricResponse
  {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public Guid? ParentId { get; set; }
      public bool IsActive { get; set; }
      public IEnumerable<RubricInfo> SubRubrics { get; set; }
      public IEnumerable<ArticleInfo> Articles { get; set; }
    }
  }
