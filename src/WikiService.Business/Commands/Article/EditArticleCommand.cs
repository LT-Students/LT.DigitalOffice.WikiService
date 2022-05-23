﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using LT.DigitalOffice.WikiService.Business.Commands.Article.Interfaces;
using LT.DigitalOffice.WikiService.Data.Interfaces;
using LT.DigitalOffice.WikiService.Validation.Article.Interfaces;

namespace LT.DigitalOffice.WikiService.Business.Commands.Article
{
  public class EditArticleCommand : IEditArticleCommand
  {
    private readonly IEditArticleRequestValidator _validator;
    private readonly IArticleRepository _repository;
    private readonly IPatchDbArticleMapper _mapper;
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;

    public EditArticleCommand(
       IEditArticleRequestValidator validator,
       IArticleRepository repository,
       IPatchDbArticleMapper mapper,
       IAccessValidator accessValidator,
       IResponseCreator responseCreator)
    {
      _validator = validator;
      _repository = repository;
      _mapper = mapper;
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid articleId, JsonPatchDocument<EditArticleRequest> patch)
    {
      /*if (!(await _accessValidator.HasRightsAsync(Rights.AddEditRemoveWiki)))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(patch, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }*/
    }

  }
}



