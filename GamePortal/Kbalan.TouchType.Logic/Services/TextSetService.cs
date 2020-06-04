using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    class TextSetService : ITextSetService
    {

        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;
        private readonly IValidator<TextSetDto> _textSetValidator;

        public TextSetService(TouchTypeGameContext gameContext, IMapper mapper, IValidator<TextSetDto> TextSetValidator)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
            _textSetValidator = TextSetValidator;
        }

        /// <summary>
        /// Get All TextSet's
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<TextSetDto>> GetAll()
        {
            try
            {
                var getAllResult = _gameContext.TextSets.ProjectToArray<TextSetDto>(_mapper.ConfigurationProvider);
                return Result.Success<IEnumerable<TextSetDto>>(getAllResult);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<IEnumerable<TextSetDto>>(ex.Message);
            }

        }

        /// <summary>
        /// Add new TextSet
        /// </summary>
        public Result<TextSetDto> Add(TextSetDto model)
        {
            ValidationResult validationResult = _textSetValidator.Validate(model, ruleSet: "PostValidation");
            if (!validationResult.IsValid)
            {
               return Result.Failure<TextSetDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
            }
            try
            {
                var DbModel = _mapper.Map<TextSetDb>(model);

                _gameContext.TextSets.Add(DbModel);
                _gameContext.SaveChanges();

                model.Id = DbModel.Id;
                return Result.Success(model);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<TextSetDto>(ex.Message);
            }

        }

        /// <summary>
        /// GetTextSet by Id
        /// </summary>
        public Result<TextSetDto> GetById(int id)
        {

            try
            {
                var getResultById = _gameContext.TextSets.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<TextSetDto>(_mapper.ConfigurationProvider);

                if(getResultById != null)
                return Result.Success<TextSetDto>(getResultById);

                return Result.Failure<TextSetDto>("No text set with such id exist");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<TextSetDto>(ex.Message);
            }
        }

        /// <summary>
        /// Get TextSet by level
        /// </summary>
        public Result<TextSetDto> GetByLevel(int level)
        {
            try
            {
                var texts = _gameContext.TextSets.Where(x => x.LevelOfText == (LevelOfText)level).ToArray();
                if (texts.Length == 0)
                    return Result.Failure<TextSetDto>($"No text set with level {level} exists");
                var text = texts.ElementAt(new Random().Next(0, texts.Length));
                return Result.Success<TextSetDto>(_mapper.Map<TextSetDto>(text));
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<TextSetDto>(ex.Message);
            }   
        }

        /// <summary>
        /// Update TextSet 
        /// </summary>
        /// <param name="model">TextSet model</param>
        public Result Update(TextSetDto model)
        {
            ValidationResult validationResult = _textSetValidator.Validate(model, ruleSet: "PostValidationWithId");
            if (!validationResult.IsValid)
            {
                return Result.Failure<TextSetDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
            }

            try
            {
                var dbModel = _mapper.Map<TextSetDb>(model);

                _gameContext.TextSets.Attach(dbModel);

                var entry = _gameContext.Entry(dbModel);
                entry.Property(x => x.LevelOfText).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.TextForTyping).IsModified = true;

                _gameContext.SaveChanges();

                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        /// <summary>
        /// Delete TextSet by it's id
        /// </summary>
        /// <param name="id">TextSet Id</param>
        /// <returns></returns>
        public Result Delete(int id)
        {
            try
            {
                var dbModel = _gameContext.TextSets.Find(id);

                if (dbModel == null)
                    return Result.Failure($"No text set with id {id} exist");

                _gameContext.TextSets.Remove(dbModel);
                _gameContext.SaveChanges();
                return Result.Success();
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _gameContext.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TextSetService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
