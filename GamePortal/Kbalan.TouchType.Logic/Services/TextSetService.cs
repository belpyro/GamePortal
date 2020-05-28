using AutoMapper;
using Kbalan.TouchType.Data.Contexts;
using Kbalan.TouchType.Data.Models;
using Kbalan.TouchType.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Implementation of ITextService Add method
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
        /// Implementation of ITextSetService
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public void Delete(int id)
        {
            var dbModel = _gameContext.TextSets.Find(id);
            _gameContext.TextSets.Remove(dbModel);
            _gameContext.SaveChanges();
        }

        /// <summary>
        /// Implementation of ITextSetSetvice GetAllMethod
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TextSetDto> GetAll()
        {
            return _gameContext.TextSets.ProjectToArray<TextSetDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of ITextSetService GetById method
        /// </summary>
        public TextSetDto GetById(int Id)
        {
            return _gameContext.TextSets.Where(x => x.Id == Id)
                    .ProjectToSingleOrDefault<TextSetDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of ITextSetService GetByLevel method
        /// </summary>
/*        public TextSetDto GetByLevel(int level)
        {
            return _gameContext.TextSets.Where(x => x.LevelOfText == level)
                    .ProjectToSingleOrDefault<TextSetDto>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Implementation of ITextSetService Update method
        /// </summary>
        public void Update( TextSetDto model)
        {
            var dbModel = _gameContext.TextSets.Find(model.Id);

            dbModel.LevelOfText = model.LevelOfText;
            dbModel.TextForTyping = model.TextForTyping;

            _gameContext.SaveChanges();
        }*/

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

        public TextSetDto GetByLevel(int level)
        {
            throw new NotImplementedException();
        }

        public void Update(TextSetDto model)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
