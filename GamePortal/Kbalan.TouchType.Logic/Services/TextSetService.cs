using AutoMapper;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kbalan.TouchType.Logic.Services
{
    class TextSetService : ITextSetService
    {

        private readonly TouchTypeGameContext _gameContext;
        private readonly IMapper _mapper;

        public TextSetService(TouchTypeGameContext gameContext, IMapper mapper)
        {
            this._gameContext = gameContext;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get All TextSet's
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TextSetDto> GetAll()
        {
            return _gameContext.TextSets.ProjectToArray<TextSetDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Add new TextSet
        /// </summary>
        public TextSetDto Add(TextSetDto model)
        {
            var DbModel = _mapper.Map<TextSetDb>(model);
            _gameContext.TextSets.Add(DbModel);
            _gameContext.SaveChanges();

            model.Id = DbModel.Id;
            return model;
        }

        /// <summary>
        /// GetTextSet by Id
        /// </summary>
        public TextSetDto GetById(int id)
        {
            return _gameContext.TextSets.Where(x => x.Id == id)
                    .ProjectToSingleOrDefault<TextSetDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Get TextSet by level
        /// </summary>
        public TextSetDto GetByLevel(int level)
        {
            var texts = _gameContext.TextSets.Where(x => x.LevelOfText == (LevelOfText)level).ToArray();
            var text = texts.ElementAt(new Random().Next(0, texts.Length));
            return _mapper.Map<TextSetDto>(text);
                
        }

        /// <summary>
        /// Update TextSet 
        /// </summary>
        /// <param name="model">TextSet model</param>
        public void Update(TextSetDto model)
        {
            var dbModel = _mapper.Map<TextSetDb>(model);
            _gameContext.TextSets.Attach(dbModel);
            var entry = _gameContext.Entry(dbModel);
            entry.Property(x => x.LevelOfText).IsModified = true;
            entry.Property(x => x.Name).IsModified = true;
            entry.Property(x => x.TextForTyping).IsModified = true;

            _gameContext.SaveChanges();
        }

        /// <summary>
        /// Delete TextSet by it's id
        /// </summary>
        /// <param name="id">TextSet Id</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            var dbModel = _gameContext.TextSets.Find(id);
            _gameContext.TextSets.Remove(dbModel);
            _gameContext.SaveChanges();
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
