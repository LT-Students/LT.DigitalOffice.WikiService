﻿using FluentValidation;
using LT.DigitalOffice.Kernel.Validators;
using LT.DigitalOffice.WikiService.Business.Commands.Rubric.Interfaces;
using LT.DigitalOffice.WikiService.Data.Provider;
using LT.DigitalOffice.WikiService.Models.Db;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.WikiService.Business.Commands.Rubric
{
  public class EditRubricRequestValidator : ExtendedEditRequestValidator<DbRubric, EditRubricRequest>, IEditRubricRequestValidator
  {
    private readonly IDataProvider _provider;

    private async Task<bool> DoesExistAsync(Guid rubricId)
    {
      return await _provider.Rubrics.AnyAsync(x => x.Id == rubricId && x.IsActive);
    }

    private async Task HandleInternalPropertyValidationAsync(
      Operation<EditRubricRequest> requestedOperation,
      ValidationContext<(DbRubric, JsonPatchDocument<EditRubricRequest>)> context)
    {
      Context = context;
      RequestedOperation = requestedOperation;

      #region paths

      AddСorrectPaths(
        new List<string>
        {
          nameof(EditRubricRequest.Name),
          nameof(EditRubricRequest.ParentId),
          nameof(EditRubricRequest.IsActive)
        });

      AddСorrectOperations(nameof(EditRubricRequest.Name), new() { OperationType.Replace });
      AddСorrectOperations(nameof(EditRubricRequest.ParentId), new() { OperationType.Replace });
      AddСorrectOperations(nameof(EditRubricRequest.IsActive), new() { OperationType.Replace });

      #endregion

      #region Name

      AddFailureForPropertyIf(
        nameof(EditRubricRequest.Name),
        x => x == OperationType.Replace,
        new()
        {
          {
            x => !string.IsNullOrWhiteSpace(x.value?.ToString()),
            "Name must not be empty."
          },
          {
            x => x.value.ToString().Trim().Length < 101,
            "Name is too long."
          }
        }, CascadeMode.Stop);

      #endregion

      #region IsActive

      AddFailureForPropertyIf(
       nameof(EditRubricRequest.IsActive),
       x => x == OperationType.Replace,
       new()
       {
         {
           x => bool.TryParse(x.value?.ToString(), out bool _),
           "Incorrect rubric is active format"
         },
       });

      #endregion

      #region ParentId

      await AddFailureForPropertyIfAsync(
        nameof(EditRubricRequest.ParentId),
        x => x == OperationType.Replace,
        new()
        {
          {
            async (x) =>
            {
              if (x.value?.ToString() is null)
              {
                return true;
              }

              return Guid.TryParse(x.value.ToString(), out Guid parentId)
                ? await DoesExistAsync(parentId)
                : false;
            },
            "Parent id doesn`t exist."
          }
        });
    }

    #endregion

    public EditRubricRequestValidator(
      IDataProvider provider)
    {
      _provider = provider;

      RuleForEach(x => x.Item2.Operations)
        .CustomAsync(async (x, context, _) => await HandleInternalPropertyValidationAsync(x, context));
    }
  }
}
