using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using JetBrains.Annotations;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    public class TextSetService : ITextSetService
    {

        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;

        public TextSetService([NotNull] TouchTypeGameContext gameContext, [NotNull]IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
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
            catch (SqlException ex)
            {
                return Result.Failure<IEnumerable<TextSetDto>>(ex.Message);
            }

        }
        public  async Task<Result<IEnumerable<TextSetDto>>> GetAllAsync()
        {
            try
            {
                var getAllResult = await _gameContext.TextSets.ProjectToArrayAsync<TextSetDto>(_mapper.ConfigurationProvider)
                    .ConfigureAwait(false);
                return Result.Success<IEnumerable<TextSetDto>>(getAllResult);
            }
            catch (SqlException ex)
            {
                return Result.Failure<IEnumerable<TextSetDto>>(ex.Message);
            }
        }

        /// <summary>
        /// Add new TextSet
        /// </summary>
        /// 
        public  Result<TextSetDto> Add(TextSetDto model)
        {
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
        public async Task<Result<TextSetDto>> AddAsync(TextSetDto model)
        {
            try
            {
                var DbModel = _mapper.Map<TextSetDb>(model);

                 _gameContext.TextSets.Add(DbModel);
                await _gameContext.SaveChangesAsync().ConfigureAwait(false);

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
        public Result<Maybe<TextSetDto>> GetById(int id)
        {

            try
            {
                Maybe<TextSetDto> getResultById = _gameContext.TextSets.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<TextSetDto>(_mapper.ConfigurationProvider);
                return Result.Success(getResultById);
               
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<TextSetDto>>(ex.Message);
            }
        }
        public async Task<Result<Maybe<TextSetDto>>> GetByIdAsync(int id)
        {

            try
            {
                 Maybe<TextSetDto> getResultById = await _gameContext.TextSets.Where(x => x.Id == id)
                    .ProjectToSingleOrDefaultAsync<TextSetDto>(_mapper.ConfigurationProvider).ConfigureAwait(false);
                return Result.Success(getResultById);

            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<TextSetDto>>(ex.Message);
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
                {
                    return Result.Failure<TextSetDto>($"No text set with level {level} exists");
                }

                var text = _mapper.Map<TextSetDto>(texts.ElementAt(new Random().Next(0, texts.Length)));
                return Result.Success(text);
            }
            catch (SqlException ex)
            {
                return Result.Failure<TextSetDto>(ex.Message);
            }   
        }
        public async Task<Result<TextSetDto>> GetByLevelAsync(int level)
        {
            try
            {
                var texts = await _gameContext.TextSets.Where(x => x.LevelOfText == (LevelOfText)level)
                    .ToArrayAsync().ConfigureAwait(false);
                if (texts.Length == 0)
                {
                    return Result.Failure<TextSetDto>($"No text set with level {level} exists");
                }

                var text = _mapper.Map<TextSetDto>(texts.ElementAt(new Random().Next(0, texts.Length)));
                return Result.Success(text);
            }
            catch (SqlException ex)
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
            try
            {
                var dbModel = _mapper.Map<TextSetDb>(model);

                _gameContext.TextSets.Attach(dbModel);

                var entry = _gameContext.Entry(dbModel);
                entry.Property(x => x.LevelOfText).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.TextForTyping).IsModified = true;

                _gameContext.SaveChanges();
                return Result.Success($"Text set {dbModel.Name} with id {dbModel.Id} updated succsesfully");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }

        }
        public async Task<Result> UpdateAsync(TextSetDto model)
        {
            try
            {
                var dbModel = _mapper.Map<TextSetDb>(model);

                _gameContext.TextSets.Attach(dbModel);

                var entry = _gameContext.Entry(dbModel);
                entry.Property(x => x.LevelOfText).IsModified = true;
                entry.Property(x => x.Name).IsModified = true;
                entry.Property(x => x.TextForTyping).IsModified = true;

                await _gameContext.SaveChangesAsync().ConfigureAwait(false);
                return Result.Success($"Text set {dbModel.Name} with id {dbModel.Id} updated succsesfully");
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
                {
                    return Result.Failure($"No text set with id {id} exist");
                }
                    
                _gameContext.TextSets.Remove(dbModel);
                _gameContext.SaveChanges();
                return Result.Success($"Text set {dbModel.Name} with id {dbModel.Id} deleted succsesfully");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var dbModel = await _gameContext.TextSets.FindAsync(id).ConfigureAwait(false);

                if (dbModel == null)
                {
                    return Result.Failure($"No text set with id {id} exist");
                }

                _gameContext.TextSets.Remove(dbModel);
                await _gameContext.SaveChangesAsync().ConfigureAwait(false);
                return Result.Success($"Text set {dbModel.Name} with id {dbModel.Id} deleted succsesfully");
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
